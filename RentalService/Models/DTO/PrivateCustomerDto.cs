namespace RentalService.Models.DTO
{
    public class PrivateCustomerDto
    {
        public PrivateCustomerDto() { }

        public PrivateCustomerDto(string customerID, string? firstName, string? lastName, string? phoneNumber)
        {
            CustomerID = customerID;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        public string CustomerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
