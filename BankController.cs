using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace console_bank_c_
{
    public class BankController
    {
        private const string NewUser = "NewAccount";
        private const string AccessAccount = "AccessAccount";
        private const string Quit = "Quit";

        private Client _currentUser;
        private BankAccount _currentAccount;

        private bool _isRunning;

        private List<Client> _clients = new List<Client>();
        private List<BankAccount> _bankAccounts = new List<BankAccount>();

        public BankController()
        {
            //an empty constructor. ew!
        }

        public void StartSession()
        {
            _isRunning = true;
            MainMenu();
        }

        public void EndSession()
        {
            _isRunning = false;
        }

        private void MainMenu()
        {
            Console.WriteLine("Welcome to Kai's miserable little bank.");
            while (_isRunning)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine(NewUser + " : create new bank account.");
                // Console.WriteLine(SwitchUser + " : log into or switch to a different user account");
                Console.WriteLine(
                    AccessAccount + " : go to existing bank account to make a deposit or do a withdrawal.");
                Console.WriteLine(Quit + " / X / Q  : end session and go back to your miserable life.");
                string userResponse = Console.ReadLine();

                switch (userResponse)
                {
                    case (NewUser):
                        Console.WriteLine("... moving to new account menu...");
                        CreateNewClient();
                        break;
                    case (AccessAccount):
                        Console.WriteLine("Please enter your name:");
                        CreateNewAccount();
                        break;
                    case (Quit):
                    case ("Q"):
                    case ("X"):
                        Console.WriteLine("Goodbye!");
                        EndSession();
                        break;
                    default:
                        Console.WriteLine("Sorry, I did not understand that");
                        break;
                }
            }
        }

        private void CreateNewClient()
        {
            Console.Clear();
            Console.WriteLine("Welcome new Client!");
            Console.WriteLine("Please enter your name:");
            string userName = Console.ReadLine();

            //the FindIndex function returns the index of the object that fits the criteria. Otherwise, it returns -1.
            //so, if it returns a value below 0, we know it found nothing.
            //and if it returns a value of 0 or above, we did find something. we can use this.
            if (_clients.FindIndex(client => client.name == userName) >= 0)
            {
                Console.WriteLine(
                    "wait, that user already exists. Either go back and use your existing account or try a different name.");
                Console.WriteLine("returning to main menu...");
                return;
            }
            else
            {
                Console.WriteLine("creating new user account...");

                Client newUser = new Client(userName);
                _clients.Add(newUser);

                Console.WriteLine("new user created! Welcome to our bank, " + userName + "!");
                return;
            }
        }

        private void CreateNewAccount()
        {
            if (_currentUser == null)
            {
                Console.WriteLine("No current user logged in.");
                Console.WriteLine("...Moving to new Account menu...");
                CreateNewClient();
                return;
            }

            Console.WriteLine("Welcome to the new account menu!");
            Console.WriteLine("Please enter your name:");
            string userName = Console.ReadLine();

            Console.WriteLine("what type of account would you like to make?");
            foreach (Types type in Enum.GetValues(typeof(Types)))
            {
                Console.WriteLine(type + " : " + type + " account.");
            }

            string userResponse = Console.ReadLine();

            Console.WriteLine(
                "in order to make a new account, you have to make a minimum deposit of 5.00. How much would you like to deposit?");

            int initialDeposit = Convert.ToInt32(Console.ReadLine());
            if (initialDeposit >= 5f)
            {
                Console.WriteLine("setting up new account...");
                Console.WriteLine("Adding " + initialDeposit + " to your new account...");
                _currentAccount = new BankAccount(_currentUser, Types.Savings);
            }
            else
            {
                //get and store default console foreground color
                //then, set console foreground color to red
                //after writing the line, set foreground color back to its default.
                ConsoleColor defaultConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("cheapskate.");
                Console.ForegroundColor = defaultConsoleColor;
                return
            }
        }

        private void LogOut()
    {
    //unset current account and current user, resetting session to its default state.
    _currentAccount = null;
    _currentUser = null;
    }
}

}