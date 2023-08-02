using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Prom
    {

        int promID;
        string promCode;
        string promName;
        string promCheckitem;
        int promCountItem;
        float promAmountBil;
        string promAutoUse;
        int productGroupID;
        string promDatefrom;
        string promDateTo;
        string promtime1;
        string promtime2;
        string promtime3;
        string promDayofWeek;

        int promLimitItem;


        int promBalanceItem;



        public Prom(int promID, string promCode, string promName, string promCheckitem, int promCountItem, float promAmountBil, string promAutoUse, int productGroupID, string promDatefrom, string promDateTo, string promtime1, string promtime2, string promtime3, int promLimitItem, int promBalanceItem,string promDayofWeek)
        {
            PromID = promID;
            PromCode = promCode;
            PromName = promName;
            PromCheckitem = promCheckitem;
            PromCountItem = promCountItem;
            PromAmountBil = promAmountBil;
            PromAutoUse = promAutoUse;
            ProductGroupID = productGroupID;
            PromDatefrom = promDatefrom;
            PromDateTo = promDateTo;
            Promtime1 = promtime1;
            Promtime2 = promtime2;
            Promtime3 = promtime3;
            PromLimitItem = promLimitItem;
            PromBalanceItem = promBalanceItem;
            PromDayofWeek = promDayofWeek;
        } 

        public int PromID
        {
          get { return promID; }
          set { promID = value; }
        }  

        public string PromCode
        {
          get { return promCode; }
          set { promCode = value; }
        } 

        public string PromName
        {
            get { return promName; }
            set { promName = value; }
        } 

        public string PromCheckitem
        {
            get { return promCheckitem; }
            set { promCheckitem = value; }
        } 

        public int PromCountItem
        {
            get { return promCountItem; }
            set { promCountItem = value; }
        } 

        public float PromAmountBil
        {
            get { return promAmountBil; }
            set { promAmountBil = value; }
        } 

        public string PromAutoUse
        {
            get { return promAutoUse; }
            set { promAutoUse = value; }
        } 

        public int ProductGroupID
        {
            get { return productGroupID; }
            set { productGroupID = value; }
        } 

        public string PromDatefrom
        {
            get { return promDatefrom; }
            set { promDatefrom = value; }
        } 

        public string PromDateTo
        {
            get { return promDateTo; }
            set { promDateTo = value; }
        } 

        public string Promtime1
        {
            get { return promtime1; }
            set { promtime1 = value; }
        } 

        public string Promtime2
        {
            get { return promtime2; }
            set { promtime2 = value; }
        } 

        public string Promtime3
        {
            get { return promtime3; }
            set { promtime3 = value; }
        }


        public string PromDayofWeek
        {
            get { return promDayofWeek; }
            set { promDayofWeek = value; }
        }

        public int PromLimitItem
        {
            get { return promLimitItem; }
            set { promLimitItem = value; }
        }

        public int PromBalanceItem
        {
            get { return promBalanceItem; }
            set { promBalanceItem = value; }
        }

    }
}
