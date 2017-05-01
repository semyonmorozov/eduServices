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
        private ILibraryCallback CallBack => OperationContext.Current.GetCallbackChannel<ILibraryCallback>();

        private static Dictionary<Book, Person> library = new Dictionary<Book, Person>();

        private static List<Person> persons = new List<Person>();

        private Person person;        

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
            },null);
            library.Add(new Book
            {
                Id = 2,
                Name = "Собака Баскервилей",
                Author = "Артур Конан Дойль",
                Date = DateTime.ParseExact("1902", "yyyy", null),
                BookType = "Книга"
            }, null);
            library.Add(new Book
            {
                Id = 3,
                Name = "Этюд в багровых тонах",
                Author = "Артур Конан Дойль",
                Date = DateTime.ParseExact("1887", "yyyy", null),
                BookType = "Книга"
            }, null);
            library.Add(new Book
            {
                Id = 4,
                Name = "Знак четырёх",
                Author = "Артур Конан Дойль",
                Date = DateTime.ParseExact("1890", "yyyy", null),
                BookType = "Книга"
            }, null);
            library.Add(new Book
            {
                Id = 5,
                Name = "Страна Чудес без тормозов и Конец Света",
                Author = "Харуки Мураками",
                Date = DateTime.ParseExact("1985", "yyyy", null),
                BookType = "Книга"
            }, null);
            Book book = new Book
            {
                Id = 6,
                Name = "Охота на овец",
                Author = "Харуки Мураками",
                Date = DateTime.ParseExact("1982", "yyyy", null),
                BookType = "Книга"
            };
            Person person = new Person("user");
            persons.Add(person);
            library.Add(book, person);
            person.AddBook(book, DateTime.ParseExact("2015", "yyyy", null));
        }    

        public void Authorize(string login)
        {
            person = new Person(login);
            if (!persons.Contains(person))
                persons.Add(person);
            else person = persons.Where(p => p.GetName() == login).First();

            getedBooks = new List<Book>();
            returnedBooks = new List<Book>();
        }

        public void ExtendReservation(int id) //Обновляет дату резервации
        {
            Book book = person.GetBooks().Keys.Where(b => b.Id == id).First();
            person.RemoveBook(book);
            person.AddBook(book);
        }

        public bool Exchange()
        {
            /*  Если клиент вызвал этот метод и количество книг соответствует ограничению, 
            то обмен считаем совершенным и обнуляем массивы содержащие книги, которые клиен хотел отдать/взять  */

            if (person.GetBooks().Count+getedBooks.Count-returnedBooks.Count > 5) return false;

            foreach (Book book in getedBooks)
            {
                if (library.Where(b => b.Key == book).First().Value == null)
                {
                    person.AddBook(book);
                    library.Remove(book);
                    library.Add(book, person);
                }
                else throw new FaultException($"Книга {book.Id} уже зарезервирована.");
            }

            foreach (Book book in returnedBooks)
            {
                person.RemoveBook(book);
                library.Remove(book);
                library.Add(book, null);
            }

            getedBooks = new List<Book>();
            returnedBooks = new List<Book>();

            int[] overdueBooks = person.GetBooks()
                .Where(b => (DateTime.Now - b.Value).Days > 30)
                .Select(b => b.Key.Id)
                .ToArray();
            if (overdueBooks.Any())
                CallBack.OnCallback(overdueBooks);

            return true;
        }

        public Book GetBook(int id)
        {
            KeyValuePair<Book, Person> registredBook;
            try { registredBook = library.Where(b => b.Key.Id == id).First(); }
            catch (InvalidOperationException)
            {
                throw new FaultException("Книги с таким id не найдено.");
            }            
            Book book = registredBook.Key;
            if (returnedBooks.Contains(book)) returnedBooks.Remove(book);
            else
            {
                if (registredBook.Value != null) throw new FaultException("Эта книга уже зарезервирована.");
                getedBooks.Add(book);
            }
            return book;
        }

        public void ReturnBook(int id)
        {
            Book registredBook = library.Where(b => b.Key.Id == id).First().Key;
            if (!person.GetBooks().Keys.Contains(registredBook))
                throw new FaultException("Эта книга зарезервирована за другим пользователем.");
            if (getedBooks.Contains(registredBook)) getedBooks.Remove(registredBook);
            else returnedBooks.Add(registredBook);
        }

        public Book GetBookInfoById(int id)
        {
            try { return library.Where(b => b.Key.Id == id).First().Key; }
            catch (InvalidOperationException)
            {
                throw new FaultException("Книги с таким id не найдено.");
            }
        }

        public List<Book> GetBooksInfoByAuthor(string author)
        {
            List<Book> books = library.Where(b => b.Key.Author == author).Select(b => b.Key).ToList();
            if (books.Count == 0) throw new FaultException("Книг с таким автором не найдено.");
            return books;
        }

        public void AddBook(Book book)
        {
            library.Add(book, null);
        }

        public void Exit()
        {
            //Завершение сеанса
        }
    }
}
