namespace ProductData.Models
{
    public class Products
    {
        public string pid { get; set; }
        public string name { get; set; }   
        public decimal price { get; set; }
        public int qty { get; set; }
        public int reserve { get; set; }
    }

    public class UpdateStatus
    {
        public string pid { get; set; }
        public string status { get; set; }

    }
}
