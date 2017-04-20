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

            //Console.WriteLine(client.SingUp("user", "password"));

           client.Authorize("user");

            Console.WriteLine("Исходное состояние библиотеки");
            client.GetBooksInfoByAuthor("Артур Конан Дойль").ToList().ForEach(b => PrintBookInfo(b));
            client.GetBooksInfoByAuthor("Харуки Мураками").ToList().ForEach(b => PrintBookInfo(b));
            Console.WriteLine("Press enter...");
            Console.ReadKey();

            client.GetBook(1);
            client.GetBook(2);

            //Проверка корректности взятия и возврата одной и той же книгив рамках одной сессии
            client.GetBook(4);
            client.ReturnBook(4);

            if (client.Exchange())
                Console.WriteLine("Забрал книги 1 и 2 и подтвердил обмен.");
            client.Exit();
            client.Close();

            client = new LibraryServiceClient();
            client.Authorize("user");
            
            Console.WriteLine("Пришли еще раз, состояние библиотеки такого:");
            client.GetBooksInfoByAuthor("Артур Конан Дойль").ToList().ForEach(b => PrintBookInfo(b));
            client.GetBooksInfoByAuthor("Харуки Мураками").ToList().ForEach(b => PrintBookInfo(b));
            Console.WriteLine("Press enter...");
            Console.ReadKey();

            client.ReturnBook(1);
            client.GetBook(6);
            if (client.Exchange())
                Console.WriteLine("Вернул книгу 1, забрал книгу 6 и подтвердил обмен.");
            client.Exit();
            client.Close();

            client = new LibraryServiceClient();
            client.Authorize("user");

            Console.WriteLine("Пришли еще раз, состояние библиотеки такого:");
            client.GetBooksInfoByAuthor("Артур Конан Дойль").ToList().ForEach(b => PrintBookInfo(b));
            client.GetBooksInfoByAuthor("Харуки Мураками").ToList().ForEach(b => PrintBookInfo(b));
            Console.WriteLine("Press enter...");
            Console.ReadKey();

            client.GetBook(1);
            client.GetBook(3);
            client.GetBook(4);
            client.GetBook(5);
            if (!client.Exchange()) //Выведется, если обмен не успешен
                Console.WriteLine("Попытался взять 6 книг, не получилось, ушел не подтвердив обмен");
            client.Exit();
            client.Close();

            client = new LibraryServiceClient();
            client.Authorize("user");

            Console.WriteLine("Пришли еще раз, состояние библиотеки такого:");
            client.GetBooksInfoByAuthor("Артур Конан Дойль").ToList().ForEach(b => PrintBookInfo(b));
            client.GetBooksInfoByAuthor("Харуки Мураками").ToList().ForEach(b => PrintBookInfo(b));
            Console.WriteLine("Press enter...");
            Console.ReadKey();

            client.Exit();
            client.Close();
        }
        static void PrintBookInfo(Book book)
        {
            Console.WriteLine("Id: " + book.Id + "; Назавание: " + book.Name + "; Автор: " +book.Author+ "; Год: " + book.Date.Date.Year+ "; Тип: " + book.BookType);
        }
    }
}
