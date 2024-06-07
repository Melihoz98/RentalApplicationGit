namespace RentalService.DTO
{
    public class OrderLineDto
    {
        public OrderLineDto() { }

        public OrderLineDto(int orderID, string serialNumber) 
        {
            OrderID = orderID;
            SerialNumber = serialNumber;
        }

        public int OrderID { get; set; }
        public string SerialNumber { get; set; }
        public ProductDto Product { get; set; }
    }
}
