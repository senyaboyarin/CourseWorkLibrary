using System;
using Library;

namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Library<Account> library = new Library<Account>();
            library.AddBook("xtr", "bbb", "od");
            library.AddBook("rts", "qwert", "cvk");
            library.AddBook("azx", "ghjh", "pd");
            library.AddBook("abo", "bbb", "od");
            library.AddBook("abo", "rtx", "cvk");
            library.AddBook("cte", "qwert", "od");
            library.AddBook("ppp", "hhjj", "ui");
            library.AddBook("pep", "hasj", "ui");
            library.AddBook("azx", "ghjrrh", "ppppd");
            library.AddBook("aebo", "bfdgfbb", "od");
            library.AddBook("aqx", "ghjh", "pd");
            library.AddBook("iop", "bbb", "id");

            bool alive = true;
            Console.WriteLine("Welcome to the Library");
            while (alive)
            {

                Console.WriteLine("\n1. Create an account \t   2. Login account\n3. Search a book  \t   4. View a list of books in our library");

                Console.WriteLine("5. Exit the program");

                Console.WriteLine("\nEnter number:");

                try
                {
                    int StartCommand = Convert.ToInt32(Console.ReadLine());
                    switch (StartCommand)
                    {
                        case 1:
                            OpenAccount(library);
                            break;
                        case 2:
                            LoginAccount(library);
                            break;
                        case 3:
                            Search(library);
                            break;
                        case 4:
                            LookAllBooks(library);
                            break;
                        case 5:
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
        private static void OpenAccount(Library<Account> library)
        {
            Console.WriteLine("Enter login:");
            string login = Console.ReadLine();
            if (login == "")
                throw new Exception("Login can not be empty");
            Console.WriteLine("Select an account type:");
            Console.WriteLine("1. Admin 2. Common");
            AccountType accountType;
            int type = Convert.ToInt32(Console.ReadLine());
            switch (type)
            {
                case 1:
                    accountType = AccountType.Admin;
                    break;
                case 2:
                    accountType = AccountType.Common;
                    break;
                default:
                    throw new Exception("You have chosen a non-existent option, please try again");
            }

            library.Open(accountType,
                login,
                TookBookHandler,
                ReturnBookHandler,
                CloseAccountHandler,
                OpenAccountHandler,
                LoginAccountHandler,
                LogoutAccountHandler,
                LookAccountHandler);
        }
        private static void Search(Library<Account> library)
        {
            library.SearchBook();
        }
        private static void LoginAccount(Library<Account> library)
        {
            Console.WriteLine("Enter the account ID you want to login to:");
            int idUser = Convert.ToInt32(Console.ReadLine());
            library.LoginAccount(idUser);
        }
        private static void LookAllBooks(Library<Account> library)
        {
            library.LookAllLibraryBooks();
        }
        private static void OpenAccountHandler(object sender, AccountEventArguments e)
        {
            Console.WriteLine(e.Message);
        }
        private static void LoginAccountHandler(object sender, AccountEventArguments e)
        {
            Console.WriteLine(e.Message);
        }
        private static void LogoutAccountHandler(object sender, AccountEventArguments e)
        {
            Console.WriteLine(e.Message);
        }
        private static void CloseAccountHandler(object sender, AccountEventArguments e)
        {
            Console.WriteLine(e.Message);
        }
        private static void LookAccountHandler(object sender, AccountEventArguments e)
        {
            Console.WriteLine(e.Message);
        }
        private static void TookBookHandler(object sender, AccountEventArguments e)
        {
            Console.WriteLine(e.Message);
        }
        private static void ReturnBookHandler(object sender, AccountEventArguments e)
        {
            Console.WriteLine(e.Message);
        }

    }
}