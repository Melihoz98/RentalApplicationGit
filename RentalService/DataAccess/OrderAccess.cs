using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentalService.Models;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace RentalService.DataAccess
{
    public class OrderAccess : IOrderAccess
    {
        private readonly string _connectionString;
        private readonly IProductCopyAccess _productCopyAccess;

        public OrderAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RentalConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }

        }

        public OrderAccess(IConfiguration configuration, IProductCopyAccess productCopyAccess)
        {
            _connectionString = configuration.GetConnectionString("RentalConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }

            _productCopyAccess = productCopyAccess ?? throw new ArgumentNullException(nameof(productCopyAccess));
        }

        public int CreateOrder(Order newOrder)
        {
            try
            {
                string insertQuery = @"
 INSERT INTO Orders (customerID, orderDate, startDate, endDate, startTime, endTime, totalHours, subTotalPrice, totalOrderPrice)
 VALUES (@CustomerID, @OrderDate, @StartDate, @EndDate, @StartTime, @EndTime, @TotalHours, @SubTotalPrice, @TotalOrderPrice);
 SELECT SCOPE_IDENTITY();";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, con))
                {
                    insertCommand.Parameters.AddWithValue("@CustomerID", newOrder.CustomerID);
                    insertCommand.Parameters.AddWithValue("@OrderDate", newOrder.OrderDate);
                    insertCommand.Parameters.AddWithValue("@StartDate", newOrder.StartDate);
                    insertCommand.Parameters.AddWithValue("@EndDate", newOrder.EndDate);
                    insertCommand.Parameters.AddWithValue("@StartTime", newOrder.StartTime);
                    insertCommand.Parameters.AddWithValue("@EndTime", newOrder.EndTime);
                    insertCommand.Parameters.AddWithValue("@TotalHours", newOrder.TotalHours);
                    insertCommand.Parameters.AddWithValue("@SubTotalPrice", newOrder.SubTotalPrice);
                    insertCommand.Parameters.AddWithValue("@TotalOrderPrice", newOrder.TotalOrderPrice);

                    con.Open();

                    int newOrderID = Convert.ToInt32(insertCommand.ExecuteScalar());
                    newOrder.OrderID = newOrderID;
                    return newOrderID;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error adding order: {ex.Message}");
                throw;
            }
        }
        public int AddOrder(Order orderToAdd)
        {
            int insertedID = -1;
            using (TransactionScope transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.Serializable
                },
                TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var orderLine in orderToAdd.OrderLines.ToList())
                    {
                        bool isProductAvailable = CheckProductCopyAvailability(orderLine.SerialNumber, orderToAdd.StartDate, orderToAdd.EndDate, orderToAdd.StartTime, orderToAdd.EndTime);
                        if (!isProductAvailable)
                        {
                            var availableCopy = _productCopyAccess.GetAllAvailableProductCopyByProductID(orderLine.Product.ProductID, orderToAdd.StartDate, orderToAdd.EndDate, orderToAdd.StartTime, orderToAdd.EndTime);
                            if (availableCopy != null && availableCopy.Count > 0)
                            {
                                orderToAdd.OrderLines.Remove(orderLine);
                                OrderLine newOrderLine = new OrderLine(-1, availableCopy[0].SerialNumber, orderLine.Product);
                                orderToAdd.OrderLines.Add(newOrderLine);
                            }
                            else
                            {
                                throw new InvalidOperationException($"No available product copies for product ID {orderLine.Product.ProductID}.");
                            }
                        }
                    }

                    using (SqlConnection con = new SqlConnection(_connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmdOrder = con.CreateCommand())
                        {
                            cmdOrder.CommandText = "INSERT INTO Orders (customerID, orderDate, startDate, endDate, startTime, endTime, totalHours, subTotalPrice, totalOrderPrice) OUTPUT INSERTED.orderID VALUES (@CustomerID, @OrderDate, @StartDate, @EndDate, @StartTime, @EndTime, @TotalHours, @SubTotalPrice, @TotalOrderPrice)";
                            cmdOrder.Parameters.AddWithValue("@CustomerID", orderToAdd.CustomerID);
                            cmdOrder.Parameters.AddWithValue("@OrderDate", orderToAdd.OrderDate);
                            cmdOrder.Parameters.AddWithValue("@StartDate", orderToAdd.StartDate);
                            cmdOrder.Parameters.AddWithValue("@EndDate", orderToAdd.EndDate);
                            cmdOrder.Parameters.AddWithValue("@StartTime", orderToAdd.StartTime);
                            cmdOrder.Parameters.AddWithValue("@EndTime", orderToAdd.EndTime);
                            cmdOrder.Parameters.AddWithValue("@TotalHours", orderToAdd.TotalHours);
                            cmdOrder.Parameters.AddWithValue("@SubTotalPrice", orderToAdd.SubTotalPrice);
                            cmdOrder.Parameters.AddWithValue("@TotalOrderPrice", orderToAdd.TotalOrderPrice);
                            insertedID = (int)cmdOrder.ExecuteScalar();
                        }

                        foreach (OrderLine ol in orderToAdd.OrderLines)
                        {
                            using (SqlCommand cmdOl = con.CreateCommand())
                            {
                                cmdOl.CommandText = "INSERT INTO OrderLines (orderID, serialNumber) VALUES (@orderID, @serialNumber)";
                                cmdOl.Parameters.AddWithValue("@orderID", insertedID);
                                cmdOl.Parameters.AddWithValue("@serialNumber", ol.SerialNumber);
                                cmdOl.ExecuteNonQuery();
                            }
                        }
                    }
                    transactionScope.Complete();
                }
                catch
                {
                    throw;
                }
            }
            return insertedID;
        }


        public Order GetOrderById(int orderId)
        {
            Order foundOrder = null;

            try
            {
                string queryString = @"
                    SELECT 
                        orderID, customerID, orderDate, startDate, endDate, startTime, endTime, totalHours, subTotalPrice, totalOrderPrice 
                    FROM 
                        Orders 
                    WHERE 
                        orderID = @Id";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.Add(new SqlParameter("@Id", orderId));

                    con.Open();
                    using (SqlDataReader orderReader = readCommand.ExecuteReader())
                    {
                        if (orderReader.Read())
                        {
                            foundOrder = GetOrderFromReader(orderReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting order by ID: {ex.Message}");
                throw;
            }

            return foundOrder;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> foundOrders = new List<Order>();

            try
            {
                string queryString = "SELECT * FROM Orders";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    con.Open();

                    using (SqlDataReader orderReader = readCommand.ExecuteReader())
                    {
                        while (orderReader.Read())
                        {
                            Order order = GetOrderFromReader(orderReader);
                            foundOrders.Add(order);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all orders: {ex.Message}");
                throw;
            }

            return foundOrders;
        }

        public bool CheckProductCopyAvailability(string serialNumber, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            var productCopy = _productCopyAccess.GetProductCopyBySerialNumber(serialNumber);

            if (productCopy == null)
            {
                return false;
            }

            var availableProductCopies = _productCopyAccess.GetAllAvailableProductCopyByProductID(productCopy.ProductID, startDate, endDate, startTime, endTime);

            return availableProductCopies.Any(pc => pc.SerialNumber == serialNumber);
        }

        public void RemoveOrder(int orderId)
        {
            try
            {
                string deleteQuery = "DELETE FROM Orders WHERE orderID = @Id";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, con))
                {
                    deleteCommand.Parameters.AddWithValue("@Id", orderId);

                    con.Open();
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing order: {ex.Message}");
                throw;
            }
        }

        private Order GetOrderFromReader(SqlDataReader orderReader)
        {
            int orderID = orderReader.GetInt32(orderReader.GetOrdinal("orderID"));
            string customerID = orderReader.GetString(orderReader.GetOrdinal("customerID"));
            DateTime orderDate = orderReader.GetDateTime(orderReader.GetOrdinal("orderDate"));
            DateTime startDate = orderReader.GetDateTime(orderReader.GetOrdinal("startDate"));
            DateTime endDate = orderReader.GetDateTime(orderReader.GetOrdinal("endDate"));
            TimeSpan startTime = orderReader.GetTimeSpan(orderReader.GetOrdinal("startTime"));
            TimeSpan endTime = orderReader.GetTimeSpan(orderReader.GetOrdinal("endTime"));
            int totalHours = Convert.ToInt32(orderReader.GetDecimal(orderReader.GetOrdinal("totalHours")));
            decimal subTotalPrice = orderReader.GetDecimal(orderReader.GetOrdinal("subTotalPrice"));
            decimal totalOrderPrice = orderReader.GetDecimal(orderReader.GetOrdinal("totalOrderPrice"));

            return new Order(orderID, customerID, orderDate, startDate, endDate, startTime, endTime, totalHours, subTotalPrice, totalOrderPrice);
        }
    }
}
