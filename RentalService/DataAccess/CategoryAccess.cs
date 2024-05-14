using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentalService.Models;
using System;

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

        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            try
            {
                string queryString = "SELECT CategoryID, CategoryName, ImagePath FROM Categories";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    con.Open();

                    SqlDataReader categoryReader = readCommand.ExecuteReader();

                    while (categoryReader.Read())
                    {
                        Category category = GetCategoryFromReader(categoryReader);
                        categories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error retrieving categories: {ex.Message}");
                throw;
            }

            return categories;
        }

        public int AddCategory(Category category)
        {
            int insertedId = -1;

            try
            {
                string insertString = "INSERT INTO Categories (CategoryName, ImagePath) OUTPUT INSERTED.CategoryID VALUES (@CategoryName, @ImagePath)";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    createCommand.Parameters.AddWithValue("@ImagePath", category.ImagePath ?? (object)DBNull.Value);

                    con.Open();
                    insertedId = (int)createCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error adding category: {ex.Message}");
                throw;
            }

            return insertedId;
        }

        public void DeleteCategory(int categoryId)
        {
            try
            {
                string deleteString = "DELETE FROM Categories WHERE CategoryID = @CategoryId";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
                {
                    deleteCommand.Parameters.AddWithValue("@CategoryId", categoryId);

                    con.Open();
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error deleting category: {ex.Message}");
                throw;
            }
        }

        public Category GetCategoryById(int categoryId)
        {
            Category foundCategory = null;

            try
            {
                string queryString = "SELECT CategoryID, CategoryName, ImagePath FROM Categories WHERE CategoryID = @CategoryId";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@CategoryId", categoryId);

                    con.Open();

                    using (SqlDataReader categoryReader = readCommand.ExecuteReader())
                    {
                        if (categoryReader.Read())
                        {
                            foundCategory = GetCategoryFromReader(categoryReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error retrieving category: {ex.Message}");
                throw;
            }

            return foundCategory;
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                string updateString = "UPDATE Categories SET CategoryName = @CategoryName, ImagePath = @ImagePath WHERE CategoryID = @CategoryId";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    updateCommand.Parameters.AddWithValue("@ImagePath", category.ImagePath ?? (object)DBNull.Value);
                    updateCommand.Parameters.AddWithValue("@CategoryId", category.CategoryID);

                    con.Open();
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error updating category: {ex.Message}");
                throw;
            }
        }

        private Category GetCategoryFromReader(SqlDataReader categoryReader)
        {
            int categoryId = categoryReader.GetInt32(categoryReader.GetOrdinal("CategoryID"));
            string categoryName = categoryReader.GetString(categoryReader.GetOrdinal("CategoryName"));
            string imagePath = categoryReader.IsDBNull(categoryReader.GetOrdinal("ImagePath")) ? null : categoryReader.GetString(categoryReader.GetOrdinal("ImagePath"));

            return new Category(categoryId, categoryName, imagePath);
        }
    }
}
