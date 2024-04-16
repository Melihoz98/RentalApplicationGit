using Microsoft.Data.SqlClient;
using RentalService.DataAccess;
using RentalService.Models;
using System;
namespace RentalService.DataAccess
{
    public class CategoryAccess : ICategoryAccess
    {
        readonly string? _connectionString;

        public CategoryAccess(IConfiguration inConfig)
        {
            _connectionString = inConfig.GetConnectionString("RentalConnection");
        }
        public CategoryAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public List<Category> GetCategoryAll() {

            List<Category> foundCategory;
            Category readCategory;

            string queryString = "select categoryID, categoryName from Categories";

            using (SqlConnection con = new SqlConnection(_connectionString))

            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
               con.Open();
                SqlDataReader categoryReader = readCommand.ExecuteReader();
                foundCategory = new List<Category>();
                while (categoryReader.Read())
                {
                    readCategory = GetCategoryFromReader(categoryReader);
                    foundCategory.Add(readCategory);
                }

            }
              return foundCategory;
        }





        public Category GetCategoryById(int findId)
        {
            Category foundCategory;
            //
            string queryString = "select categoryID, categoryName from Categories where categoryID = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                // Prepace SQL
                SqlParameter idParam = new SqlParameter("@Id", findId);
                readCommand.Parameters.Add(idParam);
                //
                con.Open();
                // Execute read
                SqlDataReader categoryReader = readCommand.ExecuteReader();
                foundCategory = new Category();
                while (categoryReader.Read())
                {
                    foundCategory = GetCategoryFromReader(categoryReader);
                }
            }
            return foundCategory;
        }




        private Category GetCategoryFromReader(SqlDataReader categoryReader)
        {
            Category foundCategory;
            int tempId;
            bool differsFromNull;
            string? tempName;
            tempId = categoryReader.GetInt32(categoryReader.GetOrdinal("categoryID"));
            tempName = categoryReader.GetString(categoryReader.GetOrdinal("categoryName"));

            foundCategory = new Category(tempId, tempName);
            return foundCategory;
        }
    }
}
