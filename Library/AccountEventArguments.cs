using System;
namespace Library
{
    public delegate void AccountHandler(object sender, AccountEventArguments e);

    public class AccountEventArguments : EventArgs
    {
        public string Message { get; private set; }

        public string Login { get; private set; }

        public int Sum { get; private set; }

        public AccountEventArguments(string Message, string Login, int Sum)
        {
            this.Login = Login;
            this.Message = Message;
            this.Sum = Sum;
        }
        public AccountEventArguments(string Message, string Login)
        {
            this.Login = Login;
            this.Message = Message;
        }
    }
}
