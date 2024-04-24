namespace RentalService.Models
{
    public class ProductCopy
    {
       

        public string SerialNumber { get; set; }
        public int ProductID { get; set; }

        public ProductCopy() { }

        public ProductCopy(string? serialNumber) {

            SerialNumber = serialNumber;
        }


        public ProductCopy( int productID, string? serialNumber)
        {

            SerialNumber = serialNumber;
            ProductID = productID;

        }


    }


}
