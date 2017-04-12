using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LibraryService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class LibraryService : ILibraryService
    {
        private static Dictionary<Book, bool> library = new Dictionary<Book, bool>();

        public void AddBook(Book book)
        {
            library.Add(book, true);
        }

        public Book GetBook(int id)
        {            
            Book registredBook = library.Where(b => b.Key.Id == id).Where(b=>b.Value==true).First().Key;
            library.Remove(registredBook);
            library.Add(registredBook, false);
            return registredBook;
        }

        public Book GetBookInfoById(int id)
        {
            return library.Where(b => b.Value == true).Where(b => b.Key.Id == id).First().Key;
        }

        public List<Book> GetBooksInfoByAuthor(string author)
        {
            return library.Where(b => b.Value == true).Where(b => b.Key.Author == author).Select(b => b.Key).ToList();
        }

        public void ReturnBook(int id)
        {
            Book registredBook = library.Where(b => b.Key.Id == id).First().Key;
            library.Remove(registredBook);
            library.Add(registredBook, true);
        }
    }
}
