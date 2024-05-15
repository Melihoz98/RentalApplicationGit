namespace RentalService.Models.DTO
{
    public class BusinessCustomerDto
    {
        public BusinessCustomerDto() { }

        public BusinessCustomerDto(string customerID, string? companyName, string? _CVR, string? phoneNumber)
        {
            CustomerID = customerID;
            CompanyName = companyName;
            CVR = _CVR;
            PhoneNumber = phoneNumber;
        }

        public string CustomerID { get; set; }
        public string? CompanyName { get; set; }
        public string? CVR { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
