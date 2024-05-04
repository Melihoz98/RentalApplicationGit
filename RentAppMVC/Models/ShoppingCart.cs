using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;

public class ShoppingCart
{
    private List<Product> _items;

    public ShoppingCart()
    {
        _items = new List<Product>();
    }

    public async Task AddItem(int productId, ProductLogic productLogic)
    {
        // Retrieve product by ID from API
        Product product = await productLogic.GetProductById(productId);
        if (product != null)
        {
            _items.Add(product);
        }
    }

    public void RemoveItem(int productId)
    {
        Product productToRemove = _items.FirstOrDefault(p => p.ProductID == productId);
        if (productToRemove != null)
        {
            _items.Remove(productToRemove);
        }
    }

    public List<Product> GetItems()
    {
        return _items;
    }
}
