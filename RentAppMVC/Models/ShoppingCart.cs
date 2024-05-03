namespace RentAppMVC.Models
{
    public class ShoppingCart
    {
        private List<CartItem> items;



        public ShoppingCart ()
        {
            items = new List<CartItem> ();
        }

        public void AddItem(Product product)
        {
            if (!items.Any(item => item.Product.ProductID == product.ProductID))
            {
                items.Add(new CartItem { Product = product });
            }

        
        }

        public void RemoveItem(int productId)
        {
            var itemToRemove = items.FirstOrDefault(item => item.Product.ProductID == productId);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }

        public List<CartItem> GetItems()
        {
            return new List<CartItem> (items);
        }


    }
}
