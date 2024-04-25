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

        public void AddOrder(Order order)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string queryString = @"INSERT INTO Orders (customerID, orderDate, startDate, endDate, startTime, endTime, totalHours, subTotalPrice, totalOrderPrice)
                                           VALUES (@CustomerID, @OrderDate, @StartDate, @EndDate, @StartTime, @EndTime, @TotalHours, @SubTotalPrice, @TotalOrderPrice);
                                           SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                        command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                        command.Parameters.AddWithValue("@StartDate", order.StartDate);
                        command.Parameters.AddWithValue("@EndDate", order.EndDate);
                        command.Parameters.AddWithValue("@StartTime", order.StartTime);
                        command.Parameters.AddWithValue("@EndTime", order.EndTime);
                        command.Parameters.AddWithValue("@TotalHours", order.TotalHours);
                        command.Parameters.AddWithValue("@SubTotalPrice", order.SubTotalPrice);
                        command.Parameters.AddWithValue("@TotalOrderPrice", order.TotalOrderPrice);

                        con.Open();
                        order.OrderID = Convert.ToInt32(command.ExecuteScalar());
                    }
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
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string queryString = "SELECT * FROM Orders WHERE orderID = @OrderId";

                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@OrderId", orderId);

                        con.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            return GetOrderFromReader(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting order by ID: {ex.Message}");
                throw;
            }

            return null;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string queryString = "SELECT * FROM Orders";

                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        con.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            orders.Add(GetOrderFromReader(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting all orders: {ex.Message}");
                throw;
            }

            return orders;
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string queryString = @"UPDATE Orders SET customerID = @CustomerID, orderDate = @OrderDate, startDate = @StartDate, endDate = @EndDate, 
                                           startTime = @StartTime, endTime = @EndTime, totalHours = @TotalHours, subTotalPrice = @SubTotalPrice, 
                                           totalOrderPrice = @TotalOrderPrice WHERE orderID = @OrderID";

                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                        command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                        command.Parameters.AddWithValue("@StartDate", order.StartDate);
                        command.Parameters.AddWithValue("@EndDate", order.EndDate);
                        command.Parameters.AddWithValue("@StartTime", order.StartTime);
                        command.Parameters.AddWithValue("@EndTime", order.EndTime);
                        command.Parameters.AddWithValue("@TotalHours", order.TotalHours);
                        command.Parameters.AddWithValue("@SubTotalPrice", order.SubTotalPrice);
                        command.Parameters.AddWithValue("@TotalOrderPrice", order.TotalOrderPrice);
                        command.Parameters.AddWithValue("@OrderID", order.OrderID);

                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error updating order: {ex.Message}");
                throw;
            }
        }

        public void DeleteOrder(int orderId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string queryString = "DELETE FROM Orders WHERE orderID = @OrderId";

                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@OrderId", orderId);

                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error deleting order: {ex.Message}");
                throw;
            }
        }

        private Order GetOrderFromReader(SqlDataReader reader)
        {
            return new Order
            {
                OrderID = reader.GetInt32(reader.GetOrdinal("orderID")),
                CustomerID = reader.GetInt32(reader.GetOrdinal("customerID")),
                OrderDate = reader.GetDateTime(reader.GetOrdinal("orderDate")),
                StartDate = reader.GetDateTime(reader.GetOrdinal("startDate")),
                EndDate = reader.GetDateTime(reader.GetOrdinal("endDate")),
                StartTime = reader.GetTimeSpan(reader.GetOrdinal("startTime")),
                EndTime = reader.GetTimeSpan(reader.GetOrdinal("endTime")),
                TotalHours = reader.GetInt32(reader.GetOrdinal("totalHours")),
                SubTotalPrice = reader.GetDecimal(reader.GetOrdinal("subTotalPrice")),
                TotalOrderPrice = reader.GetDecimal(reader.GetOrdinal("totalOrderPrice"))
            };
        }
    }
}
