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

        public List<ProductCopy> GetProductCopiesAll()
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
                Console.WriteLine($"Error retrieving categories: {ex.Message}");
                throw;
            }

            return foundProductCopies;
        }
        
        public ProductCopy GetBySerialNumber(string serialNumber)
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
                        command.Parameters.AddWithValue("@Id", serialNumber);
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
                Console.WriteLine($"Error retrieving category by ID: {ex.Message}");
                throw;
            }

            return foundProductCopy;
        }
        private ProductCopy GetProductCopyFromReader(SqlDataReader reader)
        {
            int ProductID = reader.GetInt32(reader.GetOrdinal("productID"));
            string SerialNumber = reader.GetString(reader.GetOrdinal("serialNumber"));
            return new ProductCopy(ProductID, SerialNumber);
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
                Console.WriteLine($"Error deleting productCopy: {ex.Message}");
                throw;
            }
        }
    }

}
