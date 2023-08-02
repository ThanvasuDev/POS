using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class MemWork
    {
        int userID;
        string userName;
        string status;
        int workShift;

        string workIN;
        string workOut;
        float workHour;

        public MemWork()
        {

        }

        public MemWork(int userID, string userName, string status, int workShift, string workIN, string workOut, float workHour)
        {
            UserID = userID;
            UserName = userName;
            Status = status;
            WorkShift = workShift;
            WorkIN = workIN;
            WorkOut = workOut;
            WorkHour = workHour;
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



        public string Status
        {
            get { return status; }
            set { status = value; }
        }


        public int WorkShift
        {
            get { return workShift; }
            set { workShift = value; }
        }

        public string WorkIN
        {
            get { return workIN; }
            set { workIN = value; }
        }

        public string WorkOut
        {
            get { return workOut; }
            set { workOut = value; }
        }


        public float WorkHour
        {
            get { return workHour; }
            set { workHour = value; }
        }

    }
}
