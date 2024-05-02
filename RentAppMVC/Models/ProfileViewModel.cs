namespace RentAppMVC.Models
{
    public class ProfileViewModel
    {
        public BusinessCustomer BusinessCustomer { get; set; }
        public PrivateCustomer PrivateCustomer { get; set; }
        public string CustomerType { get; set; }
    }
}
