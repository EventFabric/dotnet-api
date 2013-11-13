using System;

namespace EventFabricExample_Products
{
    public class Product
    {
        public string product { get; set; }
        public int count { get; set; }
        public double price { get; set; }
        public bool delivered { get; set; }

        public Product(string product, int count, double price, bool delivered)
        {
            this.product = product;
            this.count = count;
            this.price = price;
            this.delivered = delivered;
        }

        public Product()
        {

        }

        public override String ToString()
        {
            return String.Format("Product: {0}. Count: {1}. Price: {1}. Delivered: {1}", product, count, price, delivered);
        }
    }
}