using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartMart
{
    internal class Admin
    {
        private string description;
        public List<Buyer> buyers;
        public List<Seller> sellers;

        public Admin(List<Buyer> buyers, List<Seller> sellers, string description)
        {
            this.description = description;
            this.buyers = buyers;
            this.sellers = sellers;
        }

        public Admin()
        {
            description = "No Name";
            buyers = new List<Buyer>();
            sellers = new List<Seller>();
        }

        public Admin(string description)
        {
            this.description = description;
            buyers = new List<Buyer>();
            sellers = new List<Seller>();
        }

        public void AddBuyer(string username, string password, Address address)
        {
            buyers.Add(new Buyer(username, password, address));
        }

        public void AddSeller(string username, string password, Address address)
        {
            sellers.Add(new Seller(username, password, address));
        }

        public void AddProductToSeller(string sellerUsername, string productName, double price, Product.ProductCategory category, bool specialPackaging)
        {
            Seller seller = sellers.FirstOrDefault(s => s != null && s.Username == sellerUsername);
            if (seller == null)
            {
                Console.WriteLine("Error, seller not found.");
                return;
            }
            if (seller.GetProducts().Any(p => p.Name == productName))
            {
                Console.WriteLine("Error, this product/item already exists in your product selling items.");
                return;
            }

            Product product;
            if (specialPackaging)
            {
                product = new SpecialProduct(productName, price, category);
            }
            else
            {
                product = new Product(productName, price, category);
            }
            seller.AddProduct(product);
        }

        public void AddProductToBuyerCart(string buyerUsername, string productName)
        {
            Buyer buyer = buyers.FirstOrDefault(b => b != null && b.Username == buyerUsername);
            if (buyer == null)
            {
                Console.WriteLine("Error, buyer not found.");
                return;
            }
            if (buyer.cart == null)
            {
                buyer.cart = new Cart();
            }
            Seller seller = sellers.FirstOrDefault(s => s != null && s.GetProducts().Any(p => p.Name == productName));
            if (seller == null)
            {
                Console.WriteLine("Error, this product does not exist!");
                return;
            }
            Product product = seller.GetProducts().FirstOrDefault(p => p.Name == productName);
            if (product != null)
            {
                buyer.cart.AddProduct(product);
                Console.WriteLine($"Product has been added successfully to {buyerUsername}'s cart.");
            }
        }

        public void PayForOrder(string buyerUsername)
        {
            Buyer buyer = buyers.FirstOrDefault(b => b != null && b.Username == buyerUsername);
            double totalPrice = buyer.cart.GetTotalPrice();
            if (buyer == null)
            {
                Console.WriteLine("Error, buyer not found.");
                return;
            }

            else if (buyer.cart == null || buyer.cart.GetProductCount() <= 1)
            {
                Console.WriteLine("Sorry, you can't pay with an empty cart or 1 product");
                return;
            }

            else if (totalPrice > 0)
            {
                Order order = new Order(buyer.cart.GetProducts(), totalPrice, buyer);
                buyer.AddOrder(order);
                buyer.cart = new Cart();
                Console.WriteLine($"Order paid successfully. Total price: ${totalPrice}");
                Console.WriteLine("Items in the order:");
                foreach (var product in order.Products)
                {
                    Console.WriteLine($"- {product.Name} (${product.Price})");
                }
            }

            else
            {
                Console.WriteLine("The cart is empty");
            }
        }

        public void DisplayBuyers()
        {
            if (buyers == null || buyers.Count == 0)
            {
                Console.WriteLine("There are no buyers");
                return;
            }

            foreach (Buyer buyer in buyers)
            {
                if (buyer == null)
                {
                    Console.WriteLine("Encountered a null buyer.");
                }
                else
                {
                    Console.WriteLine(buyer.ToString());
                }
            }
        }

        public void DisplaySellers()
        {
            if (sellers.Count == 0)
            {
                Console.WriteLine("There are no sellers");
                return;
            }
            foreach (var seller in sellers.OrderByDescending(s => s.GetProducts().Count()))
            {
                Console.WriteLine(seller.ToString());
            }
        }

        public void DisplayAvailableProducts()
        {
            foreach (var seller in sellers)
            {
                Console.WriteLine($"Seller name: {seller.Username}\n");
                foreach (var product in seller.GetProducts())
                {
                    Console.WriteLine(product.ToString());
                }
            }
        }

        public bool BuyerExists(string buyerUsername)
        {
            return buyers.Any(b => b != null && b.Username == buyerUsername);
        }

        public bool SellerExists(string sellerUsername)
        {
            return sellers.Any(s => s != null && s.Username == sellerUsername);
        }

        public bool ProductExists(string productName)
        {
            return sellers.Any(s => s != null && s.GetProducts().Any(p => p.Name == productName));
        }
        public List<Buyer> GetBuyers()
        {
            return buyers;
        }
        public List<Seller> GetSellers()
        {
            return sellers;
        }
        public int GetBuyersCounter()
        {
            return buyers.Count;
        }
        public int GetSellersCounter()
        {
            return sellers.Count;
        }
        public string GetDescription()
        {
            return description;
        }
        public void CompareBuyers(string firstBuyerUsername, string secondBuyerUsername)
        {
            Buyer firstBuyer = GetBuyers().FirstOrDefault(b => b.Username == firstBuyerUsername);
            Buyer secondBuyer = GetBuyers().FirstOrDefault(b => b.Username == secondBuyerUsername);

            if (firstBuyer == null && secondBuyer == null)
            {
                Console.WriteLine("Both buyers were not found. Please ensure the usernames are correct.");
            }
            else if (firstBuyer == null)
            {
                Console.WriteLine($"The first buyer with username '{firstBuyerUsername}' was not found. Please ensure the username is correct.");
            }
            else if (secondBuyer == null)
            {
                Console.WriteLine($"The second buyer with username '{secondBuyerUsername}' was not found. Please ensure the username is correct.");
            }

            else if (firstBuyer.GetCart() == null && secondBuyer.GetCart() == null)
            {
                Console.WriteLine("Both buyers have an empty or non-existent cart.");
            }
            else if (firstBuyer.GetCart() == null || firstBuyer.GetCart().GetTotalPrice() == 0)
            {
                Console.WriteLine($"The first buyer '{firstBuyerUsername}' has an empty or non-existent cart.");
            }
            else if (secondBuyer.GetCart() == null || secondBuyer.GetCart().GetTotalPrice() == 0)
            {
                Console.WriteLine($"The second buyer '{secondBuyerUsername}' has an empty or non-existent cart.");
            }
            else
            {
                if (firstBuyer < secondBuyer)
                {
                    Console.WriteLine($"{firstBuyerUsername} has a total price that is lower than {secondBuyerUsername}.");
                }
                else
                {
                    Console.WriteLine($"{firstBuyerUsername} has a total price that is higher or the same as {secondBuyerUsername}.");
                }
            }
        }
        public static Admin operator +(Admin admin, Buyer buyer)
        {
            admin.buyers.Add(buyer);
            Console.WriteLine("A new buyer added successfully!");
            return admin;
        }

        public static Admin operator +(Admin admin, Seller seller)
        {
            admin.sellers.Add(seller);
            Console.WriteLine("A new seller added successfully!");
            return admin;
        }
    }
}
