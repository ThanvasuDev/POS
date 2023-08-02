using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Member
    {

        private int userID;
        private string userName;
        private string password;
        private string name;
        private string tel;
        private string address;
        private string status;
        private int workRate;
        private string flagUse;
        private int workShift;


 
        public Member()
        {
            userID = 0;
            userName = "";
            status = "";
        }

        public Member(int userID, string userName, string status, int workRate, int workShift)
        {
            UserID = userID;
            UserName = userName;
            Status = status;
            WorkRate = workRate;
            WorkShift = workShift;
        }


        public Member(int userID, string userName, string password, string name, string tel, string address, string status, int workRate, string flagUse, int workShift)
        {
            UserID = userID;
            UserName = userName;
            Password = password;
            Name = name;
            Tel = tel;
            Address = address;
            Status = status;
            WorkRate = workRate;
            FlagUse = flagUse;
            WorkShift = workShift;
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
         
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }


        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }


        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }


        public int WorkRate
        {
            get { return workRate; }
            set { workRate = value; }
        }

        public string FlagUse
        {
            get { return flagUse; }
            set { flagUse = value; }
        }


        public int WorkShift
        {
            get { return workShift; }
            set { workShift = value; }
        }

    }
}
