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
        private static Dictionary<Book, bool> library = new Dictionary<Book, bool>();

        private static Dictionary<string,List<Book>> persons = new Dictionary<string, List<Book>>();

        private String person;        

        private List<Book> getedBooks;

        private List<Book> returnedBooks;

        static LibraryService()
        {
            library.Add(new Book
            {
                Id = 1,
                Name = "Приключения Шерлока Холмса (сборник)",
                Author = "Артур Конан Дойль",
                Date = DateTime.ParseExact("1892", "yyyy", null),
                BookType = "Книга"
            },true);
            library.Add(new Book
            {
                Id = 2,
                Name = "Собака Баскервилей",
                Author = "Артур Конан Дойль",
                Date = DateTime.ParseExact("1902", "yyyy", null),
                BookType = "Книга"
            }, true);
            library.Add(new Book
            {
                Id = 3,
                Name = "Этюд в багровых тонах",
                Author = "Артур Конан Дойль",
                Date = DateTime.ParseExact("1887", "yyyy", null),
                BookType = "Книга"
            }, true);
            library.Add(new Book
            {
                Id = 4,
                Name = "Знак четырёх",
                Author = "Артур Конан Дойль",
                Date = DateTime.ParseExact("1890", "yyyy", null),
                BookType = "Книга"
            }, true);
            library.Add(new Book
            {
                Id = 5,
                Name = "Страна Чудес без тормозов и Конец Света",
                Author = "Харуки Мураками",
                Date = DateTime.ParseExact("1985", "yyyy", null),
                BookType = "Книга"
            }, true);
            library.Add(new Book
            {
                Id = 6,
                Name = "Охота на овец",
                Author = "Харуки Мураками",
                Date = DateTime.ParseExact("1982", "yyyy", null),
                BookType = "Книга"
            }, true);
        }
       

        //public bool SingUp(string login, string pass)
        //{
        //    try
        //    {
        //        persons.Add(new Person { Login = login, Pass = GetMD5(pass) }, new List<Book>());
        //    }
        //    catch (ArgumentException)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        

        public void Authorize(string login)
        {
            person = login;
             if (!persons.Keys.Contains(person))
                 persons.Add(person, new List<Book>());

            getedBooks = new List<Book>();
            returnedBooks = new List<Book>();
        }

        public bool Exchange()
        {
            /*  Если клиент вызвал этот метод и количество книг соответствует ограничению, 
            то обмен считаем совершенным и обнуляем массивы содержащие книги, которые клиен хотел отдать/взять  */

            if (persons[person].Count+getedBooks.Count-returnedBooks.Count > 5) return false;

            foreach (Book book in getedBooks)            
                persons[person].Add(book);            

            foreach (Book book in returnedBooks)            
                persons[person].Remove(book);

            getedBooks = new List<Book>();
            returnedBooks = new List<Book>();
            return true;
        }

        public void Exit()
        {
            /* Если клиент покинул библиотеку не подтвердив обмен, то возвращаем состояние библиотеки к исходному */

            foreach (Book book in getedBooks)
            {                                
                library.Remove(book);
                library.Add(book, true);
            }

            foreach (Book book in returnedBooks)
            {
                library.Remove(book);
                library.Add(book, false);
            }
        }

        public void AddBook(Book book)
        {
            library.Add(book, true);
        }

        public Book GetBook(int id)
        {            
            Book registredBook = library.Where(b => b.Key.Id == id).Where(b=>b.Value==true).First().Key;
            library.Remove(registredBook);
            library.Add(registredBook, false);
            getedBooks.Add(registredBook);
            if (returnedBooks.Contains(registredBook)) returnedBooks.Remove(registredBook);
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
            if (getedBooks.Contains(registredBook)) getedBooks.Remove(registredBook);
            returnedBooks.Add(registredBook);
        }

        //public static byte[] GetMD5(string input)
        //{
        //    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        //    {
        //        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        //        return md5.ComputeHash(inputBytes);                
        //    }
        //}

    }
}
