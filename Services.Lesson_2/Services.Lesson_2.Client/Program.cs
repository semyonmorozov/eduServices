using System;
using Services.Lesson_2.Client.IWarehouseService;

namespace Services.Lesson_2.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new WarehouseServiceClient();

            var all1 = client.GetAll();
            foreach (var product in all1)
                Print(product);

            client.Add(new Product { Id = 2, Name = "Name_2" });

            var all2 = client.GetAll();
            foreach (var product in all2)
                Print(product);

            Console.ReadLine();
        }

        private static void Print(Product product)
        {
            Console.WriteLine($"{product.Id,3} {product.Name,8}");
        }
    }
}