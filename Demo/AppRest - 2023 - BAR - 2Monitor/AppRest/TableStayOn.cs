using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class TableStayOn
    {
        

  
        int tableID;
        string tableName;
        string userStayOn;

        public TableStayOn(int tableID, string tableName, string userStayOn)
        {
            TableID = tableID;
            TableName = tableName;
            UserStayOn = userStayOn;

        }
         
        public int TableID
        {
            get { return tableID; }
            set { tableID = value; }
        }


        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        

        public string UserStayOn
        {
            get { return userStayOn; }
            set { userStayOn = value; }
        }






    }
}
