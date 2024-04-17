using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public class CategoryAccess : ICategoryAccess
    {
        private readonly string _connectionString;

        public CategoryAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RentalConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public List<Category> GetCategoryAll()
        {
            List<Category> foundCategories = new List<Category>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "SELECT categoryID, categoryName FROM Categories";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Category category = GetCategoryFromReader(reader);
                                foundCategories.Add(category);
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

            return foundCategories;
        }

        public Category GetCategoryById(int id)
        {
            Category foundCategory = null;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string queryString = "SELECT categoryID, categoryName FROM Categories WHERE categoryID = @Id";
                    using (SqlCommand command = new SqlCommand(queryString, con))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foundCategory = GetCategoryFromReader(reader);
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

            return foundCategory;
        }

        private Category GetCategoryFromReader(SqlDataReader reader)
        {
            int categoryId = reader.GetInt32(reader.GetOrdinal("categoryID"));
            string categoryName = reader.GetString(reader.GetOrdinal("categoryName"));
            return new Category(categoryId, categoryName);
        }
    }
}
