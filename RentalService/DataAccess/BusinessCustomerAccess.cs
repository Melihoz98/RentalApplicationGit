using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public class BusinessCustomerAccess : IBusinessCustomerAccess
    {
        private readonly string _connectionString;

        public BusinessCustomerAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RentalConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public int AddBusinessCustomer(BusinessCustomer businessCustomer)
        {
            int insertedId = -1;

            try
            {
                string insertString = "INSERT INTO BusinessCustomers (companyName, CVR, userID, phoneNumber) OUTPUT INSERTED.businessCustomerID VALUES (@CompanyName, @CVR, @UserID, @PhoneNumber)";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    // Prepare SQL
                    createCommand.Parameters.AddWithValue("@CompanyName", businessCustomer.CompanyName);
                    createCommand.Parameters.AddWithValue("@CVR", businessCustomer.CVR);
                    createCommand.Parameters.AddWithValue("@UserID", businessCustomer.UserID);
                    createCommand.Parameters.AddWithValue("@PhoneNumber", businessCustomer.PhoneNumber);

                    con.Open();
                    // Execute save and read generated key (ID)
                    insertedId = (int)createCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error adding business customer: {ex.Message}");
                throw;
            }

            return insertedId;
        }

        public BusinessCustomer GetBusinessCustomerById(int id)
        {
            BusinessCustomer foundCustomer = null;

            try
            {
                string queryString = "SELECT businessCustomerID, companyName, CVR, userID, phoneNumber FROM BusinessCustomers WHERE businessCustomerID = @Id";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@Id", id);

                    con.Open();

                    SqlDataReader customerReader = readCommand.ExecuteReader();

                    if (customerReader.Read())
                    {
                        foundCustomer = GetBusinessCustomerFromReader(customerReader);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error retrieving business customer by ID: {ex.Message}");
                throw;
            }

            return foundCustomer;
        }

        public List<BusinessCustomer> GetAllBusinessCustomers()
        {
            List<BusinessCustomer> foundCustomers = new List<BusinessCustomer>();

            try
            {
                string queryString = "SELECT businessCustomerID, companyName, CVR, userID, phoneNumber FROM BusinessCustomers";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    con.Open();

                    SqlDataReader customerReader = readCommand.ExecuteReader();

                    while (customerReader.Read())
                    {
                        BusinessCustomer customer = GetBusinessCustomerFromReader(customerReader);
                        foundCustomers.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error retrieving all business customers: {ex.Message}");
                throw;
            }

            return foundCustomers;
        }

        private BusinessCustomer GetBusinessCustomerFromReader(SqlDataReader customerReader)
        {
            int customerId = customerReader.GetInt32(customerReader.GetOrdinal("businessCustomerID"));
            string companyName = customerReader.GetString(customerReader.GetOrdinal("companyName"));
            string cvr = customerReader.GetString(customerReader.GetOrdinal("CVR"));
            string userId = customerReader.GetString(customerReader.GetOrdinal("userID"));
            string phoneNumber = customerReader.GetString(customerReader.GetOrdinal("phoneNumber"));

            BusinessCustomer customer = new BusinessCustomer
            {
                BusinessCustomerID = customerId,
                CompanyName = companyName,
                CVR = cvr,
                UserID = userId,
                PhoneNumber = phoneNumber
            };

            return customer;
        }
    }
}
