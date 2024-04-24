
namespace RentalService.DTO
{
    public class ProductCopyDto
    {
        public ProductCopyDto()
        {
        }

        public ProductCopyDto(string? serialNumber)
        {
            SerialNumber = serialNumber;
        }

        public string? SerialNumber { get; set; }
    }
}


