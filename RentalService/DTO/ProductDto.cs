namespace RentalService.DTO
{
    
        
        public class ProductDto
        {


            public ProductDto() { }

            public ProductDto(string? inproductName, string? indescription, decimal? inhourlyPrice, int? ininventory, int? incategoryID)
            {
                ProductName = inproductName;
                Description = indescription;
                HourlyPrice = inhourlyPrice;
                Inventory = ininventory;
                CategoryID = incategoryID;
            }

            public string? ProductName { get; set; }
            public string? Description { get; set; }
            public decimal? HourlyPrice { get; set; }
            public int? Inventory { get; set; }
            public int? CategoryID { get; set; }
        }

    }


