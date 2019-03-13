using QuanLyBanHang.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHang.Helpers
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }

        public Cart()
        {
            this.Items = new List<CartItem>();
        }
        public int SumQ()
        {
           return Items.Sum(i => i.Quantity);
        }

        public void AddItem(CartItem item)
        {
            var eItem = this.Items
                 .Where(i => i.Product.ProID == item.Product.ProID)
                 .FirstOrDefault();
            if (eItem != null)
            {
                eItem.Quantity += item.Quantity;

            }
            else
            {
                Items.Add(item);
            }
        }

        public void RemoveItem(int proId)
        {
            var toDeleteItem = this.Items.Where(i => i.Product.ProID == proId).FirstOrDefault();
            if (toDeleteItem != null)
            {
                this.Items.Remove(toDeleteItem);
            }
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}