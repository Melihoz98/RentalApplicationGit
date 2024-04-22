using RentalService.DataAccess;
using RentalService.DTO;
using RentalService.Models;


namespace RentalService.Business
{
    public class ProductDataLogic : IProductData
    {
        private readonly IProductAccess _productAccess;

        public ProductDataLogic(IProductAccess productAccess)
        {
            _productAccess = productAccess;
        }

        public ProductDto? Get(int idToMatch)
        {
            ProductDto? foundProductDto;
            try
            {
                Product? foundProduct = _productAccess.GetProductById(idToMatch);
                foundProductDto = ModelConversion.ProductDtoConvert.FromProduct(foundProduct);
            }
            catch
            {
                foundProductDto = null;
            }
            return foundProductDto;
        }

        public List<ProductDto?>? Get()
        {
            List<ProductDto?>? foundDtos;
            try
            {
                List<Product>? foundProducts = _productAccess.GetProductAll();
                foundDtos = ModelConversion.ProductDtoConvert.FromProductCollection(foundProducts);
            }
            catch (Exception ex)
            {
                foundDtos = null;
                string errorMessage = ex.Message;
                // Handle exception
            }
            return foundDtos;
        }
        public int Add(ProductDto productDto)
        {
            int insertedId = 0;
            try
            {
                Product? dbProduct = ModelConversion.ProductDtoConvert.ToProduct(productDto);
                if (dbProduct != null)
                {
                    insertedId = _productAccess.AddProduct(dbProduct);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                // Handle exception
            }
            return insertedId;
        }

        public void Put(ProductDto productDto)
        {
            try
            {
                // Hent det eksisterende produkt baseret på ID
                Product existingProduct = _productAccess.GetProductById(productDto.ProductID);

                if (existingProduct != null)
                {
                    // Sammenlign og opdater kun ændrede felter
                    if (productDto.ProductName != null && productDto.ProductName != existingProduct.ProductName)
                    {
                        existingProduct.ProductName = productDto.ProductName;
                    }

                    if (productDto.Description != null && productDto.Description != existingProduct.Description)
                    {
                        existingProduct.Description = productDto.Description;
                    }

                    if (productDto.HourlyPrice != null && productDto.HourlyPrice != existingProduct.HourlyPrice)
                    {
                        existingProduct.HourlyPrice = productDto.HourlyPrice;
                    }

                    if (productDto.Inventory != null && productDto.Inventory != existingProduct.Inventory)
                    {
                        existingProduct.Inventory = productDto.Inventory;
                    }

                    if (productDto.CategoryID != null && productDto.CategoryID != existingProduct.CategoryID)
                    {
                        existingProduct.CategoryID = productDto.CategoryID;
                    }

                    // Opdater produktet i databasen med kun ændrede felter
                    _productAccess.UpdateProduct(existingProduct);
                }
            }
            catch (Exception ex)
            {
                // Håndter undtagelsen
                Console.WriteLine($"Error updating product: {ex.Message}");
                throw;
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                _productAccess.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                // Handle exception
            }
        }
    }
}
