using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Account
    {
        int accountNo;
        string accountName;
        string accountDesc;


        public Account(int accountNo, string accountName, string accountDesc)
        {
            AccountNo = accountNo;
            AccountName = accountName;
            AccountDesc = accountDesc;
        }


        public int AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }
 
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }
      

        public string AccountDesc
        {
            get { return accountDesc; }
            set { accountDesc = value; }
        }





    }
}
