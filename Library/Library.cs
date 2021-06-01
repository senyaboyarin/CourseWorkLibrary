using System;
using System.Collections.Generic;
namespace Library
{

    public class Library<T> where T : Account
    {
        List<Book> Books = new List<Book>();
        static int BooksCounter = 0;
        T[] Accs;
        public Library()
        {

        }
        public void Open(AccountType AccountType, string Login,
            AccountHandler TookBookHandler, AccountHandler ReturnBookHandler,
            AccountHandler CloseAccountHandler, AccountHandler OpenAccountHandler,
            AccountHandler LoginAccountHandler, AccountHandler logoutAccountHandler, AccountHandler LookAccountHandler)
        {
            T NewAccount = null;

            switch (AccountType)
            {
                case AccountType.Admin:
                    NewAccount = new AdminAccount(Login) as T;
                    break;
                case AccountType.Common:
                    NewAccount = new CommonAccount(Login) as T;
                    break;
            }

            if (NewAccount == null)
                throw new Exception("Incorrect creation of account");
            if (Accs == null)
                Accs = new T[] { NewAccount };
            else
            {
                T[] TempAccounts = new T[Accs.Length + 1];
                for (int i = 0; i < Accs.Length; i++)
                    TempAccounts[i] = Accs[i];
                TempAccounts[TempAccounts.Length - 1] = NewAccount;
                Accs = TempAccounts;
            }
            NewAccount.Took += TookBookHandler;
            NewAccount.Returned += ReturnBookHandler;
            NewAccount.Closed += CloseAccountHandler;
            NewAccount.Opened += OpenAccountHandler;
            NewAccount.Logined += LoginAccountHandler;
            NewAccount.Looked += LookAccountHandler;
            NewAccount.Logouted += logoutAccountHandler;
            NewAccount.OpenAccount();
        }
        public void TakeBook(int ID)
        {
            Console.WriteLine("Enter the Id of the book you want to take:");
            int BookID = Convert.ToInt32(Console.ReadLine());
            if (Books.Exists(x => x.BookID == BookID))
            {
                Book temp = Books.Find(x => x.BookID == BookID);
                FindAccount(ID).TakeBook(BookID, temp);
                RemoveBook(BookID);
            }
            else
                throw new Exception("There is no book with such Id in our library");
        }
        public void ReturnBook(int ID)
        {
            Console.WriteLine("Enter the Id of the book you want to return:");
            int BookID = Convert.ToInt32(Console.ReadLine());
            Book temp = new Book();
            temp = FindAccount(ID).ReturnBook(BookID);
            if (temp != null)
                Books.Add(temp);
            Books.Sort(delegate (Book x, Book y)
            {
                return x.BookID.CompareTo(y.BookID);
            });
        }
        private void AccountBooks(int ID)
        {
            if (FindAccount(ID) == null)
                throw new Exception("The account does not exist");
            FindAccount(ID).LookAll();
        }
        private void DeleteAccount(int ID)
        {
            int index;

            if (FindAccount(ID, out index) == null)
                throw new Exception("The account does not exist");
            if (FindAccount(ID, out index).BookAmount == 0)
            {
                FindAccount(ID, out index).CloseAccount();
                if (Accs.Length <= 1)
                    Accs = null;
                else
                {
                    T[] tempAccounts = new T[Accs.Length - 1];
                    for (int i = 0, j = 0; i < Accs.Length; i++)
                    {
                        if (i != index)
                            tempAccounts[j++] = Accs[i];
                    }
                    Accs = tempAccounts;
                }
            }
            else
                throw new Exception("Account with Login \"" + FindAccount(ID, out index).Login + "\" and Id \"" + FindAccount(ID, out index).UserID + "\" cann`t leave until he gives all the Books back!.");
        }

