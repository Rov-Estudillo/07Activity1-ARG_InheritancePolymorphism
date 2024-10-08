using System;

namespace UserNamespace
{
    public class User
    {
        public string UserId { get; private set; }
        protected string UserPassword { get; private set; }

        public User(string id, string pass)
        {
            UserId = id;
            UserPassword = pass;
        }

        public virtual bool VerifyLogin(string id, string pass)
        {
            return string.Equals(UserId, id, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(UserPassword, pass);
        }

        public bool UpdatePassword(string currentPassword, string newPassword)
        {
            if (string.Equals(UserPassword, currentPassword))
            {
                UserPassword = newPassword;
                return true;
            }
            return false;
        }
    }

    public class Administrator : User
    {
        public Administrator(string userId, string password) : base(userId, password) { }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter User ID: ");
            string userID = Console.ReadLine();

            Console.Write("Enter Password: ");
            string userPass = Console.ReadLine();

            Console.Write("Enter Admin ID: ");
            string adUserID = Console.ReadLine();

            Console.Write("Enter Admin Password: ");
            string adUserPass = Console.ReadLine();
            User user = new User(userID, userPass);
            Administrator admin = new Administrator(adUserID, adUserPass);

            Console.WriteLine("User Login: " + user.VerifyLogin(userID, userPass));
            Console.WriteLine("Admin Login: " + admin.VerifyLogin(adUserID, adUserPass));

            Console.WriteLine("User Login (wrong password): " + user.VerifyLogin(userID, "wrongpassword"));
            Console.WriteLine("Admin Login (wrong username): " + admin.VerifyLogin("wrongadmin", adUserPass));

            Console.WriteLine("Update User Password: " + user.UpdatePassword(userPass, "newpassword"));
            Console.WriteLine("User Login (new password): " + user.VerifyLogin(userID, "newpassword"));

            Console.WriteLine("Update User Password (wrong current password): " + user.UpdatePassword("wrongpassword", "anothernewpassword"));
            Console.WriteLine("User Login (still using new password): " + user.VerifyLogin(userID, "newpassword"));
        }
    }
}
