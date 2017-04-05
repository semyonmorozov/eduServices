using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Services.Lesson_2.Service
{
    public class LibraryService : ILibraryService
    {
        private static Dictionary<Book,bool> library;        

        public void AddBook(Book book)
        {
            library.Add(book,true);
        }

        public Book GetBook(int id)
        {
            return library.Keys.Where(b => b.id == id).First();
        }

        public Book GetBookInfoById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetBooksInfoByAuthor(string name)
        {
            throw new NotImplementedException();
        }

        public Book ReturnBook(int id)
        {
            throw new NotImplementedException();
        }
    }


    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILibraryService
    {

        [OperationContract]
        void AddBook(Book book);
        
        [OperationContract]
        Book GetBookInfoById(int id);

        [OperationContract]
        List<Book> GetBooksInfoByAuthor(string name);

        [OperationContract]
        Book GetBook(int id);

        [OperationContract]
        Book ReturnBook(int id);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Book
    {      

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string author { get; set; }

        [DataMember]
        public DateTime year { get; set; }

        [DataMember]
        public string bookType { get; set; }
    }
}
