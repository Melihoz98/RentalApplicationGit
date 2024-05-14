using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public class OrderAccess : IOrderAccess
    {
        private readonly string _connectionString;

        public OrderAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RentalConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public int AddOrder(Order newOrder)
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
                    // ExecuteScalar is used to get the newly inserted order ID
                    int newOrderID = Convert.ToInt32(insertCommand.ExecuteScalar());
                    newOrder.OrderID = newOrderID; // Update the order object with the new order ID
                    return newOrderID;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error adding order: {ex.Message}");
                throw;
            }
        }



        public Order GetOrderById(int orderId)
        {
            Order foundOrder = null;

            try
            {
                string queryString = "SELECT orderID, customerID, orderDate, startDate, endDate, startTime, endTime, totalHours, subTotalPrice, totalOrderPrice FROM Orders WHERE orderID = @Id";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    SqlParameter idParam = new SqlParameter("@Id", orderId);
                    readCommand.Parameters.Add(idParam);

                        con.Open();
                        SqlDataReader orderReader = readCommand.ExecuteReader();

                        if (orderReader.Read())
                        {
                            foundOrder = GetOrderFromReader(orderReader);
                        }
                }
                
            }
            catch (Exception ex)
            {
                // Handle exception
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

                     SqlDataReader orderReader = readCommand.ExecuteReader();

                    while (orderReader.Read())
                     {
                        Order order = GetOrderFromReader(orderReader);
                        foundOrders.Add(order);
                     }
                } 
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting all orders: {ex.Message}");
                throw;
            }

            return foundOrders;
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
            int totalHours = orderReader.GetInt32(orderReader.GetOrdinal("totalHours"));
            decimal subTotalPrice = orderReader.GetDecimal(orderReader.GetOrdinal("subTotalPrice"));
            decimal totalOrderPrice = orderReader.GetDecimal(orderReader.GetOrdinal("totalOrderPrice"));

            return new Order(orderID, customerID, orderDate, startDate, endDate, startTime, endTime, totalHours, subTotalPrice, totalOrderPrice);
     }   
    }
}
