using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartMart
#pragma warning disable CS0659
{
    internal class Order
    {
        public List<Product> Products;
        public double TotalPrice;
        public Buyer Buyer;

        public Order(List<Product> products, double totalPrice, Buyer buyer)
        {
            this.Products = products ?? new List<Product>(); 
            this.TotalPrice = totalPrice;
            this.Buyer = buyer;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Order other = (Order)obj;
            return TotalPrice == other.TotalPrice && Buyer.Equals(other.Buyer) && Products.SequenceEqual(other.Products);
        }

        public override string ToString()
        {
            string productsString = string.Join("\n", Products.Select(p => p.ToString()));
            return $"Products:\n{productsString}\nTotal Price: {TotalPrice}$\n\n";
        }
    }
}
