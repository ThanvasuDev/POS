using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class EndBill
    {
       
        string endDayDate;
        int startBillNo;
        int endBillNo;
        int noofBills;
        float salesTotal;
        float salesCash;
        float salesCreditCard;
        float salesCreditCust;
        float otherIncomeCash;
        float bankOutforCash;
        float changeCash;
        float totalCash;
        float totalExpenseCash;
        float balanceCash;
        float countRealCash;
        float netBalanceCash;
        float adjustCash;
        float finalBalance;
        float cashtoBank;
        float endBalance;
        string remark;
        DateTime createDate;
        string createBy;
        DateTime updateDate;
        string updateBy;


        public EndBill( string endDayDate, int startBillNo, int endBillNo ,int noofBills , float salesTotal,  float salesCash, float salesCreditCard, float salesCreditCust,  float otherIncomeCash, float bankOutforCash,  float changeCash,  float totalCash,  float totalExpenseCash,  float balanceCash,  float countRealCash,  float netBalanceCash, float adjustCash,  float finalBalance, float cashtoBank, float endBalance,  string remark,  DateTime createDate, string createBy,  DateTime updateDate,  string updateBy)
        {

            EndDayDate = endDayDate;
            StartBillNo = startBillNo;
            EndBillNo =  endBillNo;
            NoofBills = noofBills;
            SalesTotal= salesTotal;
            SalesCash= salesCash;
            SalesCreditCard= salesCreditCard;
            SalesCreditCust= salesCreditCust;
            OtherIncomeCash = otherIncomeCash;
            BankOutforCash = bankOutforCash;
            ChangeCash = changeCash;
            TotalCash = totalCash;
            TotalExpenseCash = totalExpenseCash;
            BalanceCash = balanceCash;
            CountRealCash = countRealCash;
            NetBalanceCash = netBalanceCash;
            AdjustCash = adjustCash;
            FinalBalance =finalBalance;
            CashtoBank = cashtoBank;
            EndBalance = endBalance;
            Remark = remark;
            CreateDate = createDate;
            CreateBy = createBy;
            UpdateDate = updateDate;
            UpdateBy = updateBy;
        }

        public EndBill(string endDayDate, int startBillNo)
        {
            EndDayDate = endDayDate;
            StartBillNo = startBillNo;
        }

        public string EndDayDate
        {
            get { return endDayDate; }
            set { endDayDate = value; }
        }
         
        public int StartBillNo
        {
            get { return startBillNo; }
            set { startBillNo = value; }
        } 

        public int EndBillNo
        {
            get { return endBillNo; }
            set { endBillNo = value; }
        } 

        public int NoofBills
        {
            get { return noofBills; }
            set { noofBills = value; }
        } 

        public float SalesTotal
        {
            get { return salesTotal; }
            set { salesTotal = value; }
        } 

        public float SalesCash
        {
            get { return salesCash; }
            set { salesCash = value; }
        } 

        public float SalesCreditCard
        {
            get { return salesCreditCard; }
            set { salesCreditCard = value; }
        } 

        public float SalesCreditCust
        {
            get { return salesCreditCust; }
            set { salesCreditCust = value; }
        } 

        public float OtherIncomeCash
        {
            get { return otherIncomeCash; }
            set { otherIncomeCash = value; }
        } 

        public float BankOutforCash
        {
            get { return bankOutforCash; }
            set { bankOutforCash = value; }
        } 

        public float ChangeCash
        {
            get { return changeCash; }
            set { changeCash = value; }
        } 

        public float TotalCash
        {
            get { return totalCash; }
            set { totalCash = value; }
        } 

        public float TotalExpenseCash
        {
            get { return totalExpenseCash; }
            set { totalExpenseCash = value; }
        } 

        public float BalanceCash
        {
            get { return balanceCash; }
            set { balanceCash = value; }
        } 

        public float CountRealCash
        {
            get { return countRealCash; }
            set { countRealCash = value; }
        } 

        public float NetBalanceCash
        {
            get { return netBalanceCash; }
            set { netBalanceCash = value; }
        } 

        public float AdjustCash
        {
            get { return adjustCash; }
            set { adjustCash = value; }
        } 

        public float FinalBalance
        {
            get { return finalBalance; }
            set { finalBalance = value; }
        } 

        public float CashtoBank
        {
            get { return cashtoBank; }
            set { cashtoBank = value; }
        } 

        public float EndBalance
        {
            get { return endBalance; }
            set { endBalance = value; }
        } 

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        } 

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        } 

        public string CreateBy
        {
            get { return createBy; }
            set { createBy = value; }
        } 

        public DateTime UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        } 

        public string UpdateBy
        {
            get { return updateBy; }
            set { updateBy = value; }
        }


    }
}
