namespace RentalService.DTO
{
    public class ProductCopyDto
    {
        public ProductCopyDto() { }

        public ProductCopyDto(int productID, string serialNumber)
        {
            ProductID = productID;
            SerialNumber = serialNumber;
        }

        public int ProductID { get; set; }
        public string SerialNumber { get; set; }
    }
}
