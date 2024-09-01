using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartMart
#pragma warning disable CS0659
{
    internal class Seller : User
    {
        private List<Product> products;

        public Seller() : base()
        {
            products = new List<Product>();
        }
        public Seller(string username, string password, Address residence)
            : base(username, password, residence)
        {
            products = new List<Product>(); 
        }

        public void AddProduct(Product product)
        {
            if (products == null)
            {
                products = new List<Product>(); 
            }

            products.Add(product); 
            Console.WriteLine("Product added successfully!\n");
        }

        public List<Product> GetProducts()
        {
            return products ?? new List<Product>(); 
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Seller other = (Seller)obj;
            return Username == other.Username && Password == other.Password && Residence.Equals(other.Residence);
        }

        public override string ToString()
        {
            string productString = string.Join(", \n", GetProducts().Select(p => p.ToString()));
            if (products.Count == 0)
            {
                productString = "There are no available products!";
            }
            return $"\nSeller name: {Username}\nPassword: {Password}\nAddress: {Residence}\n\nProducts: {productString}";
        }
    }
}
