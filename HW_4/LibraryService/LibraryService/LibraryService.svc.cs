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

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class LibraryService : ILibraryService
    {
        private static List<Book> library = new List<Book>();
        

        static LibraryService()
        {
            library.Add(new Book
            {
                Id = 1,
                Name = "Приключения Шерлока Холмса (сборник)",
                Author = "Arthur Conan Doyle",
                Year = 1892,
                BookType = "Книга"
            });
            library.Add(new Book
            {
                Id = 2,
                Name = "Собака Баскервилей",
                Author = "Arthur Conan Doyle",
                Year = 1902,
                BookType = "Книга"
            });
            library.Add(new Book
            {
                Id = 3,
                Name = "Этюд в багровых тонах",
                Author = "Arthur Conan Doyle",
                Year = 1887,
                BookType = "Книга"
            });
            library.Add(new Book
            {
                Id = 4,
                Name = "Знак четырёх",
                Author = "Arthur Conan Doyle",
                Year = 1890,
                BookType = "Книга"
            });
            library.Add(new Book
            {
                Id = 5,
                Name = "Страна Чудес без тормозов и Конец Света",
                Author = "Haruki Murakami",
                Year = 1985,
                BookType = "Книга"
            });
            library.Add(new Book
            {
                Id = 6,
                Name = "Охота на овец",
                Author = "Haruki Murakami",
                Year = 1982,
                BookType = "Книга"
            });
            
        }

        public Book AddBook(Book book)
        {
            if (!library.Contains(book))
                library.Add(book);
            else throw new FaultException("Книга с таким id уже есть");
            return book;
        }

        public List<Book> GetAllBooks()
        {
            return library;
        }

        public Book GetBookById(string id)
        {
            return library.Find(x => x.Id == int.Parse(id));
        }

        public List<Book> GetBooksInfoByAuthor(string author)
        {
            return library.Where(x => x.Author == author).ToList();
        }
    }
}
