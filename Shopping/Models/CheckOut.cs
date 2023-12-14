namespace Shopping.Models
{
    public class CheckOut
    {
        public int CustomerID { get; set; }
        
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public string ActivationCode { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string[]? ActivationCodeList { get; set; }




    }
}
