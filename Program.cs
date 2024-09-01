using System;
using System.Linq;

namespace SmartMart
{
    internal class Program
    {
        static Admin system = new Admin("E-CommerceProgram");

        static void Main(string[] args)
        {
            while (true)
            {
                PrintMenu();
                string choice = GetMenuChoice();

                switch (int.Parse(choice))
                {
                    case 1:
                        AddBuyer();
                        break;
                    case 2:
                        AddSeller();
                        break;
                    case 3:
                        AddProductToSeller();
                        break;
                    case 4:
                        AddProductToBuyerCart();
                        break;
                    case 5:
                        PayForOrder();
                        break;
                    case 6:
                        system.DisplayBuyers();
                        break;
                    case 7:
                        system.DisplaySellers();
                        break;
                    case 8:
                        CompareTwoBuyers();
                        break;
                    case 9:
                        CreateNewCartFromHistory();
                        break;
                    case 10:
                        return;
                    default:
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        break;
                }
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add a buyer");
            Console.WriteLine("2. Add a seller");
            Console.WriteLine("3. Add a product to a seller");
            Console.WriteLine("4. Add a product to a buyer's cart");
            Console.WriteLine("5. Pay for an order");
            Console.WriteLine("6. Display all details of all buyers");
            Console.WriteLine("7. Display all details of all sellers");
            Console.WriteLine("8. Compare two buyers");
            Console.WriteLine("9. Create a new cart from order history");
            Console.WriteLine("10. Exit");
            Console.Write("Enter your choice: ");
        }

        static string GetMenuChoice()
        {
            string choice;
            do
            {
                choice = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(choice) || !int.TryParse(choice, out _))
                {
                    Console.WriteLine("Invalid input. Please try again:");
                    PrintMenu();
                }
            } while (string.IsNullOrWhiteSpace(choice) || !int.TryParse(choice, out _));

            return choice;
        }

        static void AddBuyer()
        {
            bool fValidInput = false;
            Buyer newBuyer = new Buyer();
            Address newBuyerAddress = new Address();

            fValidInput = GetValidUsername(newBuyer, "buyer");

            if (!fValidInput) return;

            fValidInput = GetValidPassword(newBuyer);

            if (!fValidInput) return;

            GetValidAddress(newBuyerAddress);

            newBuyer.Residence = newBuyerAddress;
            system += newBuyer;
        }

        static void AddSeller()
        {
            bool fValidInput = false;
            Seller newSeller = new Seller();
            Address newSellerAddress = new Address();

            fValidInput = GetValidUsername(newSeller, "seller");

            if (!fValidInput) return;

            fValidInput = GetValidPassword(newSeller);

            if (!fValidInput) return;

            GetValidAddress(newSellerAddress);

            newSeller.Residence = newSellerAddress;
            system += newSeller;
        }

        static bool GetValidUsername(User user, string userType)
        {
            bool fValidInput = false;
            do
            {
                try
                {
                    Console.Write($"Enter {userType} Username: ");
                    user.Username = Console.ReadLine();

                    if (system.BuyerExists(user.Username) || system.SellerExists(user.Username))
                    {
                        Console.WriteLine("Error, Username already exists! Try another Username please.");
                    }
                    else
                    {
                        fValidInput = true;
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            } while (!fValidInput);
            return fValidInput;
        }

        static bool GetValidPassword(User user)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter password: ");
                    user.Password = Console.ReadLine();
                    return true;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }
        }

        static void GetValidAddress(Address address)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter street: ");
                    address.Street = Console.ReadLine();
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Enter building number: ");
                    string inputBuildingnumber = Console.ReadLine();

