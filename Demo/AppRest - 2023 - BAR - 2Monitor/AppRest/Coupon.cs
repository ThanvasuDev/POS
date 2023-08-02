using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Coupon
    {
        int couponID;
        string couponCode;
        string couponName;
        string couponDesc;
        string couponFromDate;
        string couponToDate;
        string couponSynTax;
        int couponUserID;
        string couponRemark;
        string couponTextShow;
        string couponUsed;
        string couponFlagUse;
        string couponFlagExpire;  
         

        public Coupon()
        {

        }

        public Coupon(int couponID, string couponCode, string couponName, string couponDesc, string couponFromDate, string couponToDate, string couponSynTax, int couponUserID, string couponRemark, string couponUsed, string couponFlagUse, string couponFlagExpire, string couponTextShow)
        {
            CouponID = couponID;
            CouponCode = couponCode;
            CouponName = couponName;
            CouponDesc = couponDesc;
            CouponFromDate = couponFromDate;
            CouponToDate = couponToDate;
            CouponSynTax = couponSynTax;
            CouponUserID = couponUserID;
            CouponRemark = couponRemark;
            CouponUsed = couponUsed;
            CouponFlagUse = couponFlagUse;
            CouponFlagExpire = couponFlagExpire;
            CouponTextShow = couponTextShow;

        }

        public int CouponID
        {
            get { return couponID; }
            set { couponID = value; }
        }

        public string CouponCode
        {
            get { return couponCode; }
            set { couponCode = value; }
        }


        public string CouponName
        {
            get { return couponName; }
            set { couponName = value; }
        }


        public string CouponDesc
        {
            get { return couponDesc; }
            set { couponDesc = value; }
        }


        public string CouponFromDate
        {
            get { return couponFromDate; }
            set { couponFromDate = value; }
        }


        public string CouponToDate
        {
            get { return couponToDate; }
            set { couponToDate = value; }
        }


        public string CouponSynTax
        {
            get { return couponSynTax; }
            set { couponSynTax = value; }
        }


        public int CouponUserID
        {
            get { return couponUserID; }
            set { couponUserID = value; }
        }


        public string CouponRemark
        {
            get { return couponRemark; }
            set { couponRemark = value; }
        }


        public string CouponTextShow
        {
            get { return couponTextShow; }
            set { couponTextShow = value; }
        }


        public string CouponUsed
        {
            get { return couponUsed; }
            set { couponUsed = value; }
        }



        public string CouponFlagUse
        {
            get { return couponFlagUse; }
            set { couponFlagUse = value; }
        }

        public string CouponFlagExpire
        {
            get { return couponFlagExpire; }
            set { couponFlagExpire = value; }
        }
 



    }
}
