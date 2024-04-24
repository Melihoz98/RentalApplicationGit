namespace RentalService.DTO
{
    public class PrivateCustomerDto
    {
        public PrivateCustomerDto() { }

        public PrivateCustomerDto(string? firstName, string? lastName, string? userID, string? phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            UserID = userID;
            PhoneNumber = phoneNumber;
        }

        public int PrivateCustomerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserID { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
