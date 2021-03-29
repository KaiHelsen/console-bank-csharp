using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace console_bank_c_
{
    public class BankController
    {
        //declare const values for easy navigation and menu usage.
        private const string NewUser = "NewUser";
        private const string MakeNewAccount = "AccessAccount";
        private const string Quit = "Quit";
        private const string Deposit = "MakeDeposit";
        private const string Withdraw = "MakeWithdrawal";

        //declare session storage variables
        private Client _currentUser;
        private BankAccount _currentAccount;

        //utility bool
        private bool _isRunning;

        //general storage; ideally this would be data stored in a more permanent format, but I guess we'll come to that eventually.
        private List<Client> _clients = new List<Client>();
        private List<BankAccount> _bankAccounts = new List<BankAccount>();

        //declare functions
        /**
         * Bank Controller constructor
         */
        public BankController()
        {
            //an empty constructor. ew!
        }

        /**
         * start session. Sets _isRunning to true to maintain program loop and launches main menu.
         */
        public void StartSession()
        {
            _isRunning = true;
            MainMenu();
        }

        /**
         * end session function. Logs out any user and any active bank accounts, and sets isRunning to false, ending the loop.
         */
        public void EndSession()
        {
            LogOut();
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
                    MakeNewAccount + " : go to existing bank account to make a deposit or do a withdrawal.");
                if (_currentAccount != null && _currentUser != null)
                {
                    Console.WriteLine(Deposit + " : make a deposit");
                    Console.WriteLine(Withdraw + " : make a withdrawal");
                }
                else
                {
                    Console.WriteLine("currently cannot make deposits or withdrawals.");
                };

                Console.WriteLine(Quit + " / X / Q  : end session and go back to your miserable life.");
                string userResponse = Console.ReadLine();

                switch (userResponse)
                {
                    case (NewUser):
                        CreateNewClient();
                        break;
                    case (MakeNewAccount):
                        CreateNewBankAccount();
                        break;
                    case Deposit:
                        MakeDeposit();
                        break;
                    case Withdraw:
                        MakeWithdrawal();
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
                _currentUser = newUser;

                Console.WriteLine("new user created! Welcome to our bank, " + userName + "!");
                return;
            }
        }

        private void CreateNewBankAccount()
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
                _currentAccount.MakeDeposit(initialDeposit);
                _bankAccounts.Add(_currentAccount);
            }
            else
            {
                //get and store default console foreground color
                //then, set console foreground color to red
                //after writing the line, set foreground color back to its default.
                ConsoleColor defaultConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("cheapskate.");
                Console.WriteLine("No new account made, since you couldn't meet the base deposit amount.");
                Console.ForegroundColor = defaultConsoleColor;
                return;
            }
        }

        private void MakeDeposit()
        {
            if (_currentUser == null && _currentAccount == null)
            {
                Console.WriteLine("No user or account currently active. returning to main menu...");
                return;
            }
            Console.WriteLine("How much would you like to deposit?");
            float deposit = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine("Adding " + deposit + " to your account...");
            _currentAccount.MakeDeposit(deposit);
            Console.WriteLine("Transaction successful!");
            Console.WriteLine("your account now has a total balance of " + _currentAccount.getBalance());


        }

        private void MakeWithdrawal()
        {
            if (_currentUser == null && _currentAccount == null)
            {
                Console.WriteLine("No user or account currently active. returning to main menu...");
                return;
            }

            Console.WriteLine("How much would you like to withdraw?");
            float withdrawal = Convert.ToSingle(Console.ReadLine());
            if (withdrawal > _currentAccount.getBalance())
            {
                Console.WriteLine("Cannot withdraw more than the balance of your account!");
                return;
            }

            Console.WriteLine("Withrawing...");
            _currentAccount.MakeWithdrawal(withdrawal);
            Console.WriteLine("Amount successfully Withdrawn!");
            Console.WriteLine("your account now has a total balance of " + _currentAccount.getBalance());

        }
        private void LogOut()
        {
            //unset current account and current user, resetting session to its default state.
            _currentAccount = null;
            _currentUser = null;
        }
    }
}