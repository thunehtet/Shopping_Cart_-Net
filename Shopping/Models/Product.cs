namespace Shopping.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Image { get; set; }

        public int ReviewCount { get; set; }

        public int CountClick { get; set; }

        public double AverageCount
        {
            get
            {
                if (CountClick == 0)
                {
                    return 0;
                }

                return (double)ReviewCount / CountClick;
            }
        }
    }
}
