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
