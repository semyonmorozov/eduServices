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
        void AddBook(Book book);

        [OperationContract]
        Book GetBookInfoById(int id);

        [OperationContract]
        List<Book> GetBooksInfoByAuthor(string author);

        [OperationContract]
        Book GetBook(int id);

        [OperationContract]
        void ReturnBook(int id);
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

        private DateTime date;

        [DataMember]
        public DateTime Date
        { 
            get { return date; }

            set
            {
                date = value.Date;
            }
        }

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