        private T FindAccount(int ID)
        {
            for (int i = 0; i < Accs.Length; i++)
            {
                if (Accs[i].UserID == ID)
                    return Accs[i];
            }
            return null;
        }
        private T FindAccount(int ID, out int index)
        {
            for (int i = 0; i < Accs.Length; i++)
            {
                if (Accs[i].UserID == ID)
                {
                    index = i;
                    return Accs[i];
                }
            }
            index = -1;
            return null;
        }
        public void AddBook(string Name, string Author, string Theme)
        {
            if (Name == "" || Author == "" || Theme == "" || Name == null || Author == null || Theme == null)
                throw new Exception("You can not add a book where one of the parameters is empty!");
            Books.Add(new Book() { BookID = ++BooksCounter, Name = Name, Author = Author, Theme = Theme });
            Books.Sort(delegate (Book x, Book y)
            {
                return x.BookID.CompareTo(y.BookID);
            });
        }
        public void LookAllLibraryBooks()
        {
            if (Books.Count != 0)
            {
                Console.WriteLine("List of all books in the library:");
                foreach (Book book in Books)
                    Console.WriteLine(book);
            }
            else
                throw new Exception("There are no books in the library");
        }
        private void LookAllLibraryAccounts()
        {
            if (Account.GetUserIDCounter() != 0)
            {
                Console.WriteLine("List of all accounts in library:");
                for (int i = 0; i < Account.GetUserIDCounter(); i++)
                    Console.WriteLine("User Id: " + Accs[i].UserID + "\tLogin: " + Accs[i].Login + "\tAccount type: " + Accs[i].GetUserType() + "\tNumber of Books taken: " + Accs[i].BookAmount);
            }
            else
                throw new Exception("There are no accounts in the library ");
        }
        public void SearchBook()
        {
            Console.WriteLine("Indicate with what you want to search book:");
            Console.WriteLine("1. Search by Name 2. Search by Theme 3. Search by Author");
            int flag = Convert.ToInt32(Console.ReadLine());
            if (flag > 3 || flag < 1)
                throw new Exception("This item does not exist, try again");
            Console.WriteLine("Enter your keyword to search :");
            string KeyWord = Console.ReadLine();
            switch (flag)
            {
                case 1:
                    List<Book> ResultName = Books.FindAll(FindName);
                    if (ResultName.Count != 0)
                    {
                        Console.WriteLine($"Books where \"{KeyWord}\" is contained in the title:");
                        foreach (Book b in ResultName)
                            Console.WriteLine(b);
                    }
                    else
                        throw new Exception("There are no such books!");
                    bool FindName(Book book)
                    {
                        if (book.Name == KeyWord)
                            return true;
                        else
                            return false;
                    }
                    break;
                case 2:
                    List<Book> resultTheme = Books.FindAll(FindTheme);
                    if (resultTheme.Count != 0)
                    {
                        Console.WriteLine($"Books with the theme \"{KeyWord}\":");
                        foreach (Book b in resultTheme)
                            Console.WriteLine(b);
                    }
                    else
                        throw new Exception("There are no such books!");
                    bool FindTheme(Book book)
                    {

                        if (book.Theme == KeyWord)
                            return true;
                        else
                            return false;
                    }
                    break;
                case 3:
                    List<Book> ResultAuthor = Books.FindAll(FindAuthor);
                    if (ResultAuthor.Count != 0)
                    {
                        Console.WriteLine($"Books where the author has the name \"{KeyWord}\":");
                        foreach (Book b in ResultAuthor)
                            Console.WriteLine(b);
                    }
                    else
                        throw new Exception("There are no such books!");
                    bool FindAuthor(Book book)
                    {
                        if (book.Author == KeyWord)
                            return true;
                        else
                            return false;
                    }
                    break;

            }
        }
        private void RemoveBook(int BookID)
        {
            Book temp = Books.Find(x => x.BookID == BookID);
            if (Books.Contains(new Book { BookID = BookID }))
            {
                Books.Remove(temp);
                Books.Sort(delegate (Book x, Book y)
                {
                    return x.BookID.CompareTo(y.BookID);
                });
            }
            else
                throw new Exception("You can not delete a book that is not in the library");
        }
        public void LoginAccount(int IDUser)
        {
            Account account = FindAccount(IDUser);
            if (account == null)
                throw new Exception("The account does not exist");
            FindAccount(IDUser).LogIn();
            switch (account.AccountType)
            {
                case 1:
                    AdminAccountMenu(IDUser);
                    break;
                case 2:
                    CommonAccountMenu(IDUser);
                    break;
            }
        }
        private void AdminAccountMenu(int IDUser)
        {
            bool alive = true;
            while (alive)
            {
                Console.WriteLine("\n1. Take a book \t   2. Return a book \t   3. View account Books   4. Book search\n5. Add a book      6. Remove a book \t   7.  View a list of Books in our library\t   8. Account List\n9. Delete account");
                Console.WriteLine("10. Log out");
                Console.WriteLine("\nEnter item number:");
                try
                {
                    int flag = Convert.ToInt32(Console.ReadLine());
                    switch (flag)
                    {
                        case 1:
                            TakeBook(IDUser);
                            break;
                        case 2:
                            ReturnBook(IDUser);
                            break;
                        case 3:
                            Console.WriteLine("Enter the account ID on which you want to see logbook:");
                            int id3 = Convert.ToInt32(Console.ReadLine());
                            AccountBooks(id3);
                            break;
                        case 4:
                            SearchBook();
                            break;
                        case 5:
                            Console.WriteLine("Enter the Author Name and the Theme of the book you want to add through enter:");
                            Console.WriteLine("Name: ");
                            string Name = Console.ReadLine();
                            Console.WriteLine("Author: ");
                            string Author = Console.ReadLine();
                            Console.WriteLine("Theme: ");
                            string Theme = Console.ReadLine();
                            AddBook(Name, Author, Theme);
                            break;
                        case 6:
                            Console.WriteLine("Enter the book ID you want to delete:");
                            int id5 = Convert.ToInt32(Console.ReadLine());
                            RemoveBook(id5);
                            break;
                        case 7:
                            LookAllLibraryBooks();
                            break;
                        case 8:
                            LookAllLibraryAccounts();
                            break;
                        case 9:
                            Console.WriteLine("Enter the ID of the account to be closed:");
                            int id6 = Convert.ToInt32(Console.ReadLine());
                            DeleteAccount(id6);
                            if (IDUser == id6)
                            {
                                alive = false;
                                throw new Exception("Your account have been deleted, so you have been moved to the main menu");
                            }
                            break;
                        case 10:
                            FindAccount(IDUser).LogOut();
                            alive = false;
                            continue;
                        default:
                            throw new Exception("You have chosen a non-existent option, please try again");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private void CommonAccountMenu(int UserID)
        {
            bool alive = true;
            while (alive)
            {
                Console.WriteLine("\n1. Take a book \t   2. Return a book \t   3. View my account Books\n4. Book search \t   5.View a list of Books in our library  6.Delete my account ");
                Console.WriteLine("7. Log out");
                Console.WriteLine("\nEnter item number:");
                try
                {
                    int flag = Convert.ToInt32(Console.ReadLine());
                    switch (flag)
                    {
                        case 1:
                            TakeBook(UserID);
                            break;
                        case 2:
                            ReturnBook(UserID);
                            break;
                        case 3:
                            FindAccount(UserID).LookAll();
                            break;
                        case 4:
                            SearchBook();
                            break;
                        case 5:
                            LookAllLibraryBooks();
                            break;
                        case 6:
                            DeleteAccount(UserID);
                            alive = false;
                            throw new Exception("You deleted your own account, so you have been moved to the main menu");

                        case 7:
                            FindAccount(UserID).LogOut();
                            alive = false;
                            continue;
                        default:
                            throw new Exception("You have chosen a non-existent option, please try again");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}