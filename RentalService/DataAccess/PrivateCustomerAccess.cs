using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentalService.Models;
using System;
using System.Collections.Generic;

namespace RentalService.DataAccess
{
    public class PrivateCustomerAccess : IPrivateCustomerAccess
    {
        private readonly string _connectionString;

        public PrivateCustomerAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RentalConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public int AddPrivateCustomer(PrivateCustomer privateCustomer)
        {
            int insertedId = -1;

            try
            {
                string insertString = "INSERT INTO PrivateCustomers (firstName, lastName, phoneNumber) OUTPUT INSERTED.customerID VALUES (@FirstName, @LastName, @PhoneNumber)";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    // Prepare SQL
                    createCommand.Parameters.AddWithValue("@FirstName", privateCustomer.FirstName);
                    createCommand.Parameters.AddWithValue("@LastName", privateCustomer.LastName);
                    createCommand.Parameters.AddWithValue("@PhoneNumber", privateCustomer.PhoneNumber);

                    con.Open();
                    // Execute save and read generated key (ID)
                    insertedId = (int)createCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error adding private customer: {ex.Message}");
                throw;
            }

            return insertedId;
        }

        public PrivateCustomer GetPrivateCustomerById(string id)
        {
            PrivateCustomer foundCustomer = null;

            try
            {
                string queryString = "SELECT customerID, firstName, lastName, phoneNumber FROM PrivateCustomers WHERE customerID = @Id";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@Id", id);

                    con.Open();

                    SqlDataReader customerReader = readCommand.ExecuteReader();

                    if (customerReader.Read())
                    {
                        foundCustomer = GetPrivateCustomerFromReader(customerReader);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error retrieving private customer by ID: {ex.Message}");
                throw;
            }

            return foundCustomer;
        }

        public List<PrivateCustomer> GetAllPrivateCustomers()
        {
            List<PrivateCustomer> foundCustomers = new List<PrivateCustomer>();

            try
            {
                string queryString = "SELECT customerID, firstName, lastName, phoneNumber FROM PrivateCustomers";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    con.Open();

                    SqlDataReader customerReader = readCommand.ExecuteReader();

                    while (customerReader.Read())
                    {
                        PrivateCustomer customer = GetPrivateCustomerFromReader(customerReader);
                        foundCustomers.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error retrieving all private customers: {ex.Message}");
                throw;
            }

            return foundCustomers;
        }

       

        public void DeletePrivateCustomer(string customerID)
        {
            throw new NotImplementedException();
        }

        private PrivateCustomer GetPrivateCustomerFromReader(SqlDataReader customerReader)
        {
            string customerID = customerReader.GetString(customerReader.GetOrdinal("customerID"));
            string firstName = customerReader.GetString(customerReader.GetOrdinal("firstName"));
            string lastName = customerReader.GetString(customerReader.GetOrdinal("lastName"));
            string phoneNumber = customerReader.GetString(customerReader.GetOrdinal("phoneNumber"));

            return new PrivateCustomer(customerID, firstName, lastName, phoneNumber);
        }
    }
}
