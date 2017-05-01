using LibraryClient.LibraryServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClient
{
    class Program
    {
        private static LibraryServiceClient client;
        static void Main(string[] args)
        {            
            client = new LibraryServiceClient(new InstanceContext(new LibraryServiceCallback()));            

            client.Authorize("user");

            //Пытаемся получить информацию о книгах по автору, книг которого нет в библиотека
            try { client.GetBooksInfoByAuthor("Джон Рональд Руэл Толкин").ToList().ForEach(b => PrintBookInfo(b)); }
            catch(FaultException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Пытаемся получить книгу с id=666, которой нет в библиотеке
            try { client.GetBook(666); }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Пытаемся получить уже зарезервированную книгу
            try { client.GetBook(6); }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //за пользователем уже зарегестрирована книга id=6 от 2015 года
            //в результате работы следующего метода должен вызваться CallBack
            client.Exchange();

            Console.ReadKey();
            client.Exit();
            client.Close();


        }
        static void PrintBookInfo(Book book)
        {
            Console.WriteLine("Id: " + book.Id + "; Назавание: " + book.Name + "; Автор: " +book.Author+ "; Год: " + book.Date.Date.Year+ "; Тип: " + book.BookType);
        }

        public class LibraryServiceCallback : ILibraryServiceCallback
        {
            public void OnCallback(int[] overdueBooks)
            {
                foreach(int id in overdueBooks)
                {
                    client.ExtendReservation(id);
                    Console.WriteLine($"Резервация книги {id} продлена");
                }
            }
            
        }
    }
}
