using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class CashCard
    {

        int cCID;
        string cCCode;
        int cCType;
        int cCGroupID;
        int cCDeposit;
        int cCBalanceAmt;
        int cCBalanceUnit;
        int cCLastPayType;
        string cCLastPayTypeName;
        int cCStatus;
        string cCStatusDesc;
        string cCStartDate;
        string cCLastUpdate;  
        string cCExpireDate; 
        string flagExpire;
        string cCLastPOS;
        string cCRemark;
        string createBy;
        DateTime createDate;
        string updateBy;
        DateTime updateDate;

         

    


        public CashCard(int cCID, string cCCode, int cCType, int cCGroupID, int cCDeposit, int cCBalanceAmt, int cCBalanceUnit, int cCLastPayType, string cCLastPayTypeName,int cCStatus, string cCStatusDesc, string cCStartDate, string cCLastUpdate,string cCExpireDate , string flagExpireDate, string cCLastPOS, string cCRemark, string createBy, DateTime createDate, string updateBy, DateTime updateDate)
        {
            CCID = cCID;
            CCCode = cCCode;
            CCType = cCType;
            CCGroupID = cCGroupID;
            CCDeposit = cCDeposit;
            CCBalanceAmt = cCBalanceAmt;
            CCBalanceUnit = cCBalanceUnit;

            CCLastPayType = cCLastPayType;
            CCLastPayTypeName = cCLastPayTypeName;
            CCStatus = cCStatus;
            CCStatusDesc = cCStatusDesc;

            CCStartDate = cCStartDate;
            CCLastUpdate = cCLastUpdate;

            CCExpireDate = cCExpireDate;
            FlagExpire = flagExpireDate;

            CCLastPOS = cCLastPOS;
            CCRemark = cCRemark;
            CreateBy = createBy;
            CreateDate = createDate;
            UpdateBy = updateBy;
            UpdateDate = updateDate;  

            // Add field


        }


        public int CCID
        {
            get { return cCID; }
            set { cCID = value; }
        } 

        public string CCCode
        {
            get { return cCCode; }
            set { cCCode = value; }
        } 

        public int CCType
        {
            get { return cCType; }
            set { cCType = value; }
        } 

        public int CCGroupID
        {
            get { return cCGroupID; }
            set { cCGroupID = value; }
        } 

        public int CCDeposit
        {
            get { return cCDeposit; }
            set { cCDeposit = value; }
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

        public string CCStartDate
        {
            get { return cCStartDate; }
            set { cCStartDate = value; }
        } 

        public string CCLastUpdate
        {
            get { return cCLastUpdate; }
            set { cCLastUpdate = value; }
        } 

        public string CCLastPOS
        {
            get { return cCLastPOS; }
            set { cCLastPOS = value; }
        } 

        public string CCRemark
        {
            get { return cCRemark; }
            set { cCRemark = value; }
        } 

        public string CreateBy
        {
            get { return createBy; }
            set { createBy = value; }
        } 

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        } 

        public string UpdateBy
        {
            get { return updateBy; }
            set { updateBy = value; }
        } 

        public DateTime UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }

        public string CCExpireDate
        {
            get { return cCExpireDate; }
            set { cCExpireDate = value; }
        }

        public string FlagExpire
        {
            get { return flagExpire; }
            set { flagExpire = value; }
        }
        public int CCLastPayType
        {
            get { return cCLastPayType; }
            set { cCLastPayType = value; }
        }

        public string CCLastPayTypeName
        {
            get { return cCLastPayTypeName; }
            set { cCLastPayTypeName = value; }
        }

        public int CCStatus
        {
            get { return cCStatus; }
            set { cCStatus = value; }
        }

        public string CCStatusDesc
        {
            get { return cCStatusDesc; }
            set { cCStatusDesc = value; }
        }

    }
}
