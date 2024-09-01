using System;

namespace SmartMart
#pragma warning disable CS0659
{
    internal class User
    {
        private string _username;
        private string _password;
        private Address _residence;
        public string Username
        {
            get { return _username; }
            set
            {
                if ((string.IsNullOrWhiteSpace(value))||(value.Length==0))
                {
                    throw new ArgumentException("Invalid input, username cannot be empty. Please try again.");
                }
                else if (!Validation.IsLetter(value))
                {
                    throw new ArgumentException("Invalid input, username cannot contain digits. Please try again.");
                }
                _username = value;
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid input, password cannot be empty. Please try again.");
                }
                _password = value;
            }
        }

        public Address Residence
        {
            get { return _residence; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Invalid input, address cannot be empty. Please try again.");
                }
               _residence = value;
            }
        }
        public User() { }
        
       
        public User(string username, string password, Address residence)
        {
            this.Username = username;
            this.Password = password;
            this.Residence = residence;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            User other = (User)obj;
            return Username == other.Username && Password == other.Password && Residence.Equals(other.Residence);
        }
        public override string ToString()
        {
            return $"Username: {Username}, Password: {Password}, Address: {Residence}";
        }
      
    }
}
