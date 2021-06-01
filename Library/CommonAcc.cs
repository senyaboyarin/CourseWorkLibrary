namespace Library
{
    public class CommonAccount : Account
    {
        public CommonAccount(string login) : base(login) { AccountType = 2; }
        protected internal override void OpenAccount()
        {
            base.IfOpened(new AccountEventArguments($"New common account has been just created! Account Id: {UserID} Account Login: {Login} ", Login));
        }
        public override void TakeBook(int BookID, Book temp)
        {
            if (BookAmount < 10)
                base.TakeBook(BookID, temp);
            else
                base.IfTook(new AccountEventArguments($"A common account with login \"{Login}\" has already taken 10 Books!", Login));
        }
        protected internal override void LogIn()
        {
            IfReturned(new AccountEventArguments($"You are logged in common account with login \"{Login}\" ", Login));
        }
        protected internal override void LogOut()
        {
            IfReturned(new AccountEventArguments($"You are logged out from common account with login \"{Login}\" ", Login));
        }
    }
}

