namespace RentalService.Models
{
    public class BusinessCustomer
    {

        public BusinessCustomer()
        {

        }



        public BusinessCustomer(string? companyName, string? cvr, string? phoneNumber)
        {
           
            CompanyName = companyName;
            CVR = cvr;
            PhoneNumber = phoneNumber;
        }
        public BusinessCustomer(string customerID, string companyName, string cvr, string phoneNumber)
        {
            CustomerID = customerID;
            CompanyName = companyName;
            CVR = cvr;
            PhoneNumber = phoneNumber;
        }

        public string CustomerID { get; set; }
        public string? CompanyName { get; set; }
        public string? CVR { get; set; }
        public string? PhoneNumber { get; set; }
    }

}
