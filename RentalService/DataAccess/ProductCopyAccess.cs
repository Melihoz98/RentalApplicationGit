using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public class ProductCopyAccess : IProductCopyAccess
    {
        private readonly string _connectionString;

        public ProductCopyAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RentalConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public List<ProductCopy> GetProductCopyAll()
        {
            List<ProductCopy> foundProductCopies = new List<ProductCopy>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "SELECT productID, serialNumber FROM ProductCopies";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductCopy productCopy = GetProductCopyFromReader(reader);
                                foundProductCopies.Add(productCopy);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log, return error response, etc.)
                Console.WriteLine($"Error retrieving product copies: {ex.Message}");
                throw;
            }

            return foundProductCopies;
        }

        public ProductCopy GetProductCopyBySerialNumber(string serialNumber)
        {
            ProductCopy foundProductCopy = null;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "SELECT productID, serialNumber FROM ProductCopies WHERE serialNumber = @SerialNumber";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@SerialNumber", serialNumber);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foundProductCopy = GetProductCopyFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log, return error response, etc.)
                Console.WriteLine($"Error retrieving product copy by serial number: {ex.Message}");
                throw;
            }

            return foundProductCopy;
        }

        public List<ProductCopy> GetAllProductCopyByProductID(int productID)
        {
            List<ProductCopy> foundProductCopies = new List<ProductCopy>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "SELECT productID, serialNumber FROM ProductCopies WHERE productID = @productID AND rented = 1";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@productID", productID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductCopy productCopy = GetProductCopyFromReader(reader);
                                foundProductCopies.Add(productCopy);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log, return error response, etc.)
                Console.WriteLine($"Error retrieving product copies by product ID: {ex.Message}");
                throw;
            }

            return foundProductCopies;
        }



        public void AddProductCopy(ProductCopy productCopy)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "INSERT INTO ProductCopies (productID, serialNumber) VALUES (@ProductId, @SerialNumber)";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@ProductId", productCopy.ProductID);
                        command.Parameters.AddWithValue("@SerialNumber", productCopy.SerialNumber);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error adding product copy: {ex.Message}");
                throw;
            }
        }

        public void UpdateProductCopy(ProductCopy productCopy)
        {
            // Implement update logic if needed
            throw new NotImplementedException();
        }

        public void DeleteProductCopy(string serialNumber)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "DELETE FROM ProductCopies WHERE serialNumber = @SerialNumber";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@SerialNumber", serialNumber);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error deleting product copy: {ex.Message}");
                throw;
            }
        }

        private ProductCopy GetProductCopyFromReader(SqlDataReader reader)
        {
            int productID = reader.GetInt32(reader.GetOrdinal("productID"));
            string serialNumber = reader.GetString(reader.GetOrdinal("serialNumber"));
            return new ProductCopy(productID, serialNumber);
        }

        public List<ProductCopy> GetAllAvailableProductCopyByProductID(int productID, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            List<ProductCopy> availableProductCopies = new List<ProductCopy>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = @"
                SELECT pc.serialNumber
FROM ProductCopies pc
WHERE pc.productID = @productID
AND NOT EXISTS (
    SELECT 1
    FROM Orders o
    INNER JOIN OrderLines ol ON o.orderID = ol.orderID
    WHERE ol.serialNumber = pc.serialNumber
    AND (
        (@startDate = o.startDate AND @endDate = o.endDate AND @startTime <= o.endTime AND @endTime >= o.startTime)
        OR (
            (@startDate = o.startDate AND @endDate = o.endDate)
            AND NOT (@startTime > o.endTime OR @endTime < o.startTime)
        )
        OR (
            (@startDate >= o.startDate AND @endDate <= o.endDate)
            AND NOT (@startTime > o.endTime OR @endTime < o.startTime)
        )
    )
)
";

                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@productID", productID);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@StartTime", startTime);
                        command.Parameters.AddWithValue("@EndTime", endTime);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string serialNumber = reader.GetString(reader.GetOrdinal("serialNumber"));
                                availableProductCopies.Add(new ProductCopy { SerialNumber = serialNumber });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting all available product copies by product ID: {ex.Message}");
                throw;
            }

            return availableProductCopies;
        }
    }
}
