using System;
using System.ServiceModel;

namespace Services.Lesson_2.Service
{
    public class Program
    {
        public static void Main()
        {
            var host = new ServiceHost(typeof(WarehouseService), new Uri("http://localhost:8886/wh"));

            host.Open();

            Console.WriteLine("Press enter...");
            Console.ReadLine();

            host.Close();
        }
    }
}