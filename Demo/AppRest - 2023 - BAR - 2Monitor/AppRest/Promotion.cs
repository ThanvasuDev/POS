using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRest
{
    class Promotion
    {
        string promotionCode; 
        string promotionDesc; 
        string promotionSyntax;
        string flagUse;



        public Promotion() { }

        public Promotion(string promotionCode, string promotionDesc, string promotionSyntax, string flagUse) 
        {
            PromotionCode = promotionCode;
            PromotionDesc = promotionDesc;
            PromotionSyntax = promotionSyntax;
            FlagUse = flagUse;

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

        public string FlagUse
        {
            get { return flagUse; }
            set { flagUse = value; }
        }



    }
}
