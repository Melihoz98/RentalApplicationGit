namespace RentalService.Models
{
    public class PrivateCustomer
    {
        public PrivateCustomer() { }


        public PrivateCustomer(string? firstName, string? lastName, string? phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        public PrivateCustomer(string customerID, string firstName, string lastName, string phoneNumber)
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
