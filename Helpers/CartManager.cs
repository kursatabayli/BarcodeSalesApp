using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Helpers
{
    public class CartManager
    {
        public List<CartItem> Cart { get; private set; } = new();
        public decimal TotalAmount => Cart.Sum(i => i.Quantity * i.SalePrice);

        public void AddProduct(CartItem item)
        {
            var existingItem = Cart.FirstOrDefault(p => p.Barcode == item.Barcode);
            if (existingItem != null)
                existingItem.Quantity++;
            else
                Cart.Add(item);
        }

        public void RemoveProduct(long barcode)
        {
            var item = Cart.FirstOrDefault(p => p.Barcode == barcode);
            if (item != null)
            {
                if (item.Quantity > 1)
                    item.Quantity--;
                else
                    Cart.Remove(item);
            }
        }

        public void UpdateQuantity(CartItem item, int newQuantity) => item.Quantity = newQuantity;

        public void ClearCart() => Cart.Clear();
    }

    public class CartItem
    {
        public long Barcode { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Profit { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Quantity * SalePrice;
    }
}
