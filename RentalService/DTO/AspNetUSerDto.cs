namespace RentalService.DTO
{
    public class AspNetUserDto
    {
        public AspNetUserDto() { }

        public AspNetUserDto(string id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public string Id { get; set; }
        public string UserName { get; set; }

    }
}
