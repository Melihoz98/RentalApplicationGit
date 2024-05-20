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

        public ProductCopy(string? serialNumber, int productID)
        {
            SerialNumber = serialNumber;
            ProductID = productID;
        
        }

        public string SerialNumber { get; set; }
        public int ProductID { get; set; }
     
    }


}
