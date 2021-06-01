namespace Library
{
    public class AdminAccount : Account
    {
        public AdminAccount(string login) : base(login) { AccountType = 1; }
        protected internal override void OpenAccount()
        {
            base.IfOpened(new AccountEventArguments($"A new admin account has been created! Account Login: {Login} Account Id: {UserID}", Login));
        }
        protected internal override void LogIn()
        {
            IfReturned(new AccountEventArguments($"You are logged in admin account with login \"{Login}\" ", Login));
        }
        protected internal override void LogOut()
        {
            IfReturned(new AccountEventArguments($"You are logged out from admin account with login \"{Login}\" ", Login));
        }
    }
}