using QuanLyBanHang.Models;
using System.Collections.Generic;

namespace QuanLyBanHang.Helpers
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }

        public Cart()
        {
            this.Items = new List<CartItem>();
        }

        public void AddItem(CartItem item)
        {
            this.Items.Add(item);
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}