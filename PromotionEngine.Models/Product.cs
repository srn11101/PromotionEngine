
using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public class Product
    {
        public Product()
        {

        }
        public string Id { get; set; }
        public decimal Price { get; set; }

        public Product(string id,decimal price)
        {
            this.Id = id;
            this.Price = price;
            //switch (id)
            //{
            //    case "A":
            //        this.Price = 50m;

            //        break;
            //    case "B":
            //        this.Price = 30m;

            //        break;
            //    case "C":
            //        this.Price = 20m;

            //        break;
            //    case "D":
            //        this.Price = 15m;
            //        break;
            //}
        }
    }
}