                    if (!int.TryParse(inputBuildingnumber, out int buildingNumber))
                    {
                        throw new ArgumentException("Invalid input, Building number must be a valid integer.");
                    }
                    address.BuildingNumber = buildingNumber;
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Enter city: ");
                    address.City = Console.ReadLine();
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Enter country: ");
                    address.Country = Console.ReadLine();
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }
        }

        static void AddProductToSeller()
        {
            if (system.GetSellers() == null || system.GetSellers().Count == 0)
            {
                Console.WriteLine("There are no sellers");
                return;
            }
            Product product = new Product();
            string sellerUsername;
            do
            {
                Console.Write("Enter seller Username: ");
                sellerUsername = Console.ReadLine();
                if (!system.SellerExists(sellerUsername))
                {
                    Console.WriteLine("Error, seller not found.");
                }
            } while (string.IsNullOrWhiteSpace(sellerUsername) || !system.SellerExists(sellerUsername));

            while(true)
            {
                try
                {
                    Console.Write("Enter product name: ");
                    string productName= Console.ReadLine();
                    if (system.ProductExists(productName))
                    {
                        throw new ArgumentException ("Error, this product/item already exists in your product selling items.");
                    }
                    product.Name = productName;
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
                
            } 

            while(true)
            {
                try
                {
                    Console.Write("Enter product price: ");
                    string inputProductPrice = Console.ReadLine();
                    if(!double.TryParse(inputProductPrice, out double productPrice))
                    {
                        throw new ArgumentException("Invalid input, Product price must be a valid integer.");
                    }
                    product.Price = productPrice;
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }
            while (true)
            {
                try
                {
                    Console.Write("Enter product category (Enter 1-4 for selecting):\n1. Children\n2. Electronics\n3. Office\n4. Clothing\n");
                    string inputProductCategoryNum = Console.ReadLine();

                    if (!int.TryParse(inputProductCategoryNum, out int ProductCategoryNum) || int.Parse(inputProductCategoryNum) < 1 || int.Parse(inputProductCategoryNum) > 4)
                    {
                        throw new ArgumentException("Error, please re-enter a number only from 1-4!");
                    }
                    product.Category = (Product.ProductCategory)(ProductCategoryNum);
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            } 
            
            bool specialPackaging = GetSpecialPackagingChoice(product);

            system.AddProductToSeller(sellerUsername, product.Name, product.Price, product.Category, specialPackaging);
        }
        static bool GetSpecialPackagingChoice(Product product)
        {
            string specialPackagingChoice;
            while(true)
            {
                try
                {

                    Console.Write("Is this item a special product? (1 for yes, 0 for no): ");
                    specialPackagingChoice = Console.ReadLine();
                     if (specialPackagingChoice != "0" && specialPackagingChoice != "1")
                     {
                        throw new ArgumentException("Error, please re-enter you choice ,0 or 1");
                     }
                    product.SpecialPackaging = bool.TryParse(specialPackagingChoice.ToString(), out _);
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            } 

            return specialPackagingChoice == "1";
        }

        static void AddProductToBuyerCart()
        {
            if (system.GetBuyers() == null || system.GetBuyers().Count == 0)
            {
                Console.WriteLine("There are no buyers");
                return;
            }
            string buyerUsername;
            do
            {
                Console.Write("Enter buyer Username: ");
                buyerUsername = Console.ReadLine();
                if (!system.BuyerExists(buyerUsername))
                {
                    Console.WriteLine("Error, buyer not found.");
                }
            } while (string.IsNullOrWhiteSpace(buyerUsername) || !system.BuyerExists(buyerUsername));

            Console.WriteLine("Available products:");
            system.DisplayAvailableProducts();

            string productName;
            do
            {
                Console.Write("\nEnter product name: ");
                productName = Console.ReadLine();
                if (!system.ProductExists(productName))
                {
                    Console.WriteLine("Error, this product does not exist!");
                }
            } while (string.IsNullOrWhiteSpace(productName) || !system.ProductExists(productName));

            system.AddProductToBuyerCart(buyerUsername, productName);
        }

        static void PayForOrder()
        {
            if (system.GetBuyers() == null || system.GetBuyers().Count == 0)
            {
                Console.WriteLine("There are no buyers");
                return;
            }
            string buyerUsername;
            do
            {
                Console.Write("Enter buyer Username: ");
                buyerUsername = Console.ReadLine();
                if (!system.BuyerExists(buyerUsername))
                {
                    Console.WriteLine("Error, buyer not found.");
                }
            } while (string.IsNullOrWhiteSpace(buyerUsername) || !system.BuyerExists(buyerUsername));

            try
            {
                system.PayForOrder(buyerUsername);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void CompareTwoBuyers()
        {
            if (system.GetBuyers() == null || system.GetBuyers().Count < 2)
            {
                Console.WriteLine("There are no buyers or not enough buyers to compare");
                return;
            }
            else
            {
                string firstBuyerUsername;
                do
                {
                    Console.WriteLine("Please enter the first buyer's username:");
                    firstBuyerUsername = Console.ReadLine();
                    if (!system.BuyerExists(firstBuyerUsername))
                    {
                        Console.WriteLine("Error, buyer not found.");
                    }
                } while (string.IsNullOrWhiteSpace(firstBuyerUsername) || !system.BuyerExists(firstBuyerUsername));
                string secondBuyerUsername;
                do
                {
                    Console.WriteLine("Please enter the second buyer's username:");
                    secondBuyerUsername = Console.ReadLine();
                    if (!system.BuyerExists(secondBuyerUsername))
                    {
                        Console.WriteLine("Error, buyer not found.");
                    }
                } while (string.IsNullOrWhiteSpace(secondBuyerUsername) || !system.BuyerExists(secondBuyerUsername));

                system.CompareBuyers(firstBuyerUsername, secondBuyerUsername);
            }
        }

        static void CreateNewCartFromHistory()
        {
            if (system.GetBuyers() == null || system.GetBuyers().Count ==0)
            {
                Console.WriteLine("There are no buyers");
                return;
            }
            string buyerUsername;
            do
            {
                Console.Write("Enter the buyer Username: ");
                buyerUsername = Console.ReadLine();
                if (!system.BuyerExists(buyerUsername))
                {
                    Console.WriteLine("Error, buyer not found.");
                }
            } while (string.IsNullOrWhiteSpace(buyerUsername) || !system.BuyerExists(buyerUsername));

            Buyer buyer = system.GetBuyers().FirstOrDefault(b => b.Username == buyerUsername);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            if (buyer.GetOrderHistory().Count == 0)
            {
                Console.WriteLine("No order history found for this buyer.");
                return;
            }
            if (!(buyer.GetCart().GetProductCount() == 0))
            {
                Console.WriteLine("Sorry your cart is not empty, therefore you can't create a new cart from history");
            }
            else
            {
                Console.WriteLine("Select an order from history:");
                for (int i = 0; i < buyer.GetOrderHistory().Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Order with total price {buyer.GetOrderHistory()[i].TotalPrice}$");
                }

                int orderIndex;
                while (!int.TryParse(Console.ReadLine(), out orderIndex) || orderIndex < 1 || orderIndex > buyer.GetOrderHistory().Count)
                {
                    Console.WriteLine("Invalid input. Please enter a valid order number.");
                }

                buyer.cart = new Cart(buyer.GetOrderHistory()[orderIndex - 1].Products);
                Console.WriteLine("New cart created from selected order.");
            }
        }
    }
}
