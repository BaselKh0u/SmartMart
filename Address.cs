using System;

namespace SmartMart
#pragma warning disable CS0659
{
    internal class Address
    {
        private string _street;
        private int _buildingNumber;
        private string _city;
        private string _country;

        public string Street
        {
            get { return _street; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid input, street cannot be empty. Please try again.");
                }
                else if (!Validation.IsLetter(value))
                {
                    throw new ArgumentException("Invalid input, street cannot contain digits. Please try again.");
                }
                _street = value;
            }
        }
        public int BuildingNumber
        {
            get { return _buildingNumber; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Error, building number must be a positive number. Please re-enter a valid building number.");
                }
                _buildingNumber = value;
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid input, city cannot be empty. Please try again.");
                }
                else if (!Validation.IsLetter(value))
                {
                    throw new ArgumentException("Invalid input, city cannot contain digits. Please try again.");
                }
                _city = value;
            }
        }
        public string Country
        {
            get { return _country; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid input, country cannot be empty. Please try again.");
                }
                else if (!Validation.IsLetter(value))
                {
                    throw new ArgumentException("Invalid input, country cannot contain digits. Please try again.");
                }
                _country = value;
            }
        }
        public Address() { }
        public Address(string street, int buildingNumber, string city, string country)
        {
            this.Street = street;
            this.BuildingNumber = buildingNumber;
            this.City = city;
            this.Country = country;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Address other = (Address)obj;
            return Street == other.Street && BuildingNumber == other.BuildingNumber && City == other.City && Country == other.Country;
        }
        public override string ToString()
        {
            return $"Street: {Street}, Building Number: {BuildingNumber}, City: {City}, Country: {Country}";
        }
    }
}
