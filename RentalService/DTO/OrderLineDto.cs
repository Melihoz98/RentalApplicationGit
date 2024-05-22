namespace RentalService.DTO
{
    public class OrderLineDto
    {
        public int OrderID { get; set; }
        public string SerialNumber { get; set; }
        public ProductDto Product { get; set; }
    }
}
