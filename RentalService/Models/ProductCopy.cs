namespace RentalService.Models
{
    public class ProductCopy
    {
       
        public ProductCopy() { }

        public ProductCopy(int productID, string serialNumber)
        {
            ProductID = productID;
            SerialNumber = serialNumber;
        }

        public ProductCopy(string? serialNumber, int productID, bool rented = false)
        {
            SerialNumber = serialNumber;
            ProductID = productID;
            Rented = rented;
        }

        public string SerialNumber { get; set; }
        public int ProductID { get; set; }
        public bool Rented { get; set; }
    }


}
