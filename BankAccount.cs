using System;

namespace console_bank_c_
{
    public enum Types
    {
        Savings,
        Salary,
        Current,
        Fixed,
        Recurring
    }

    public class BankAccount
    {
        //define type enum

        //declare variables

        private Client _client;
        private float _balance;
        private Types _type;


        public BankAccount(Client client, Types type )
        {
            this._client = client;
            this._balance = 0f;
            this._type = type;
        }

        public void MakeDeposit(float deposit)
        {
            _balance += deposit;
        }

        public bool MakeWithdrawal(float withdrawal)
        {
            if (withdrawal > this._balance) return false;

            this._balance -= withdrawal;
            return true;
        }
    }
}