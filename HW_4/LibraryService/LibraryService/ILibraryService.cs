using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LibraryService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    
    [ServiceContract]
    public interface ILibraryService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/books", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Book AddBook(Book book);

        [OperationContract]
        [WebGet(UriTemplate = "/book/{id}", ResponseFormat = WebMessageFormat.Json)]
        Book GetBookById(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/books/{author}", ResponseFormat = WebMessageFormat.Json)]
        List<Book> GetBooksInfoByAuthor(string author);

        [OperationContract]
        [WebGet(UriTemplate = "/books", ResponseFormat = WebMessageFormat.Json)]
        List<Book> GetAllBooks();   
    }
     
    [DataContract]
    public class Book
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public string BookType { get; set; }

        public override bool Equals(Object obj)
        {
            // If parameter is null return false:
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Book book = obj as Book;
            if (book == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Id == book.Id);
        }

        public bool Equals(Book book)
        {
            // If parameter is null return false:
            if (book == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Id == book.Id);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
