using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public class OrderLineAccess : IOrderLineAccess
    {
        private readonly string _connectionString;

        public OrderLineAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RentalConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public void AddOrderLine(OrderLine orderLine)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = "INSERT INTO OrderLines (orderID, serialNumber) VALUES (@OrderID, @SerialNumber)";
                    command.Parameters.AddWithValue("@OrderID", orderLine.OrderID);
                    command.Parameters.AddWithValue("@SerialNumber", orderLine.SerialNumber);

                    con.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error adding order line: {ex.Message}");
                throw;
            }
        }

        public void RemoveOrderLine(int orderID, string serialNumber)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = "DELETE FROM OrderLines WHERE orderID = @OrderID AND serialNumber = @SerialNumber";
                    command.Parameters.AddWithValue("@OrderID", orderID);
                    command.Parameters.AddWithValue("@SerialNumber", serialNumber);

                    con.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error removing order line: {ex.Message}");
                throw;
            }
        }

        public void UpdateOrderLine(OrderLine orderLine)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = "UPDATE OrderLines SET serialNumber = @SerialNumber WHERE orderID = @OrderID";
                    command.Parameters.AddWithValue("@OrderID", orderLine.OrderID);
                    command.Parameters.AddWithValue("@SerialNumber", orderLine.SerialNumber);

                    con.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error updating order line: {ex.Message}");
                throw;
            }
        }

        public OrderLine GetOrderLineByOrderID(int orderID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = "SELECT orderID, serialNumber FROM OrderLines WHERE orderID = @OrderID";
                    command.Parameters.AddWithValue("@OrderID", orderID);

                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return GetOrderLineFromReader(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting order line by order ID: {ex.Message}");
                throw;
            }
        }

        public List<OrderLine> GetOrderLinesBySerialNumber(string serialNumber)
        {
            List<OrderLine> orderLines = new List<OrderLine>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = "SELECT orderID, serialNumber FROM OrderLines WHERE serialNumber = @SerialNumber";
                    command.Parameters.AddWithValue("@SerialNumber", serialNumber);

                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderLine orderLine = GetOrderLineFromReader(reader);
                            orderLines.Add(orderLine);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting order lines by serial number: {ex.Message}");
                throw;
            }

            return orderLines;
        }

        private OrderLine GetOrderLineFromReader(SqlDataReader reader)
        {
            int orderID = reader.GetInt32(reader.GetOrdinal("orderID"));
            string serialNumber = reader.GetString(reader.GetOrdinal("serialNumber"));
            return new OrderLine(orderID, serialNumber);
        }
    }
}
