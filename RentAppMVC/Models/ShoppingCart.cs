using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAppMVC.BusinessLogicLayer;
using RentAppMVC.Models;

public class ShoppingCart
{
    private List<OrderLine> _items;
    private int _fakeOrderID;

    public ShoppingCart()
    {
        _items = new List<OrderLine>();
        _fakeOrderID = 0; // Initialize fake order ID
    }

    public void AddItem(ProductCopy productCopy)
    {
        _items.Add(new OrderLine(_fakeOrderID, productCopy.SerialNumber));
    }

    public void RemoveItem(string serialNumber)
    {
        OrderLine orderLineToRemove = _items.FirstOrDefault(ol => ol.SerialNumber == serialNumber);
        if (orderLineToRemove != null)
        {
            _items.Remove(orderLineToRemove);
        }
    }

    public List<OrderLine> GetItems()
    {
        return _items;
    }

    public void PurchaseOrder(int realOrderID)
    {
        // Update OrderID for each order line
        foreach (var item in _items)
        {
            item.OrderID = realOrderID;
        }
    }
}
