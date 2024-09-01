namespace SmartMart
{
    internal class SpecialProduct : Product
    {
        public SpecialProduct(string name, double price, ProductCategory category)
            : base(name, price + 5, category, true) { }

        public override string ToString()
        {
            return base.ToString() + " (Special Packaging)";
        }
    }
}
