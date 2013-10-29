namespace EventFabricExample_Products
{
    public class Product
    {
        private string product { get; set; }
        private int format { get; set; }
        private double price { get; set; }
        private bool delivered { get; set; }

        public Product(string product, int format, double price, bool delivered)
        {
            this.product = product;
            this.format = format;
            this.price = price;
            this.delivered = delivered;
        }

        public Product()
        {

        }
    }
}