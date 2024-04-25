namespace RentalService.Models
{
    public class BusinessCustomer
    {
        public BusinessCustomer(string customerID, string companyName, string cVR, string phoneNumber)
        {
            CustomerID = customerID;
            CompanyName = companyName;
            CVR = cVR;
            PhoneNumber = phoneNumber;
        }

        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string CVR { get; set; }
        public string PhoneNumber { get; set; }
    }

}
