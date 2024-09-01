using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartMart
#pragma warning disable CS0659
{
    internal class Buyer : User
    {
        public Cart cart;
        public List<Order> orderHistory;

        public Buyer() : base() { }
        public Buyer(string username, string password, Address residence)
            : base(username, password, residence)
        {
            cart = new Cart();
            orderHistory = new List<Order>();
        }
       
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Buyer other = (Buyer)obj;
            return Username == other.Username && Password == other.Password && Residence.Equals(other.Residence) && cart.Equals(other.cart) && orderHistory.SequenceEqual(other.orderHistory);
        }

        public void AddOrder(Order order)
        {
            if (orderHistory == null)
            {
                orderHistory = new List<Order>(); 
            }
            orderHistory.Add(order);
        }

        public List<Order> GetOrderHistory()
        {
            if (orderHistory == null)
            {
                return new List<Order>();
            }
            return orderHistory.ToList();
        }

        public int GetOrderCount()
        {
            return orderHistory.Count;
        }

        public Cart GetCart()
        {
            return cart;
        }

        public override string ToString()
        {
            string cartDetails = "";
            string orderHistoryString = "";
            if (cart == null || cart.GetProducts().Count() == 0)
            {
                cartDetails = "\nCART:\nThe current cart is empty";
            }
            else
            {
                cartDetails = "\nCART:\n";
                foreach (var product in cart.GetProducts())
                {
                    cartDetails += product.ToString() + "\n";
                }
                cartDetails += $"Total Price: {cart.GetProducts().Sum(p => p.Price)}$";
            }

            if (orderHistory == null || !orderHistory.Any())
            {
                orderHistoryString = "\n\nORDER HISTORY:\nThere is no order history";
            }
            else
            {
                orderHistoryString = "\n\nORDER HISTORY:\n";
                foreach (var order in orderHistory)
                {
                    orderHistoryString += order.ToString() + "\n";
                }
            }

            return $"\nBuyer name: {Username}\nPassword: {Password}\nAddress: {Residence}\n{cartDetails}\n{orderHistoryString}\n";
        }

        public void CreateNewCartFromHistory()
        {
            if (orderHistory.Count == 0)
            {
                Console.WriteLine("Error: No order history found.");
                return;
            }
            cart = new Cart(orderHistory.Last().Products);
        }

        public static bool operator <(Buyer b1, Buyer b2)
        {
            return b1.cart.GetTotalPrice() < b2.cart.GetTotalPrice();
        }

        public static bool operator >(Buyer b1, Buyer b2)
        {
            return b1.cart.GetTotalPrice() > b2.cart.GetTotalPrice();
        }
    }
}
