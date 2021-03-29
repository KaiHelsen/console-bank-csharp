using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace console_bank_c_
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BankController controller = new BankController();
            controller.StartSession();
        }
    }
}