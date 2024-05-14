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

        public List<BusinessCustomer> GetAllBusinessCustomers()
        {
            List<BusinessCustomer> foundCustomers = new List<BusinessCustomer>();
            try
            {
                string queryString = "SELECT customerID, companyName, CVR, phoneNumber FROM BusinessCustomers";
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
                
                Console.WriteLine($"Error retrieving business customers: {ex.Message}");
                throw;
            }
            return foundCustomers;
        }

        public BusinessCustomer GetBusinessCustomerByCustomerID(string customerID)
        {
            BusinessCustomer foundCustomer = null;
            try
            {
                string queryString = "SELECT customerID, companyName, CVR, phoneNumber FROM BusinessCustomers WHERE customerID = @CustomerID";
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    SqlParameter idParam = new SqlParameter("@CustomerID", customerID);
                    readCommand.Parameters.Add(idParam);
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
                
                Console.WriteLine($"Error retrieving business customer by ID: {ex.Message}");
                throw;
            }
            return foundCustomer;
        }

        public void CreateBusinessCustomer(BusinessCustomer customer)
        {
            try
            {
                string insertString = "INSERT INTO BusinessCustomers (customerID, companyName, CVR, phoneNumber) VALUES (@CustomerID, @CompanyName, @CVR, @PhoneNumber)";
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    createCommand.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                    createCommand.Parameters.AddWithValue("@CVR", customer.CVR);
                    createCommand.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    con.Open();
                    createCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error creating business customer: {ex.Message}");
                throw;
            }
        }

        private BusinessCustomer GetBusinessCustomerFromReader(SqlDataReader customerReader)
        {
            string customerID = customerReader.GetString(customerReader.GetOrdinal("customerID"));
            string companyName = customerReader.GetString(customerReader.GetOrdinal("companyName"));
            string CVR = customerReader.GetString(customerReader.GetOrdinal("CVR"));
            string phoneNumber = customerReader.GetString(customerReader.GetOrdinal("phoneNumber"));
            return new BusinessCustomer(customerID, companyName, CVR, phoneNumber);
        }
    }
}
