using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class MemCard
    {
        int groupRestID;
        int restID;
        string memCardID;
        string cardID; 
        string memCardName;
        string sex;
        DateTime birthDate;
        string tel;
        string address;
        string email;
        int point;
        int pointPeriod;
        float pAmount;
        float pAmountPeriod; 
        string promotionCode;
        string promotionDesc;
        string promotionSyntax; 
        string adviceID;
        string adviceName;
        DateTime applicationDate;
        DateTime renewDate;
        DateTime expireDate;
        string flagExpire;
        string createByRest;
        int cCBalanceAmt;
        int cCBalanceUnit;
        string cCLastPOS;
        int cCStatus;




        public MemCard() { }

        public MemCard(int groupRestID, int restID, string memCardID, string cardID, string memCardName, string sex, DateTime birthDate, string tel, string address, string email, int point, int pointPeriod, float pAmount, float pAmountPeriod, string promotionCode, string promotionDesc, string promotionSyntax, string adviceID, string adviceName, DateTime applicationDate, DateTime renewDate, DateTime expireDate, string flagExpire, string createByRest, int cCBalanceAmt, int cCBalanceUnit, string cCLastPOS, int cCStatus)
        {
            GroupRestID = groupRestID;
            RestID = restID;
            MemCardID = memCardID;
            CardID = cardID;
            MemCardName = memCardName;
            Sex = sex;
            BirthDate = birthDate;
            Tel = tel;
            Address = address;
            Email = email;
            Point = point;
            PointPeriod = pointPeriod;
            PAmount = pAmount;
            PAmountPeriod = pAmountPeriod;
            PromotionCode = promotionCode;
            PromotionDesc = promotionDesc;
            PromotionSyntax = promotionSyntax;
            AdviceID = adviceID;
            AdviceName = adviceName;
            ApplicationDate = applicationDate;
            RenewDate = renewDate;
            ExpireDate = expireDate;
            FlagExpire = flagExpire;
            CreateByRest = createByRest;
            CCBalanceAmt = cCBalanceAmt;
            CCBalanceUnit = CCBalanceUnit;
            CCLastPOS = cCLastPOS;
            CCStatus = cCStatus;
        }



        public int GroupRestID
        {
            get { return groupRestID; }
            set { groupRestID = value; }
        } 

        public int RestID
        {
            get { return restID; }
            set { restID = value; }
        } 

        public string MemCardID
        {
            get { return memCardID; }
            set { memCardID = value; }
        }


        public string CardID
        {
            get { return cardID; }
            set { cardID = value; }
        }

        public string MemCardName
        {
            get { return memCardName; }
            set { memCardName = value; }
        } 

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        } 

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
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

        public string Email
        {
            get { return email; }
            set { email = value; }
        } 

        public int Point
        {
            get { return point; }
            set { point = value; }
        }


        public int PointPeriod
        {
            get { return pointPeriod; }
            set { pointPeriod = value; }
        }

        public float PAmount
        {
            get { return pAmount; }
            set { pAmount = value; }
        }


        public float PAmountPeriod
        {
            get { return pAmountPeriod; }
            set { pAmountPeriod = value; }
        }

         

        public string PromotionCode
        {
            get { return promotionCode; }
            set { promotionCode = value; }
        } 

        public string PromotionDesc
        {
            get { return promotionDesc; }
            set { promotionDesc = value; }
        }


        public string PromotionSyntax
        {
            get { return promotionSyntax; }
            set { promotionSyntax = value; }
        }

        public string AdviceID
        {
            get { return adviceID; }
            set { adviceID = value; }
        } 

        public string AdviceName
        {
            get { return adviceName; }
            set { adviceName = value; }
        } 

        public DateTime ApplicationDate
        {
            get { return applicationDate; }
            set { applicationDate = value; }
        } 

        public DateTime RenewDate
        {
            get { return renewDate; }
            set { renewDate = value; }
        }



        public DateTime ExpireDate
        {
            get { return expireDate; }
            set { expireDate = value; }
        }


        public string FlagExpire
        {
            get { return flagExpire; }
            set { flagExpire = value; }
        }


        public string CreateByRest
        {
            get { return createByRest; }
            set { createByRest = value; }
        }

        public int CCBalanceAmt
        {
            get { return cCBalanceAmt; }
            set { cCBalanceAmt = value; }
        }

        public int CCBalanceUnit
        {
            get { return cCBalanceUnit; }
            set { cCBalanceUnit = value; }
        }


        public string CCLastPOS
        {
            get { return cCLastPOS; }
            set { cCLastPOS = value; }
        }


        public int CCStatus
        {
            get { return cCStatus; }
            set { cCStatus = value; }
        }

    }
}
