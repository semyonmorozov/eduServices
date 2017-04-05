using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Services.Lesson_2.Service
{
    public class WarehouseService : IWarehouseService
    {
        private static List<Product> _products;

        public WarehouseService()
        {
            _products = _products ?? new List<Product> { new Product { Id = 1, Name = "Name_1" } };
        }

        public Product Get(int id)
        {
            Console.WriteLine("Get");
            return _products.First(i => i.Id == id);
        }

        public Product[] GetAll()
        {
            Console.WriteLine("GetAll");
            return _products.ToArray();
        }

        public void Add(Product product)
        {
            Console.WriteLine("Add");
            _products.Add(product);
        }
    }

    [ServiceContract]
    public interface IWarehouseService
    {
        [OperationContract]
        Product Get(int id);

        [OperationContract]
        Product[] GetAll();

        [OperationContract]
        void Add(Product product);
    }

    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}