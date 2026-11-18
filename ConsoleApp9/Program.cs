using System.Xml.Linq;

namespace LinqToXml
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load("assets/products.xml");

            var products = doc?.Root?
                .Elements("Product")
                .Select(p => new Product
                {
                    Id = (int)p.Element("Id"),
                    Name = (string)p.Element("Name"),
                    Category = (string)p.Element("Category"),
                    Price = (decimal)p.Element("Price"),
                    Stock = (int)p.Element("Stock")
                })
                .ToList();

            var groupedProducts = products.GroupBy(p => p.Category);

            foreach (var group in groupedProducts)
            {
                Console.WriteLine($"Категорія: {group.Key}");
                foreach (var product in group)
                {
                    Console.WriteLine($"  - {product.Name} | Ціна: {product.Price} | Залишок: {product.Stock}");
                }
            }
        }
    }
}