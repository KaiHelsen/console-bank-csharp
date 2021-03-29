using System;

namespace console_bank_c_
{
    public class Client
    {
        private int id { get; }
        public string name { get; }
        private DateTime dateJoined { get; }

        public Client(string name)
        {
            this.name = name;
            //set date joined to current time
            dateJoined = DateTime.Now;
            //pick random id value
            id = new Random().Next(0,10000);
        }
    }
}