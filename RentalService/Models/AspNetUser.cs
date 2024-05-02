namespace RentalService.Models
{
    public class AspNetUser
    {
        public AspNetUser() { }

        public AspNetUser(string id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public string Id { get; set; }
        public string UserName { get; set; }

    }
}
