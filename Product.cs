using System;

namespace SmartMart
#pragma warning disable CS0659
{
    internal class Product
    {
        public enum ProductCategory
        {
            Children = 1,
            Electronics = 2,
            Office = 3,
            Clothing = 4
        }

        private string _name;
        private double _price;
        private int _serialNumber;
        private ProductCategory _category;
        private bool _specialPackaging;
        private double _packagingPrice;

        private static int nextSerialNumber = 1;

        public string Name 
        { 
            get { return _name; }
            set 
            {
                
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Invalid input, username cannot be empty. Please try again.");
                    }
                    else if (!Validation.IsLetter(value))
                    {
                    throw new ArgumentException("Invalid input, username cannot contain digits. Please try again.");
                        
                    }
                    
                  _name = value;
            }
        }
        public double Price
        {
            get { return _price; }
            set
            {
                if (!double.TryParse(value.ToString(), out _))
                {
                    throw new ArgumentException("Invalid input, price must be a number. Please try again.");
                }
                if (value <= 0)
                {
                    throw new ArgumentException("Error,  product price must be a positive number bigger than 0. Please re-enter a valid price.");
                }

                _price = value;
            }
        }
        public int SerialNumber
        {
            get { return _serialNumber; }
        }
        public ProductCategory Category
        {
            get { return _category; }
            set
            {

                if (!Enum.IsDefined(typeof(ProductCategory), value))
                {
                    throw new ArgumentException("Invalid input, please select a valid category.");

                }
                _category = value;
            }
        }
        public bool SpecialPackaging
        {
            get { return _specialPackaging; }
            set
            {

                if (!bool.TryParse(value.ToString(), out _))
                {
                    throw new ArgumentException("Invalid input, please enter 1 or 0 .");

                }
                _specialPackaging = value;
            }
        }
        public double PackagingPrice
        {
            get { return _packagingPrice; }
            set
            {
                if (!double.TryParse(value.ToString(), out _))
                {
                    throw new ArgumentException("Invalid input, packaging price must be a number. Please try again.");

                }
                if (value <= 0)
                {
                    throw new ArgumentException("Error, packaging price must be a positive number that is bigger than 0. Please re-enter a valid price.");
                }
                _packagingPrice = value;
            }
        }
        public Product() { }
        public Product(string name, double price, ProductCategory category, bool specialPackaging=false)
        {
            this._name = name;
            this._price = price;
            this._category = category;
            this._specialPackaging = specialPackaging;
            _packagingPrice = specialPackaging ? 5 : 0;
            _serialNumber = nextSerialNumber++;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Product other = (Product)obj;
            return Name == other.Name && Price == other.Price && Category == other.Category && SpecialPackaging == other.SpecialPackaging && PackagingPrice == other.PackagingPrice && SerialNumber == other.SerialNumber;
        }


        public override string ToString()
        {
            return $"\nProduct name: {Name}\nPrice: {Price}$\nSerial Number: {SerialNumber}\nCategory: {Category}\nSpecial Packaging: {SpecialPackaging}\nPackaging price: {PackagingPrice}$";
        }
     
    }
}
