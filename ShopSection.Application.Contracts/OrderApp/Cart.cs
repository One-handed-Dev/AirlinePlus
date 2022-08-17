using System.Linq;
using System.Collections.Generic;

namespace ShopSection.Application.Contracts.OrderApp
{
    public sealed class Cart
    {
        public double TotalPayAmount { get; set; }
        public List<CartItem> Items { get; set; } = new();

        public void Add(CartItem cartItem)
        {
            Items.Add(cartItem);
            TotalPayAmount += cartItem.PayAmount;
        }

        public double CalculateTotalPayAmount() => TotalPayAmount = Items.Sum(x => x.CalculateTotalItemPrice());
    }
}