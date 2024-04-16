namespace RentalService.Models
{
    public class OrderLine
    {
        public int OrderLineID { get; set; }
        public int OrderID { get; set; }
        public string SerialNumber { get; set; }
        public decimal HourlyPrice { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
