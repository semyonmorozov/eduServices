using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryService
{
    public class Person
    {
        private string name;
        private Dictionary<Book, DateTime> books;

        public Person(string name)
        {
            this.name = name;
            books = new Dictionary<Book, DateTime>();
        }

        public string GetName()
        {
            return name;
        }

        public Dictionary<Book, DateTime> GetBooks()
        {
            return books;
        }

        public void AddBook(Book book)
        {
            books.Add(book,DateTime.Now);
        }

        public void AddBook(Book book, DateTime date)
        {
            books.Add(book, date);
        }

        public void RemoveBook(Book book)
        {
            books.Remove(book);
        }

        public override bool Equals(Object obj)
        {
            // If parameter is null return false:
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Person person = obj as Person;

            return Equals(person);
        }

        public bool Equals(Person person)
        {
            // If parameter is null return false:
            if (person == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (name.Equals(person.name));
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }
}