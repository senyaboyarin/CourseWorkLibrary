using System;

namespace Library
{
    public class Book : IComparable<Book>, IEquatable<Book>
    {
        public string Author { get; set; }

        public string Name { get; set; }

        public string Theme { get; set; }

        public int BookID { get; set; }

        public Book()
        {

        }

        public Book(string Name, string Author, string Theme)
        {
            this.Name = Name;
            this.Author = Author;
            this.Theme = Theme;
        }

        public override int GetHashCode()
        {
            return BookID;
        }
        public override string ToString()
        {
            return "Id: " + BookID + "\tName: " + Name + "\tAuthor: " + Author + "\tTheme: " + Theme;
        }
        public bool Equals(Book other)
        {
            if (other == null) return false;
            return (BookID.Equals(other.BookID));
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Book ObjAsBook = obj as Book;
            if (ObjAsBook == null) return false;
            else return Equals(ObjAsBook);
        }
        public int CompareTo(Book compareBook)
        {
            if (compareBook == null)
                return 1;

            else
                return this.BookID.CompareTo(compareBook.BookID);
        }

    }
}