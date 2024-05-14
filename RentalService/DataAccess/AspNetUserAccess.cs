using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentalService.Models;
using System;

namespace RentalService.DataAccess
{
    public class AspNetUserAccess : IAspNetUserAccess
    {
        private readonly string _connectionString;

        public AspNetUserAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RentalConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public string GetAspNetUserById(string id)
        {
            string userName = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT UserName FROM AspNetUsers WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        userName = (string)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error retrieving ASP.NET user name by ID: {ex.Message}");
            }

            return userName;
        }

        public string GetAspNetIdByUserName(string userName)
        {
            string id = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id FROM AspNetUsers WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);
                        id = (string)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error retrieving ASP.NET user ID by user name: {ex.Message}");
            }

            return id;
        }
    }
}
