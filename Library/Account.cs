using System;
using System.Collections.Generic;

namespace Library
{
    public enum AccountType
    {
        Admin,
        Common
    }
    public abstract class Account
    {
        protected internal event AccountHandler Returned;
        protected internal event AccountHandler Took;
        protected internal event AccountHandler Opened;
        protected internal event AccountHandler Looked;
        protected internal event AccountHandler Closed;
        protected internal event AccountHandler Logined;
        protected internal event AccountHandler Logouted;
        static int userIDcounter = 0;
        static int userID = userIDcounter;
        List<Book> UserBooks = new List<Book>();
        public Account(string login)
        {
            Login = login;
            BookAmount = 0;
            UserID = ++userIDcounter;
            AccountType = 0;
        }
        public int BookAmount { get; private set; }
        public int AccountType { get; protected set; }
        public string Login { get; private set; }
        public int UserID { get; private set; }
        public static int GetUserIDCounter() { return userID; }
        public string GetUserType()
        {
            switch (AccountType)
            {
                case 1:
                    return "Admin";
                case 2:
                    return "Common";
                default:
                    return "NONE";
            }

        }
        private void DoEvent(AccountEventArguments e, AccountHandler handler)
        {
            if (e != null)
            {
                handler?.Invoke(this, e);
            }

        }
        protected virtual void IfReturned(AccountEventArguments e)
        {
            DoEvent(e, Returned);
        }
        protected virtual void IfTook(AccountEventArguments e)
        {
            DoEvent(e, Took);
        }
        protected virtual void IfOpened(AccountEventArguments e)
        {
            DoEvent(e, Opened);
        }
        protected virtual void IfLook(AccountEventArguments e)
        {
            DoEvent(e, Looked);
        }
        protected virtual void IfClosed(AccountEventArguments e)
        {
            DoEvent(e, Closed);
            userID--;
        }
        protected virtual void IfLogined(AccountEventArguments e)
        {
            DoEvent(e, Logined);
        }
        protected virtual void IfLogouted(AccountEventArguments e)
        {
            DoEvent(e, Logouted);
        }
        public virtual void TakeBook(int BookID, Book book)
        {
            BookAmount += 1;
            IfTook(new AccountEventArguments($"The account with login \"{Login}\" took the book with id \"{BookID}\"", Login, BookID));
            UserBooks.Add(book);
            UserBooks.Sort(delegate (Book x, Book y)
            {
                return x.BookID.CompareTo(y.BookID);
            });
        }
        public virtual Book ReturnBook(int BookID)
        {
            Book temp = UserBooks.Find(x => x.BookID == BookID);
            if (UserBooks.Contains(new Book { BookID = BookID }))
            {
                BookAmount -= 1;
                UserBooks.Remove(new Book() { BookID = BookID });
                IfReturned(new AccountEventArguments($"The account with login \"{Login}\" return the book with id \"{BookID}\"", Login, BookID));
            }
            else
                throw new Exception("The account with login \"" + Login + "\" didn`t take the book with id \"" + BookID + "\"");
            return temp;
        }
        protected internal virtual void OpenAccount()
        {
            IfOpened(new AccountEventArguments($"A new account has been created! User login: {Login} User id: {UserID}", Login));
        }
        public virtual void LookAll()
        {
            if (UserBooks.Count != 0)
            {
                IfLook(new AccountEventArguments($"List of all Books taken users with User with login \"{Login}\":", Login));
                foreach (Book book in UserBooks)
                    Console.WriteLine(book);
            }
            else
                IfLook(new AccountEventArguments($"Account with login \"{Login}\" hasn't taken any Books yet, so list is empty.", Login));
        }
        protected internal virtual void CloseAccount()
        {
            IfClosed(new AccountEventArguments($"Account with login \"{Login}\" and Id \"{UserID}\" is closed.", Login));
        }
        protected internal virtual void LogIn()
        {
            IfLogined(new AccountEventArguments($"You are logged in account with login \"{Login}\" ", Login));
        }
        protected internal virtual void LogOut()
        {
            IfLogouted(new AccountEventArguments($"You are logged out from account with login \"{Login}\" ", Login));
        }
    }
}
