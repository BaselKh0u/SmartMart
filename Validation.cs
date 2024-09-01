namespace SmartMart
{
    internal class Validation
    {
        public static bool IsLetter(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
