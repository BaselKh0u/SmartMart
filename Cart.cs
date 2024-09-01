using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartMart
#pragma warning disable CS0659
{
    internal class Cart
    {
        private List<Product> products;

        public Cart()
        {
            products = new List<Product>();
        }

        public Cart(IEnumerable<Product> products)
        {
            this.products = new List<Product>(products);
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public List<Product> GetProducts()
        {
            return products.ToList();
        }

        public int GetProductCount()
        {
            return products.Count;
        }

        public double GetTotalPrice()
        {
            return products.Sum(p => p.Price);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Cart other = (Cart)obj;
            return products.SequenceEqual(other.products);
        }

        public override string ToString()
        {
            string cartToString = " ";
            Console.WriteLine("Current cart:");
            foreach (var product in GetProducts())
            {
                cartToString += product.ToString() + "\n";
            }
            return cartToString;
        }
    }
}
