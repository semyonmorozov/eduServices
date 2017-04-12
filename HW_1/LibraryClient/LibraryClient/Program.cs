using LibraryClient.LibraryServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClient
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryServiceClient client = new LibraryServiceClient();            

            client.AddBook(new Book { Id = 1, Name = "Приключения Шерлока Холмса (сборник)",
                Author = "Артур Конан Дойль", Date = DateTime.ParseExact("1892", "yyyy", null), BookType = "Книга" });
            client.AddBook(new Book { Id = 2, Name = "Собака Баскервилей",
                Author = "Артур Конан Дойль", Date = DateTime.ParseExact("1902", "yyyy", null), BookType = "Книга" });
            client.AddBook(new Book { Id = 3, Name = "Этюд в багровых тонах",
                Author = "Артур Конан Дойль", Date = DateTime.ParseExact("1887", "yyyy", null), BookType = "Книга" });
            client.AddBook(new Book { Id = 4, Name = "Знак четырёх",
                Author = "Артур Конан Дойль", Date = DateTime.ParseExact("1890", "yyyy", null), BookType = "Книга" });

            client.GetBooksInfoByAuthor("Артур Конан Дойль").ToList().ForEach(b => PrintBookInfo(b));

            client.GetBook(3);
            Console.WriteLine("Забрал книгу с Id=3");
            client.GetBooksInfoByAuthor("Артур Конан Дойль").ToList().ForEach(b => PrintBookInfo(b));

            client.GetBook(1);
            client.ReturnBook(3);
            Console.WriteLine("Забрал книгу с Id=1 и вернул с Id=3");
            client.GetBooksInfoByAuthor("Артур Конан Дойль").ToList().ForEach(b => PrintBookInfo(b));

            Console.ReadKey();
            client.Close();
        }
        static void PrintBookInfo(Book book)
        {
            Console.WriteLine("Id: " + book.Id + " Назавание: " + book.Name + " Автор: " +book.Author+ " Год: " + book.Date.Date.Year+ " Тип: " + book.BookType);
        }
    }
}
