namespace RentalService.DTO
{
    public class BusinessCustomerDto
    {
        public BusinessCustomerDto() { }

        public BusinessCustomerDto(string? companyName, string? cvr, string? userID, string? phoneNumber)
        {
            CompanyName = companyName;
            CVR = cvr;
            UserID = userID;
            PhoneNumber = phoneNumber;
        }

        public int BusinessCustomerID { get; set; }
        public string? CompanyName { get; set; }
        public string? CVR { get; set; }
        public string? UserID { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
