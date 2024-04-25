namespace RentalService.Models
{
    public class OrderLine
    {

        public OrderLine(int orderID, string serialNumber)
        {
            OrderID = orderID;
            SerialNumber = serialNumber;
        }

        public int OrderID { get; set; }
        public string SerialNumber { get; set; }

    }

}
