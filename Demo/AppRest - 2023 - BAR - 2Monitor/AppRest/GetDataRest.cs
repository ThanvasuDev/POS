using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace AppRest
{
    class GetDataRest
    {
        public int branchID;
        public int groupRestID;
        public int restID;
        public string pOSIDConfig;

        string strConn;

        public GetDataRest()
        {
            branchID  =  Int32.Parse(ConfigurationSettings.AppSettings["BranchID"].ToString()  ) ;
            groupRestID = Int32.Parse(ConfigurationSettings.AppSettings["GroupRestID"].ToString());
            restID = Int32.Parse(ConfigurationSettings.AppSettings["RestID"].ToString());

            pOSIDConfig =  ConfigurationSettings.AppSettings["POSIDConfig"].ToString();

         //   restID = "Data Source=.;Initial Catalog=CFS_POS_Local;Persist Security Info=True;User ID=sa;Password=p@$$w0rd";
        }


        public List<Table> getMainTable(int zoneID )
        {
           
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_MainTable(0, zoneID);
            
            List<Table> result = new List<Table>();
            
            foreach (var r in strQuery)
            {
                result.Add(new Table((int)r.TableZoneID, (int)r.TableID, r.TableName, r.TableCountOrder, r.TableFlagUse, (int)r.TablePrintBill, (int)r.TableCreditCustID , r.TableFCCode ));
            }

            return result;

        }

        public List<Table> getMainTable(int zoneID, int orderType)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_MainTable(orderType, zoneID);

            List<Table> result = new List<Table>();

            foreach (var r in strQuery)
            {
                result.Add(new Table((int)r.TableZoneID, (int)r.TableID, r.TableName, r.TableCountOrder, r.TableFlagUse, (int)r.TablePrintBill, (int)r.TableCreditCustID, r.TableFCCode));
            } 

            return result;

        }

        public Table getOrderTableStatus(int tableID)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_OrderTableStatus(this.branchID,tableID);

            Table result = new Table();

            foreach (var r in strQuery)
            {
                result = new Table((int)r.TableZoneID, r.FlagServiceCharge ,(int)r.TableID, r.TableName, r.TableCountOrder, r.TableFlagUse, (int)r.TablePrintBill, (int)r.TableCreditCustID,(int) r.DisCountType, (int) r.DiscountPer , (int) r.DisCountAmt , (int) r.Tax , (int) r.ServiceCharge,r.Remark,r.StrMemSearch,r.TableZoneVAT , (int)r.TableZonePriceID );
            }
            return result;

        }

        public Table getMainOrderByTable(int tableID, string flagLang, int proGropCatID, int proFlagSend,int memID,int subOrderID)
        {

            DataRestDataContext db = new DataRestDataContext();

            Table result = new Table();


            var strQuery = db.Sp_App_OrderDetail(tableID, this.branchID, flagLang, proGropCatID, proFlagSend, memID, subOrderID, Login.userName);

            List<Order> order = new List<Order>();

            foreach (var r in strQuery)
            {
                order.Add(new Order((int)r.TableID, (int)r.CatID, r.CatName, (int)r.ProductID, r.ProductName, (float)r.ProducrPrice, (float)r.OrderQTY, (float)r.OrderAmount, r.CreateDate, (int)r.FlagSend, (int)r.OrderNo, (int)r.ProductCatID, r.ProductCatName, r.ProductDesc, r.OrderBarcode, (float)r.RemarkOrderAmt, r.TimetoOrder, (int)r.FlagNonServiceCharge, (int)r.FlagNonVAT, (int)r.FlagNonDiscount, (int)r.StdTime, (int)r.DiscountGroupID, r.ProductBarcode, (float)r.ProducrPriceM, (float)r.OrderAddAmt, (int)r.OrderProductPoint));
                 
            }

            var strQuery1 = db.Sp_App_OrderHeader(tableID, this.branchID, memID, subOrderID);

            foreach (var r in strQuery1)
            {
                result = new Table((int)r.ZoneID, (int)r.TableID, r.TableName, r.TableFlagUse, (float)r.OrderQTY, (float)r.OrderAmount , order  , r.QROrder);
            }

            return result ;

        }

        public List<Cat> getOrderCat(int type)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllCat(type);

            List<Cat> result = new List<Cat>();

            foreach (var r in strQuery)
            {
                if (r.CatFlagUse == "Y")
                    result.Add(new Cat((int)r.CatID, (int)r.GroupCatId, r.CatName, r.CatNameTH, r.CatNameEN, r.CatDesc, (int)r.CatPrinterNo, r.CatPrinterType, r.CatColour, (int)r.CatSort, (float)r.CatConsignmentPercent, r.CatFlagUse));
            }

            return result;

        }

        public List<GroupCat> getAllGroupCat(int type)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllGroupCat(type);

            List<GroupCat> result = new List<GroupCat>();

            foreach (var r in strQuery)
            {
                result.Add(new GroupCat((int)r.GroupCatID, r.GroupCatName,r.GroupCatNameTH,r.GroupCatNameEN,(int)r.DiscountGroupID ,r.GroupCatColour,(int)r.GroupCatSort,r.GroupCatFlagUse  ));
            }

            return result;

        }

        public List<Zone> getAllZone()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllZone(this.branchID);

            List<Zone> result = new List<Zone>();

            foreach (var r in strQuery)
            {
                result.Add(new Zone((int) r.ZoneID ,r.ZoneName,r.ZoneDesc,r.FlagServiceCharge ,(int)r.ZoneSort,r.ZoneColour,(int)r.ZonePriceID ,r.ZoneVAT , r.ZoneType , (int)r.ZonePrinterNo , r.ZonePrinterType , (int)r.ZonePrinterCheckerNo ,r.ZoneRemark));
            }

            return result;

        }

        public List<PayType> getAllPayType()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllPayType(this.branchID);

            List<PayType> result = new List<PayType>();

            foreach (var r in strQuery)
            {
                result.Add(new PayType((int)r.PayTypeID, r.PayTypeName));
            }

            return result;

        }

        public List<Shipper> getAllShipper()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllShipper(this.branchID);

            List<Shipper> result = new List<Shipper>();

            foreach (var r in strQuery)
            {
                result.Add(new Shipper((int)r.ShipperID, r.ShipperName));
            }

            return result;

        }

        public List<Cat> getCatByGroupCat(int groupCatID)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_CatByGroupCat(groupCatID);

            List<Cat> result = new List<Cat>();

            foreach (var r in strQuery)
            {
                result.Add(new Cat((int)r.CatID, (int) r.GroupCatId,r.CatName,r.CatNameTH,r.CatNameEN,r.CatDesc, (int)r.CatPrinterNo, r.CatPrinterType , r.CatColour ,  (int)r.CatSort, (float) r.CatConsignmentPercent ,r.CatFlagUse ) ) ;
            }

            return result;

        }


        public List<Product> getProductByCat(int catID, int zoneID, int memID)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_ProductByCat(catID, zoneID, memID);

            List<Product> result = new List<Product>();

            foreach (var r in strQuery)
            {
                result.Add(new Product((int)r.ProductID, (int)r.ProductCatID, r.ProductName, r.ProductNameEN, r.ProductUnit, r.ProductDesc, r.ProductColour, (float)r.ProductPrice, (float)r.ProductPrice2, (float)r.ProductPrice3, (float)r.ProductPrice4, (float)r.ProductPrice5, (float)r.ProductCost, r.ProductFlagUse, r.ProductImage, r.ProductFlagStock, r.StockType, (int)r.ProductPromID, (int)r.ProductGetPoint,0,0, (int)r.ProductPrinterNo, (int)r.ProductPrinterNo2, (int)r.ProductSuplierID, (float)r.ProductConPercent,(int) r.ProductFlagNonServiceCharge ,(int) r.ProductFlagNonVAT ,(int) r.ProductFlagNonDiscount ,(int) r.ProductStdTime , (int)r.ProductFlagDelivery, (int)r.ProductCRM_Flag,r.ProductCRM_CPType,r.ProductCRM_ImgURL,r.ProductCRM_ImgURL2,r.ProductCRM_SynTax,r.ProductCRM_PeriodTime,r.ProductCRM_Store,r.ProductCRM_TC , (int)r.ProductQROrder));
            }

            return result;

        }

        public List<Product> getProductByCat_Search(int catID, string strSearch, int zoneID, int memID)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_ProductByCat_Search(catID, strSearch, zoneID, memID);

            List<Product> result = new List<Product>();

            foreach (var r in strQuery)
            {
                result.Add(new Product((int)r.ProductID, (int)r.ProductCatID, r.ProductName, r.ProductName, r.ProductUnit, r.ProductDesc ,"", (float)r.ProductPrice, 0, 0, 0,0,0, "Y", "-", "Y", "Y", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,"","","","","","","" , 0));
            }

            return result;

        }

        public List<Product> getProductByCat_SCBarCode(string strSearchBarcode,int zoneID,int memID)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_ProductByCat_SCBarCode(strSearchBarcode, zoneID, memID);

            List<Product> result = new List<Product>();

            foreach (var r in strQuery)
            {
                result.Add(new Product((int)r.ProductID, (int)r.ProductCatID, r.ProductName, r.ProductName, r.ProductUnit, r.ProductDesc,"", (float)r.ProductPrice, (float)r.ProductPrice2, (float)r.ProductPrice3, (float)r.ProductPrice4, (float)r.ProductPrice5, 0, "Y", "-", "Y", "Y", 0, 0, (float)r.ProductWeight, 0, 0, 0, 0, 0, 0, 0, 0,0, 0,0 ,"", "", "", "", "", "", "",0));
            }

            return result;

        }

        public List<Tbl> getTableByZone(int zoneID)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_TableByZone(zoneID);

            List<Tbl> result = new List<Tbl>();

            foreach (var r in strQuery)
            {
                result.Add(new Tbl((int)r.TableID, (int)r.TableZoneID , r.TableName,r.TableDesc,r.TableFlagUse));
            }

            return result;

        }

        public List<Cust> getListAllCust()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_ListAllCust(this.branchID);

            List<Cust> result = new List<Cust>();

            foreach (var r in strQuery)
            {
                result.Add(new Cust((int)r.UserID, r.UserName));
            }

            return result;

        } 

        public List<StoreCat> getListAllStoreCat()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllStoreCat();

            List<StoreCat> result = new List<StoreCat>();

            foreach (var r in strQuery)
            {
                result.Add(new StoreCat((int)r.StoreCatID, r.StoreCatName , r.StoreCatDesc));
            }

            return result;

        }

        public List<Store> getListAllStore(int storeCatID, int storeID, string storeBarcode)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllStore(storeCatID, storeID, storeBarcode);

            List<Store> result = new List<Store>();

            foreach (var r in strQuery)
            {
                result.Add(new Store((int)r.StoreID, (int)r.StoreCatID, r.StoreName, r.StoreUnit, r.StoreConvertUnit, (float)r.StoreConvertRate, (float)r.StoreCost, (float)r.StoreAvgCost, (float)r.KPILowStock, (float)r.KPIOverStock, r.StoreOrder, r.StoreBarcode, r.StoreSupCode, r.StoreFlagUse));
            }

            return result;

        }

        public List<BillRemark> getAllBillRemark()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllBillRemark();

            List<BillRemark> result = new List<BillRemark>();

            foreach (var r in strQuery)
            {
                result.Add(new BillRemark(1,r.BillRemarkline1,r.BillRemarkline2,r.BillRemarkline3));
            }

            return result;

        }


        public int updsBillRemark(int billRemarkID,string billRemarkline1 , string billRemarkline2 , string billRemarkline3 )
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateBillRemark(billRemarkline1,billRemarkline2,billRemarkline3);

            return (int)strQuery;

        }

        public int updsReshBranch(string branchNameTH, string branchNameEN, string restNameTH, string restAddr1TH, string restAddr2TH, string restNameEN, string restAddr1EN, string restAddr2EN, string restTel, string restTaxID, string restLine1, string restLine2, string restTAXRD)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateRestBranch(this.branchID, branchNameTH, branchNameEN, restNameTH, restAddr1TH, restAddr2TH, restNameEN, restAddr1EN, restAddr2EN, restTel, restTaxID, restLine1, restLine2, "", "", restTAXRD, 0.0);

            return (int)strQuery;

        }


        public int instOrderByTable(int tableID, int productID, int memID, string remark,float addOrderAmt)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertOrderByTable(tableID, productID, memID, this.branchID, remark, addOrderAmt);

            return (int) strQuery ;

        }

        public int instRemarkOrderByTable(int tableID, int productID, int memID, string createDate, string remark, float addOrderAmt, int flagAddOrderAmtBaht, int flagAddRemarkByItem)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertRemarkOrderByTable(tableID, productID, memID, this.branchID, createDate, remark, addOrderAmt,flagAddOrderAmtBaht,flagAddRemarkByItem);

            return (int)strQuery;

        }

        public int updsMoveOrderByTable(int tableID, int productID, int memID, string createDate, int newTableID)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateMoveOrderByTable(tableID, productID, memID, this.branchID, createDate, newTableID);

            return (int)strQuery;

        }

        public int updsMoveOrderSubID(int tableID, int OrderSubID, int newOrderSubID, string orderBarcode)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateMoveSubOrder(this.branchID,tableID, OrderSubID, newOrderSubID, orderBarcode);

            return (int)strQuery;

        }

    

        public int updsMoveOrderByTable(int tableID, int productID, int memID, string createDate)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateHoldOrderByTable(tableID, productID, memID, this.branchID, createDate);

            return (int)strQuery;

        }

        public int delOrderByTable(int tableID, int productID,string createDate,string reason)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_DeleteOrderByTable(tableID, productID, this.branchID, createDate, Login.userName, reason);

            return (int)strQuery;

        }


        public int delAllOrderByTable(int tableID)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_DeleteAllOrderByTable(tableID, this.branchID,Login.userName);

            return (int)strQuery;

        }

        public int UpsMoveTable(int oldTableID,int newTableID)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateMoveTable(oldTableID, newTableID, this.branchID,Login.userName );

            return (int)strQuery;

        }


        public int UpsTaxAgo(int trnID, int payCustID, int type, string shipName, string shipNo, string shipRem, float eanrningAmt, int eanrningPayTypeID,int newPaymentTypeID)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateTaxAgo(this.branchID, trnID, payCustID, type, shipName, shipNo, shipRem, eanrningAmt, eanrningPayTypeID, newPaymentTypeID);

            return (int)strQuery;

        }

        public int instNewCoupon(string couponCode, string couponName, string couponDesc, string couponFromDate, string couponToDate, string couponSynTax, int couponUserID, string couponRemark, string couponUsed, string couponFlagUse )
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertNewCoupon(couponCode, couponName, couponDesc, couponFromDate, couponToDate, couponSynTax, couponUserID, couponRemark, couponFlagUse, Login.userName);

            return (int)strQuery;

        }

        public int updsCoupon(int couponID, string couponCode, string couponName, string couponDesc, string couponFromDate, string couponToDate, string couponSynTax, int couponUserID, string couponRemark, string couponUsed, string couponFlagUse , string updateType)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateCoupon(couponID, couponName, couponDesc, couponFromDate, couponToDate, couponSynTax, couponUserID, couponRemark, couponFlagUse, updateType);

            return (int)strQuery;

        }





        public int instNewMember(string userName, string password, string name, string tel, string address,string status ,int workRate, string flagUse,int workShift)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertNewMember(userName, password, name, tel, address, status, workRate, flagUse, this.branchID, workShift); 

            return (int)strQuery;

        }

        public int updsMember(int userID, string userName, string password, string name, string tel, string address, string status, int workRate, string flagUse, int workShift)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateMember(userID, userName, password, name, tel, address, status, workRate, flagUse, this.branchID, workShift);

            return (int)strQuery;

        }

        public int instNewProduct(int catID, string productName, string productNameEN, string productUnit, string productDesc,  string productColour, float productPrice, float productPrice2, float productPrice3, float productPrice4, float productPrice5, float productCost, string productFlagUse, string productBarcode, string productFlagStock, string productStockType, int productPromID, int productGetPoint,int productPrinterNo, int productPrinterNo2, int productSuplierID, float productPercentCont, int productFlagNonServiceCharge, int productFlagNonVAT, int productFlagNonDiscount, int stdTime, int flagDelivery, int flagCRM,
                       string crmCPType, string crmImgUrl1, string crmImgUrl2, string crmCouponSynTax, string crmPeriodTime, string crmStore, string crmTC , int productQROrder)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertNewProduct(catID, productName, productNameEN,  productUnit, productDesc, productColour, productPrice, productPrice2, productPrice3, productPrice4, productPrice5, productCost, productFlagUse, productBarcode, productFlagStock, productStockType, productPromID, productGetPoint, productPrinterNo, productPrinterNo2, productSuplierID, productPercentCont, productFlagNonServiceCharge, productFlagNonVAT, productFlagNonDiscount, stdTime, flagDelivery, flagCRM, crmCPType, crmImgUrl1, crmImgUrl2, crmCouponSynTax, crmPeriodTime, crmStore, crmTC, productQROrder, Login.userName);

            return (int)strQuery;

        }

        public int updsProduct(int productCatID, int productID, string productName, string productNameEN, string productUnit, string productDesc, string productColour, float productPrice, float productPrice2, float productPrice3, float productPrice4, float productPrice5, float productCost, string productFlagUse, string productBarcode, string productFlagStock, string productStockType, int productPromID, int productGetPoint,  int productPrinterNo, int productPrinterNo2, int productSuplierID, float productPercentCont, int productFlagNonServiceCharge, int productFlagNonVAT, int productFlagNonDiscount, int stdTime, int flagDelivery, int flagCRM,
                      string crmCPType, string crmImgUrl1, string crmImgUrl2, string crmCouponSynTax, string crmPeriodTime, string crmStore, string crmTC, int productQROrder )
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateProduct(productCatID, productID, productName, productName, productNameEN, productUnit, productDesc, productColour, productPrice, productPrice2, productPrice3, productPrice4, productPrice5, productCost, productFlagUse, productBarcode, productFlagStock, productStockType, productPromID, productGetPoint, productPrinterNo, productPrinterNo2, productSuplierID, productPercentCont, productFlagNonServiceCharge, productFlagNonVAT, productFlagNonDiscount, stdTime, flagDelivery, flagCRM, crmCPType, crmImgUrl1, crmImgUrl2, crmCouponSynTax, crmPeriodTime, crmStore, crmTC, productQROrder, Login.userName);

            return (int)strQuery;

        }

        public int updsProductFlag( int productID,string productFlagUse, string strParam)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateProductFlag( productID, productFlagUse,strParam,  Login.userName);

            return (int)strQuery;

        }

        public int instNewStore(int storeCatID, string storeName, string storeUnit, string storeConvertUnit, float storeConvertRate, float storeCost, float storeAvgCost, float storeKPILowStock, float storeKPIOverStock, string storeOrder, string storeBarcode, string storeSupCode, string storeFlagUse)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertNewStore(storeCatID, storeName, storeUnit, storeConvertUnit, storeConvertRate, storeCost, storeAvgCost, storeKPILowStock, storeKPIOverStock, storeOrder, storeBarcode, storeSupCode, storeFlagUse);

            return (int)strQuery;

        }

        public int updsStore(int storeID, int storeCatID, string storeName, string storeUnit, string storeConvertUnit, float storeConvertRate, float storeCost, float storeAvgCost, float storeKPILowStock, float storeKPIOverStock, string storeOrder, string storeBarcode, string storeSupCode, string storeFlagUse)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateStore(storeID, storeCatID, storeName, storeUnit, storeConvertUnit, storeConvertRate, storeCost, storeAvgCost, storeKPILowStock, storeKPIOverStock, storeOrder, storeBarcode, storeSupCode, storeFlagUse);

            return (int)strQuery;

        }


        public int instNewCat(int groupCatID, string catName, string catNameTH, string catNameEN, string catDesc, int catPrinterNo, string catPrinterType, string catColour, int catSort, float catConsignmentPercent, string catFlagUse)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertNewCat(groupCatID, catName, catNameTH, catNameEN, catDesc, catPrinterNo, catPrinterType, catColour, catSort, catConsignmentPercent, catFlagUse);

            return (int)strQuery;

        }

        public int updsCat(int catID, int groupCatID, string catName, string catNameTH, string catNameEN, string catDesc, int catPrinterNo, string catPrinterType, string catColour, int catSort, float catConsignmentPercent, string catFlagUse)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateCat(catID, groupCatID,  catName, catNameTH, catNameEN, catDesc, catPrinterNo, catPrinterType, catColour, catSort, catConsignmentPercent, catFlagUse);

            return (int)strQuery;

        }


        public int instNewStoreCat(string storeCatName, string storeCatDesc)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertNewStoreCat(storeCatName, storeCatDesc);

            return (int)strQuery;

        }

        public int updsStoreCat(int storeCatID,string storeCatName, string storeCatDesc)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateStoreCat(storeCatID, storeCatName, storeCatDesc);

            return (int)strQuery;

        } 


        public int instNewZone(string zoneName, string zoneDesc,string flagServiceCharge,int zoneSort, string zoneColour,int zonePriceID , string zoneVAT , string zoneType , int zonePrinterNo , string zonePrinterType , int zonePrinterCheckerNo , string zoneRemark )
        {
            DataRestDataContext db = new DataRestDataContext(); 
            var strQuery = db.Sp_App_InsertNewZone(zoneName, zoneDesc,flagServiceCharge, this.branchID,zoneSort,zoneColour,zonePriceID ,zoneVAT,zoneType , zonePrinterNo, zonePrinterType, zonePrinterCheckerNo, zoneRemark);

            return (int)strQuery;

        }

        public int updsZone(int zoneID, string zoneName, string zoneDesc, string flagServiceCharge, int zoneSort, string zoneColour, int zonePriceID, string zoneVAT, string zoneType, int zonePrinterNo, string zonePrinterType, int zonePrinterCheckerNo, string zoneRemark)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateZone(zoneID, zoneName, zoneDesc, flagServiceCharge, this.branchID, zoneSort, zoneColour, zonePriceID, zoneVAT, zoneType, zonePrinterNo, zonePrinterType, zonePrinterCheckerNo, zoneRemark);

            return (int)strQuery;

        }



        public int instNewTable(int tableZoneID, string tableName, string tableDesc, string tableFlagUse)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertNewTable(this.branchID, tableZoneID, tableName, tableDesc, tableFlagUse);

            return (int)strQuery;

        }

        public int updsTable(int tableID , int tableZoneID, string tableName, string tableDesc, string tableFlagUse)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateTable(tableID, tableZoneID, tableName, tableDesc, tableFlagUse);

            return (int)strQuery;

        }

        public int instPrintBillFlag(int tableID, int custID, int disCountType, int discountPer, int disCountAmt, int tax, int serviceCharge, int printBillFlag,string remark,string strMemSearch)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertPrintBillFlag(this.branchID, tableID, custID, disCountType, discountPer, disCountAmt, tax, serviceCharge, printBillFlag, remark, strMemSearch, Login.userName);

            return (int)strQuery;

        }

        public int instWorkInOut(int userID,int flagWork)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertWorkInOut(this.branchID, userID, flagWork);

            return (int)strQuery;

        }

        public int instCustPayment(int userID, int amount ,int  payTypeID , string PayRemark , string PayCreditCardType , string PayCreditCardNo , string PayCreditCardCust ,string PayOTHERs , string PayBillID )
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertCustPayment(this.branchID, userID, amount , payTypeID ,  PayRemark ,PayCreditCardType , PayCreditCardNo ,PayCreditCardCust ,PayOTHERs ,PayBillID, Login.userName);

            return (int)strQuery;

        }

        public int delWorkInOut(int userID)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_DelWorkInOut(this.branchID, userID);

            return (int)strQuery;

        }

        public int delBill(int trnID, string code)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_DelBill(trnID,code,Login.userID);

            return (int)strQuery;

        }

        public int checkBillByTable(int tableID, int memID,float payAmount ,float discount, float serviceCharge, float vat,int custID, string reason, string memcardID , string couponCode, int subOrderID,List<BillPayment> billpayments)
        {
            int result = 0;

            try
            {

                DataRestDataContext db = new DataRestDataContext();

                var strQuery = db.Sp_App_CheckBill(this.branchID, tableID, memID, payAmount, discount, serviceCharge, vat, custID, reason, memcardID, this.groupRestID, this.restID, couponCode, 0, "", "", 0, "", subOrderID);

                result = (int)strQuery;

                if (result > 0)
                {
                    foreach (BillPayment bp in billpayments)
                    {
                        strQuery = db.Sp_App_InsertNewPaymentType(result, bp.PaySeqNo, bp.PayAmount, bp.PaytypeID, bp.PayDesc1, bp.PayDesc2, bp.PayDesc3, reason, Login.userName);
                    }

                }

                return result;
            }
            catch (Exception Ex)
            {

                return result;
            }

        }

        public Member checkAuthentication(string userName, string password)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_Login(userName, password,this.branchID);

            Member result = new Member();

            foreach (var r in strQuery)
            {
                result = new Member((int)r.UserID, r.UserName,r.Status,(int)r.WorkRate,(int)r.WorkShift);
            }

            return result;

        }

        public List<SalesToday> getSalesToday(int typeID,string fromDate,string toDate , int shiftID)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_SalesToday(this.branchID, typeID, fromDate, toDate, shiftID);

            List<SalesToday> result = new List<SalesToday>();

            foreach (var r in strQuery)
            {
                result.Add(new SalesToday(r.LableDate, r.LableName, (float)r.LableUnit, (float)r.LableSales));
            }

            return result;

        }

        public List<SalesToday> getSalesByday(int typeID,string salesDate)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_SalesByDay(this.branchID, typeID, salesDate);

            List<SalesToday> result = new List<SalesToday>();

            foreach (var r in strQuery)
            {
                result.Add(new SalesToday(r.LableDate, r.LableName, (float)r.LableUnit, (float)r.LableSales));
            }

            return result;

        }


        public List<Member> getListAllMember()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllMember(this.branchID);

            List<Member> result = new List<Member>();

            foreach (var r in strQuery)
            {
                 result.Add(new Member((int)r.UserID, r.UserName, r.Password, r.Name, r.Tel, r.Address, r.Status, (int)r.WorkRate, r.FlagUse, (int)r.WorkShift));
            }

            return result;

        }


        public List<Member> getListAllUser()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllMember(this.branchID);

            List<Member> result = new List<Member>();

            foreach (var r in strQuery)
            {
                if (r.Status.ToLower() != "customer" && r.Status.ToLower() != "b2b" && r.Status.ToLower() != "vip")  
                    result.Add(new Member((int)r.UserID, r.UserName,r.Password,r.Name, r.Tel, r.Address, r.Status, (int)r.WorkRate, r.FlagUse,(int) r.WorkShift));
            }

            return result;

        }


        public List<Customer> getListAllCustomer()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllDataCustomer(0);

            List<Customer> result = new List<Customer>();

            foreach (var r in strQuery)
            {
                result.Add(new Customer((int)r.CustID, r.CustCode, r.TaxID, r.Title, r.Name, r.Tel, r.Address, r.Status, r.FlagUse));
            }

            return result;

        }

        public DataTable getDataAllCustomer()
        {
            DataTable result = null;

            try
            {

                DataRestDataContext db = new DataRestDataContext();

                var strQuery = db.Sp_App_AllDataCustomer(0);

                result = ClassConvert.LINQToDataTable(strQuery);

            }
            catch (Exception ex)
            {
                result = new DataTable();

            }

            return result;
        }


        public DataTable getDataAllMember()
        {
            DataTable result = null;

            try
            {

                DataRestDataContext db = new DataRestDataContext();

                var strQuery = db.Sp_App_AllDataMember(this.branchID);

                result = ClassConvert.LINQToDataTable(strQuery);

            }
            catch (Exception ex)
            {
                result = new DataTable();

            }

            return result;
        }

         

        public List<string> getListAllMemStatus()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_AllMemStatus();

            List<string> result = new List<string>();

            foreach (var r in strQuery)
            {
                result.Add(r.MemStatus);
            }

            return result;

        }

        public Branch getBranchDesc()
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_BranchDesc(this.branchID);

            Branch result = new Branch();

            foreach (var r in strQuery)
            {
                result = new Branch((int)r.BranchId, r.BranchNameTH, r.BranchNameEN, r.RestNameTH, r.RestAddr1TH, r.RestAddr2TH, r.RestNameEN, r.RestAddr1EN, r.RestAddr2EN,r.RestTel,r.RestTaxID,r.RestLine1,r.RestLine2,r.RestTAXRD);
            }

            return result;

        }


        public Payment getTrnPayment(int trnID)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_TrnBill_Pay(trnID, this.branchID);

            Payment result = new Payment();

            foreach (var r in strQuery)
            {
                result = new Payment((int)r.TrnID, r.TrnDate, (float)r.PayAmount, (int)r.PayCustID, r.PayCustName, r.PayRemark, r.OrderDateTime.ToString(), r.CreateDate.ToString(), (int)r.TableID, r.TableName, (int)r.TaxInvID);
            }

            return result;

        }

        public List<Transaction> getTrnOrder(int trnID)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_TrnBill_Order(trnID, this.branchID);

            List<Transaction> result = new List<Transaction>();

            foreach (var r in strQuery)
            {
                result.Add(new Transaction((int)r.TrnID, (int)r.GroupCatID, (int)r.ProductID, r.ProductName, r.ProductNameEN, r.TrnRemark, (float)r.SalesQTY, (float)r.SalesAmount));
            }

            return result;
        }





        public List<Transaction> getTrnOrderByDay(string trnDate)
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_TrnBill_OrderByDay(trnDate, this.branchID);

            List<Transaction> result = new List<Transaction>();

            foreach (var r in strQuery)
            {
                result.Add(new Transaction((int)1, (int)r.GroupCatID, (int)r.ProductID, r.ProductName, r.ProductNameEN, "", (float)r.SalesQTY, (float)r.SalesAmount));
            }

            return result;
        } 
  

        public List<TrnDate> getTrnPeriod()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_TranPeriod(this.branchID);

            List<TrnDate> result = new List<TrnDate>();

            foreach (var r in strQuery)
            {
                result.Add(new TrnDate( r.TrnPeriodID, r.TrnPeriod ));
            }

            return result;
        }

        public List<TrnDate> getTrnDate()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_TranDate(this.branchID);

            List<TrnDate> result = new List<TrnDate>();

            foreach (var r in strQuery)
            {
                result.Add(new TrnDate(r.TrnPeriodID, r.TrnPeriod,r.TrnDateID,r.TrnDate));
            }

            return result;
        }

         public TrnMax getTrnMax()
        {

            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_TrnMaxData(this.branchID);

            TrnMax result = new TrnMax();

            foreach (var r in strQuery)
            {
                result = new TrnMax(r.MaxPeriod, r.MaxDate,(int)r.MaxTrnID);
            }

            return result;
        }

         public List<Bill> getTrnBillByDate(string dateYYYYMMDD)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_TrnBillByDate(dateYYYYMMDD ,this.branchID);

             List<Bill> result = new List<Bill>();

             foreach (var r in strQuery)
             {
                 result.Add(new Bill( (int) r.BillID,r.BillNo));
             }

             return result;
         }



         public DataTable getSum_SalesPaymentTaxByDate_GroupDate_Type(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_SalesPaymentTaxByDate_GroupDate_Type(fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }



         public DataTable getSum_SalesPaymentTaxByDate_GroupDate(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_SalesPaymentTaxByDate_GroupDate(fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }




         public DataTable getSum_SalesPaymentTaxByDate(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_SalesPaymentTaxByDate( fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }



         public DataTable getSum_LogDelOrder(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_LogDelOrder(this.branchID,fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }










         public DataTable getSum_SalesProduct(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_SalesProduct(this.branchID, fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getSum_TrnPayment(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();


                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_TrnPayment(this.branchID, fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getSum_CreditCust(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_CreditCust(this.branchID, fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);  

             }
             catch (Exception ex)
             {
                 result = new DataTable();
                 
             }

             return result;
         }

         public DataTable getSum_CreditCustDetail(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_CreditCustDetail(this.branchID, fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getSum_TrnDay(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_TrnDay(this.branchID, fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getSum_TrnMonth(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_TrnMonth(this.branchID, fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getSum_Working(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_Sum_Working(this.branchID, Int32.Parse(fromDate), Int32.Parse(toDate),0);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getSum_WorkingDetail(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_WorkingDetail(this.branchID, Int32.Parse(fromDate), Int32.Parse(toDate), 0);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public DataTable getSum_StockReport(string fromDate, string toDate,int invID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();


                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_StockReport(this.branchID, fromDate, toDate, invID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public List<MemWork> getWorkInOut()
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_WorkInOut(this.branchID);

             List<MemWork> result = new List<MemWork>();

             foreach (var r in strQuery)
             {
                 result.Add(new MemWork((int) r.UserID,r.Name,r.Status,(int)r.WorkShift,r.WorkIn,r.WorkOut, (float) r.WorkTime));
             }

             return result;
         }

         public List<CustPayment> getCustPayment()
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_CreditCustPay(this.branchID);

             List<CustPayment> result = new List<CustPayment>();

             foreach (var r in strQuery)
             {
                 result.Add(new CustPayment((int) r.CustID , r.Customer,r.CreditAmount,r.PayAmount,r.BalanceAmount));
             }

             return result;
         }





         public DataTable getCustPaymentDataTable()
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_CreditCustPay(this.branchID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         
         public DataTable getCustPayByCust(int custID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_CreditCustPayByCust(this.branchID, custID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getCustPaymentDate(int custID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_CreditCustPaymentDate(this.branchID, custID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getStockAnalyze(string stockdate, int flagRecal, int invID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_StockAnalyz(stockdate,flagRecal,invID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }
         

         public int instNewAddStock(int storeID,string dateAddStock , float qty , float amt , string remark ,string barcode,string addtype ,int AddStockType)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_InsertNewAddStock(storeID, dateAddStock, qty, amt, remark, barcode, addtype, AddStockType, Login.userName);

             return (int)strQuery;

         }


         public int instNewAddStock_Ending(string dateAddStock)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_InsertNewAddStock_Ending( dateAddStock,  Login.userName);

             return (int)strQuery;

         }

         public List<Stock> getAllStock(int storeID, int invID)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_AllAddStock(storeID, invID);

             List<Stock> result = new List<Stock>();

             foreach (var r in strQuery)
             {
                 result.Add(new Stock(r.POOrderNo, (int)r.AddStockID, r.AddStockDate, (int)r.StoreID, (int)r.StoreLotID, r.StoreName, (float)r.StoreBigQTY ,r.BigUnit, (float)r.StoreQTY,r.Unit, (float)r.StoreAmount, r.StoreRemark, r.AddType, (DateTime)r.CreateDate));
             }

             return result;
         }


         public DataTable getDataAllStock(int storeID,int invID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_AllAddStock(storeID, invID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public List<Coupon> getAllCoupon(int couponID, string couponCode, string memCardID)
         {

            // Cloud Direct
            DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

            var strQuery = db.Sp_App_AllCoupon(couponID, couponCode, memCardID);

             List<Coupon> result = new List<Coupon>();

             foreach (var r in strQuery)
             {
                 result.Add(new Coupon((int)r.CouponID, r.CouponCode, r.CouponName, r.CouponDesc, r.CouponFromDate, r.CouponToDate, r.ConponSynTax, (int)r.CouponUserID, r.CouponRemark, r.CouponUsed, r.CouponFlaguse, r.CouponFlagExpire, r.CouponTextShow));
             }

             return result;
         }

         public DataTable getAllCoupon_DATA(int couponID, string couponCode, string memCardID)
         {
             DataTable result = null;

             try
             {
                 // Cloud Direct
                 DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

                 var strQuery = db.Sp_App_AllCoupon(couponID, couponCode, memCardID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public int updsCoupon_Used(string couponCode)
         {
            // Cloud Direct
            DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

             var strQuery = db.Sp_App_UpdateCoupon_USED(couponCode, Login.userName);

             return (int)strQuery;

         }

        public int updsCoupon_Used_ByCashier(string couponCode)
        {
            // Cloud Direct
            DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

            var strQuery = db.Sp_App_UpdateCoupon_USED_ByCashier(this.restID, couponCode);

            return (int)strQuery;

        }

        public int updsCoupon_nonUsed(string couponCode)
         {
            // Cloud Direct
            DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

             var strQuery = db.Sp_App_UpdateCoupon_NONUSED(couponCode, Login.userName);

             return (int)strQuery;

         }


         public int updsAddStock(string poOrderNo,int addStockID,int storeID, float qty , float amt , string remark, string addstocktype,int wHID)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_UpdateAddStock(poOrderNo,addStockID, storeID, qty, amt, remark, addstocktype,wHID);

             return (int)strQuery;

         }


         public List<ProductStock> getAllCatStock()
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_AllCatStock();

             List<ProductStock> result = new List<ProductStock>();

             foreach (var r in strQuery)
             {
                 result.Add(new ProductStock((int)r.ProductCatID, r.CatName));
             }

             return result;
         }


         public List<ProductStock> getAllProductStock(int catID)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_AllProductStock(catID);

             List<ProductStock> result = new List<ProductStock>();

             foreach (var r in strQuery)
             {
                 result.Add(new ProductStock ((int) r.ProductID,(int)r.ProductCatID,r.CatName,r.ProductName,r.StockType,r.StockMap ) );
             }

             return result;
         }


         public DataTable getAllProductStock_Data(int catID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_AllProductStock(catID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public List<MapSS> getAllMapSS(int productID,int storeID,string mapType)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_AllMapSS(productID, storeID, mapType);

             List<MapSS> result = new List<MapSS>();

             foreach (var r in strQuery)
             {
                 result.Add(new MapSS((int)r.MapSSID, (int)r.ProductCatID, r.CatName, (int)r.ProductID, r.ProductName, (int)r.StoreID, r.StoreName, (int)r.MapProductID, r.MapProductName, (int)r.MapStoreID, r.MapStoreName, (float)r.StoreQTY, r.MapStoreUnit ,r.MapProductUnit));
             }

             return result;
         }


         public int instNewMapSS(int productID, int stroeID, int mapproductID, int mapstroeID, float storeQTY, string remark,string mapType)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_InsertNewMapSS(productID, stroeID, mapproductID, mapstroeID, storeQTY, remark, mapType);

             return (int)strQuery;

         }

         public int updsMapSS(int mapSSID, float storeQTY, string remark)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_UpdateMapSS(mapSSID, storeQTY, remark);

             return (int)strQuery;

         }

        ////// Mem Card 

         public List<MemCard> getAllMemCard(string memCardID)
         {

             //  Cloud Direct
             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

             var strQuery = db.Sp_MC_SelectMemCard(memCardID, this.groupRestID);

             List<MemCard> result = new List<MemCard>();

             foreach (var r in strQuery)
             {
                 result.Add(new MemCard((int)r.GroupRestID, (int)r.RestID, r.MemCardID,r.CardID, r.Name, r.Sex, (DateTime)r.BirthDate, r.tel, r.Address, r.Email, (int)r.Point, (int)r.PointPeriod, (float)r.PAmount, (float)r.PAmountPeriod, r.PromotionCode, r.PromotionDesc, r.PromotionDesc, r.AdviserID, r.AdviserName, (DateTime)r.ApplicationDate, (DateTime)r.RenewDate, (DateTime)r.ExpireDate, r.FlagExpire, r.CreateByRest, (int)r.CCBalanceAmt, (int)r.CCBalanceUnit, r.CCLastPOS, (int)r.CCStatus));
             }

             return result;

         }

         public DataTable getAllMemCardDataTable(string memCardID)
         {
             DataTable result = null;

             try
             {

                 //  Cloud Direct
                 DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

                 var strQuery = db.Sp_MC_SelectMemCard(memCardID,this.groupRestID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }



         public MemCard SelMemCard_Search(string strsearch, string statusRestAllUse)
         {
             //  Cloud Direct
             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);


             var strQuery = db.Sp_MC_SelectMemCard_Search(strsearch, this.groupRestID, statusRestAllUse);

             MemCard result = new MemCard();

             foreach (var r in strQuery)
             {
                 result = new MemCard((int)r.GroupRestID, (int)r.RestID, r.MemCardID,r.CardID, r.Name, r.Sex, (DateTime)r.BirthDate, r.tel, r.Address, r.Email, (int)r.Point, (int)r.PointPeriod, (float)r.PAmount, (float)r.PAmountPeriod, r.PromotionCode, r.PromotionDesc, r.PromotionSynTax, r.AdviserID, r.AdviserName, (DateTime)r.ApplicationDate, (DateTime)r.RenewDate, (DateTime)r.ExpireDate, r.FlagExpire, r.CreateByRest, (int)r.CCBalanceAmt, (int)r.CCBalanceUnit, r.CCLastPOS, (int)r.CCStatus);
             }

             return result;

         }

         public int instMemCard(string memCardID ,string cardID , string name, string sex, DateTime birthDate, string tel, string address, string email, int point, string promotionCode, string adviceID, DateTime applicationDate, DateTime renewDate)
         {
             //  Cloud Direct
             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);


             var strQuery = db.Sp_MC_InsertMemCard(this.groupRestID, this.restID, memCardID,cardID, name, sex, birthDate, tel, address, email, point, promotionCode, adviceID, applicationDate, renewDate, Login.userName);

             return (int)strQuery;

         }

         public int updsMemCard(string memCardID, string cardID, string name, string sex, DateTime birthDate, string tel, string address, string email, int point, string promotionCode, string adviceID, DateTime applicationDate, DateTime renewDate)
         {
             //  Cloud Direct
             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

             var strQuery = db.Sp_MC_UpdateMemCard(this.groupRestID, this.restID, memCardID, cardID, name, sex, birthDate, tel, address, email, point, promotionCode, adviceID, applicationDate, renewDate, Login.userName);

             return (int)strQuery;

         }


         public List<Promotion> getAllPromotion(string promotionCode)
         {
             //  Cloud Direct
             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

             var strQuery = db.Sp_MC_SelectPromotion(promotionCode, this.groupRestID);

             List<Promotion> result = new List<Promotion>();

             foreach (var r in strQuery)
             {
                 result.Add(new Promotion(r.PromotionCode,r.PromotionDesc,r.PromotionSynTax,r.FlagUse ));
             }

             return result;

         }

         public int instPromotion(string promotionCode, string promotionDesc, string promotionSyntax, string flagUse)
         {
             //  Cloud Direct
             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

             var strQuery = db.Sp_MC_InsertPromotion(promotionCode, promotionDesc, promotionSyntax, flagUse, this.groupRestID, this.restID, Login.userName);

             return (int)strQuery;
             
         }

         public int updsPromotion(string promotionCode, string promotionDesc, string promotionSyntax, string flagUse)
         {
             //  Cloud Direct
             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

             var strQuery = db.Sp_MC_UpdatePromotion(promotionCode, promotionDesc,promotionSyntax, flagUse, this.groupRestID, this.restID, Login.userName);

             return (int)strQuery;

         }


         public DataTable getTrnPointByMemCard(string memCardID)
         {
             DataTable result = null;

             try
             {

                 //  Cloud Direct
                 DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

                 var strQuery = db.Sp_MC_SelectPoint_ByMemCard(memCardID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public int updsMemCardRenew(string memCardID)
         {
             //  Cloud Direct
             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

             var strQuery = db.Sp_MC_UpdateMemCardRenew(this.groupRestID, this.restID, memCardID, Login.userName);

             return (int)strQuery;

         }

         


         public List<CashCard> getCashCard(string ccCode)
         {

             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

            var strQuery = db.Sp_App_AllCashCard(ccCode);

             List<CashCard> result = new List<CashCard>();

             foreach (var r in strQuery)
             {
                 result.Add(new CashCard((int)r.CCID, r.CCCode, (int)r.CCType, (int)r.CCGroupID, (int)r.CCDeposit, (int)r.CCBalanceAmt, (int)r.CCBalanceUnit , (int) r.CCLastPayType , r.PayTypeName , (int) r.CCStatus , r.CCStatusDesc , r.CCStartDate, r.CCLastUpdate, r.CCExpireDate , r.CCFlagExpire , r.CCLastPOS, r.CCRemark, r.CreateBy, (DateTime)r.CreateDate, r.UpdateBy, (DateTime)r.UpdateDate));
             }

             return result;

         }

         public int updsCashCard(string cCCode,int cCType,int cCTrnType,int cCgroupID, int cCDeposit , int cCAmt , int cCUnit , int ccPayType, string remark)
         {
             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

            var strQuery = db.Sp_App_UpdateCashCard(cCCode, cCType, cCTrnType, cCgroupID, cCDeposit, cCAmt, cCUnit, ccPayType, pOSIDConfig, remark, Login.userName);

             return (int)strQuery;

         }



         public DataTable getCC_Transaction(string fromDate, string toDate, int userID, string userName, int pramInt1, int pramInt2, string pramStr1, string pramStr2)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

                var strQuery = db.Sp_App_CC_Transaction(fromDate, toDate, userID, userName, pramInt1, pramInt2, pramStr1, pramStr2);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getCC_SumTranBy_POSCouter(string fromDate, string toDate, int userID, string userName, int pramInt1, int pramInt2, string pramStr1, string pramStr2)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

                db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_CC_SumTranBy_POSCouter(fromDate, toDate, userID, userName, pramInt1, pramInt2, pramStr1, pramStr2);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getCC_AllCashCard(string fromDate, string toDate, int userID, string userName, int pramInt1, int pramInt2, string pramStr1, string pramStr2)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

                var strQuery = db.Sp_App_CC_AllCashCard(fromDate, toDate, userID, userName, pramInt1, pramInt2, pramStr1, pramStr2);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getAllProduct(int catID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_ProductByCat(catID,0,0);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public DataTable getAllStore(int storeCatID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_AllStore(storeCatID, 0 , "000");

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getAllStore_Search(int storeCatID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_AllStore_Search(storeCatID, 0, "000");

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

        public int updsFlagSendOrderByBarcode(string strorderBarcode, string updateByUserName, int checkType)
        {
            DataRestDataContext db = new DataRestDataContext();

            string orderBarcode = strorderBarcode.Replace('I', ' ').Replace('P', ' ').Trim();
            string updateType = strorderBarcode.Right(1);

            var strQuery = db.Sp_App_UpdateFlagSendByOrderBarcode(orderBarcode, updateType, Login.userID, updateByUserName, checkType);

            return (int)strQuery;

        }

        public string updsFlagSendOrderByBarcodeMsg(string strorderBarcode, string updateByUserName, int checkType)
        {
            DataRestDataContext db = new DataRestDataContext();

            string orderBarcode = strorderBarcode.Replace('I', ' ').Replace('P', ' ').Trim();
            string updateType = strorderBarcode.Right(1);

            var strQuery = db.Sp_App_UpdateFlagSendByOrderBarcodeMsg(orderBarcode, updateType, Login.userID, updateByUserName, checkType);

            string result = "";

            foreach (var r in strQuery)
            {
                result = r.ScanOrderStatus;
            }

            return result;

        }


        public int instCashDrawer(string cashDate,int cashShiftID,string cashDesc,float cashAmount,string cashRemark,string cashUseby)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_InsertNewCashDrawer(cashDate, cashShiftID, cashDesc, cashAmount, cashRemark, pOSIDConfig, cashUseby, Login.userName);

             return (int)strQuery;

         }

         public int getCashDrawerShiftIDByPOSID(string cashDate)
         {
             DataRestDataContext db = new DataRestDataContext();
  
             var strQuery = db.Sp_App_SelectCashDrawerShift(cashDate, pOSIDConfig);

             return (int)strQuery;

         }


         public DataTable getAllCashDrawerData(string cashDate, int cashShiftID,int sign)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_AllCashDrawer(cashDate,cashShiftID, pOSIDConfig ,sign);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public List<SalesToday> getCashDrawerEndDay(int typeID, string cashDate,int shiftID)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_CashDrawerShiftEnd(this.branchID, typeID, cashDate, shiftID, pOSIDConfig);

             List<SalesToday> result = new List<SalesToday>();

             foreach (var r in strQuery)
             {
                 result.Add(new SalesToday(r.LableDate, r.LableName, (float)r.LableUnit, (float)r.LableSales));
             }

             return result;

         }

        ////////////////

         public int updsMemPrice(int memberID, int  productID, float productVIPprice , int flagUpdated)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_UpdateMemPriceUSE(memberID, productID, productVIPprice, flagUpdated, Login.userName);

             return (int)strQuery;

         }

         public DataTable getAllMemPrice(int memberID,int productID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_AllMemberPrice(memberID, productID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

        ///////////////////////////////////////////////

         //////////////// PO Inventory

         public DataTable getAllPOHeader(string poNO)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_Inven_POHeader(poNO, Login.userStatus);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public int getAllPOCountAppredNext(string poNO)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_POCountAppredNext(poNO, Login.userStatus);

             return (int)strQuery;

         }


         public DataTable getAllPODetailData(string poNO, int storeID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_Inven_PODetail(poNO, storeID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public List<PODetail> getAllPODetail(string poNO, int storeID)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_PODetail(poNO, storeID);

             List<PODetail> result = new List<PODetail>();

             foreach (var r in strQuery)
             {
                 result.Add(new PODetail(r.PONo, (int)r.AddStockID, r.AddStockDate, (int)r.StoreID, r.StoreName, (float)r.StoreBigQTY , r.StoreBigUnit, (float)r.StoreQTY , r.StoreUnit, (float)r.StoreAmount, (float)r.StoreVat, r.StoreRemark, r.AddType, (DateTime)r.CreateDate));
             }

             return result;
         }

         //public int getNextPO(string poDate)
         //{
         //    DataRestDataContext db = new DataRestDataContext();

         //    var strQuery = db.Sp_App_Inven_NextPO(poDate);

         //    return (int)strQuery;

         //}

         public string getNextPO(string date)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_NextPO(Login.userID.ToString());

             string result = "";

             foreach (var r in strQuery)
             {
                 result = r.NextNo;
                 //result.Add(new Prom((int)r.PromID, r.PromCode, r.PromName, r.PromCheckitem, (int)r.PromCountItem, (float)r.PromAmountBill, r.PromAutoUse, (int)r.ProductGroupID, r.PromDatefrom, r.PromDateTo, r.Promtime1, r.Promtime2, r.Promtime3));
             }

             return result;
         }

         public int instNewPO(string poNO, int invenIDIN, int invenIDOut, string pOHeaderRemark, int storeID, string dateAddStock, float qty, float amt, float vat, string remark, string barcode, string addstocktype)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_InsertPO(poNO, invenIDIN, invenIDOut, pOHeaderRemark, storeID, dateAddStock, qty, amt, vat, remark, barcode, addstocktype, Login.userName);

             return (int)strQuery;

         }

         public List<Inventory> getAllInventory(int inven, string suplierTaxID, string suplierTelNo ,string flagSuplier)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_InvenName(inven,suplierTaxID,suplierTelNo,flagSuplier);

             List<Inventory> result = new List<Inventory>();

             foreach (var r in strQuery)
             {
                 result.Add(new Inventory((int)r.InventoryID, r.InventoryName,r.FlagSuplier,r.SuplierCode,r.SuplierCompanyName,r.SuplierAddr,r.SuplierTaxID,r.SuplierTelNo,r.SuplierFaxNo,(float)r.SuplierPercentGP, r.Remark,r.FlagUse));
             }

             return result;
         }


         public int getNextProcessPO(string poNo, string poRemark, int addNextPO)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_NextProcessPO(poNo, Login.userStatus, Login.userName, poRemark, addNextPO);

             return (int)strQuery;

         }

         public int updatePOHeaderRem(string poNo, string poRemark)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_UpdatePORem(poNo, Login.userName, poRemark);

             return (int)strQuery;

         }


         public int updatePODetail(string poNO, int addStockID, float qty, float amt, float vat, string remark, string storeType, int poProcessID)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_UpdatePODetail(poNO, addStockID, qty, amt, vat, remark, storeType, poProcessID, Login.userStatus, Login.userName);

             return (int)strQuery;

         }

         public List<TrnDate> getTrnPeriod_Stock()
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_TranPeriod_Stock(this.branchID);

             List<TrnDate> result = new List<TrnDate>();

             foreach (var r in strQuery)
             {
                 result.Add(new TrnDate(r.TrnPeriodID, r.TrnPeriod));
             }

             return result;
         }

         public List<TrnDate> getTrnDate_Stock()
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_TranDate_Stock(this.branchID);

             List<TrnDate> result = new List<TrnDate>();

             foreach (var r in strQuery)
             {
                 result.Add(new TrnDate(r.TrnPeriodID, r.TrnPeriod, r.TrnDateID, r.TrnDate));
             }

             return result;
         }


         public TrnMax getTrnMax_Stock()
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_TrnMaxData_Stock(this.branchID);

             TrnMax result = new TrnMax();

             foreach (var r in strQuery)
             {
                 result = new TrnMax(r.MaxPeriod, r.MaxDate, (int)r.MaxTrnID);
             }

             return result;
         }


         public int insertInven(string inventoryName, string flagSuplier, string suplierCode, string suplierCompanyName, string suplierAddr, string suplierTaxID, string suplierTelNo, string suplierFaxNo, float suplierPercentGP, string remark, string flagUse)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_InsertInvenName(inventoryName, flagSuplier, suplierCode,suplierCompanyName, suplierAddr, suplierTaxID, suplierTelNo,suplierFaxNo,suplierPercentGP, remark, flagUse);

             return (int)strQuery;

         }

         public int updateInven(int invenid, string inventoryName, string flagSuplier, string suplierCode, string suplierCompanyName, string suplierAddr, string suplierTaxID, string suplierTelNo, string suplierFaxNo, float suplierPercentGP, string remark, string flagUse)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_UpdateInven(invenid,inventoryName, flagSuplier, suplierCode, suplierCompanyName, suplierAddr, suplierTaxID, suplierTelNo, suplierFaxNo, suplierPercentGP, remark, flagUse);

             return (int)strQuery;

         }


         public DataTable getAllInvenData(int inven, string suplierTaxID, string suplierTelNo,string flagSuplier)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_Inven_InvenName(inven, suplierTaxID, suplierTelNo, flagSuplier);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public List<POType> getAllPOType(int pOTypeID,string pOSupplierType)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_POType(pOTypeID,pOSupplierType);

             List<POType> result = new List<POType>();

             foreach (var r in strQuery)
             {
                 result.Add(new POType((int)r.POTypeID, r.POTypeDESC,r.POSupplierType));
             }

             return result;
         }


         public DataTable getAllStoreTake(int branchid)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_Inven_AllStore(0, 0, "000", branchid);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         


         public DataTable getMappingMaterial(int tableID)
         {
             DataTable result = null;

             try
             {  
                  
                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_inven_MapProductStore(tableID, Login.userName);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();
             }

             return result;
         }
         
         ////////////////////// Promotion


         public DataTable getListProm_DATA(int promID)
         {
             DataTable result = null;

             try
             { 
                 DataRestDataContext db = new DataRestDataContext(); 
                 var strQuery = db.Sp_App_PM_GetListProm(promID); 
                 result = ClassConvert.LINQToDataTable(strQuery); 
             }
             catch (Exception ex)
             {
                 result = new DataTable();
             }

             return result;
         }

         public DataTable getListProductGroup_DATA(int productGroupID)
         {
             DataTable result = null;

             try
             {
                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_PM_GetListProGroup(productGroupID);
                 result = ClassConvert.LINQToDataTable(strQuery);
             }
             catch (Exception ex)
             {
                 result = new DataTable();
             }

             return result;
         }

        //

         public List<Prom> getListProm(int productGroupID)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PM_GetListProm(productGroupID);

             List<Prom> result = new List<Prom>();

             foreach (var r in strQuery)
             {
                 result.Add(new Prom((int)r.PromID, r.PromCode, r.PromName,  r.PromCheckitem, (int)r.PromCountItem, (float)r.PromAmountBill, r.PromAutoUse, (int)r.ProductGroupID, r.PromDatefrom, r.PromDateTo, r.Promtime1, r.Promtime2, r.Promtime3,(int)r.PromLimitItem,(int)r.PromBalanceItem,r.PromDayofWeek));
             }

             return result;
         }


         public List<ProductGroup> getListProductGroup(int productGroupID)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PM_GetListProGroup(productGroupID);

             List<ProductGroup> result = new List<ProductGroup>();

             foreach (var r in strQuery)
             {
                 result.Add(new ProductGroup((int)r.ProGroupID,r.ProductGroupCode,r.ProgroupName,r.ProgroupGroupCatID,r.ProgroupCatID,r.ProgroupProductID,r.ProgroupFlaguse ));
             }

             return result;
         }
        

        // Get Product list
         public List<Product> getProductGroup(int progroupID,string pram1)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PM_GetProductGroup(progroupID, pram1);

             List<Product> result = new List<Product>();

             foreach (var r in strQuery)
             {
                 result.Add(new Product((int)r.ProductID, (int)r.ProductCatID, r.ProductName, r.ProductNameEN, r.ProductUnit, r.ProductDesc, "",(float)r.ProductPrice, (float)r.ProductPrice2, (float)r.ProductPrice3, (float)r.ProductPrice4, (float)r.ProductPrice5, 0, r.ProductFlagUse, r.ProductImage, r.ProductFlagStock, r.StockType, (int)r.ProductPromID, (int)r.ProductGetPoint,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,"","","","","","","",0));
             }

             return result;
         }


         public List<PromDetail> getPromDetail(int promID, int promSeqNo)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PM_GetPromDetail(promID, promSeqNo);

             List<PromDetail> result = new List<PromDetail>();

             foreach (var r in strQuery)
             {
                 result.Add(new PromDetail((int)r.PromID,(int)r.PromSegNo,(int)r.ProductGroupID,r.ProgroupName,r.SetPriceGroup,(float)r.SetPrice,(float)r.DiscountPeritem,(float)r.DiscountAmtitem));
             }

             return result;
         }


         public int insertNewGroupProduct(  string productGroupCode, string progroupName, string progroupGroupCatID, string progroupCatID, string progroupProductID, string progroupFlaguse)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PM_InsertNewGroupProduct(productGroupCode,  progroupName,  progroupGroupCatID,  progroupCatID,  progroupProductID,  progroupFlaguse,Login.userName);

             return (int)strQuery;

         }


         public int updateGroupProduct(int productGeoupID, string productGroupCode, string progroupName, string progroupGroupCatID, string progroupCatID, string progroupProductID, string progroupFlaguse)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PM_UpdateGroupProduct(productGeoupID,productGroupCode, progroupName, progroupGroupCatID, progroupCatID, progroupProductID, progroupFlaguse, Login.userName);

             return (int)strQuery;

         }


         public int insertNewPromotion(string  promCode, string promName, string promCheckitem, int promCountItem, float promAmountBil, string promAutoUse, int productGroupID, string promDatefrom, string promDateTo, string promtime1, string promtime2, string promtime3, int promQtyLimits, int promQtyBalance,string promDayW)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PM_InsertNewPromotion( promCode,  promName,  promCheckitem,  promCountItem,  promAmountBil,  promAutoUse,  productGroupID,  promDatefrom,  promDateTo,  promtime1,  promtime2,  promtime3, promQtyLimits, promQtyBalance, promDayW, Login.userName);

             return (int)strQuery;

         }


         public int updateNewPromotion(int promID, string promCode, string promName, string promCheckitem, int promCountItem, float promAmountBil, string promAutoUse, int productGroupID, string promDatefrom, string promDateTo, string promtime1, string promtime2, string promtime3, int promQtyLimits, int promQtyBalance, string promDayW)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PM_UpdatePromotion(promID,promCode, promName, promCheckitem, promCountItem, promAmountBil, promAutoUse, productGroupID, promDatefrom, promDateTo, promtime1, promtime2, promtime3, promQtyLimits, promQtyBalance, promDayW, Login.userName);

             return (int)strQuery;

         }


         public int updatePromDetail(int promID, int promSegNo, int productGroupID, string setPriceGroup, float setPrice, float discountPeritem, float discountAmtitem)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PM_UpdatePromDetail( promID,  promSegNo,  productGroupID,  setPriceGroup,  setPrice,  discountPeritem,  discountAmtitem );

             return (int)strQuery;

         }


         public int instOrderByTable_Prom(int tableID, int productID, int memID, string remark, float addOrderAmt,int productproID,int productGroupID,int seqNo)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_InsertOrderByTable_Prom(tableID, productID, memID, this.branchID, remark, addOrderAmt, productproID, productGroupID, seqNo);

             return (int)strQuery;

         }

         public DataTable getAllMemMapSS_Final(int productID, int storeID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_AllMapSS_Final(productID, storeID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public int salesNextBillDay(string today)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_SalesNextBillDay(today);

             return (int)strQuery;

         }


        /// Bill All Payment
        /// 

         public DataTable getAllPayment(string fromDate,string toDate ,int zoneID , string shipper)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_TrnBill_AllPayment(fromDate, toDate, zoneID, shipper);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public List<CustPaymentByPay> getCustPaymentByPay(int CustPaymentID)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_CreditCustPay_Payment(CustPaymentID);

             List<CustPaymentByPay> result = new List<CustPaymentByPay>();

             foreach (var r in strQuery)
             {
                result.Add(new CustPaymentByPay((int)r.CustPaymentID,(int)r.TrnID,r.PayDate,(float) r.BillBalanceAmount,(float)r.PayAmount,(float)r.BalanceAmount,r.PayRemark));
             }

             return result;

         }

         public List<DiscountGroup> getAllDiscountGroup()
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_AllDiscountGroup();

             List<DiscountGroup> result = new List<DiscountGroup>();

             foreach (var r in strQuery)
             {
                 result.Add(new DiscountGroup((int)r.DiscountGroupID, r.DiscountGroupName, r.DiscountGroupNameTH, r.DiscountGroupNameEN, (int)r.SeqID, r.Flaguse));
             }

             return result;
         }


         public DataTable getHistPaymentByCust(string fromDate, string toDate, int custID, string memCardID, int type)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_TrnBill_ByCustMemCard(fromDate, toDate, custID, memCardID, type);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getHistTransactionByCust(string fromDate, string toDate, int custID, string memCardID, int type, int trnID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_TrnBill_ByCustMemCardOrder(fromDate, toDate, custID, memCardID, type, trnID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public DataTable getBook_AllBooking(int bookID, DateTime fromDate, DateTime toDate, int flagConfirm, int flagActive, int zoneID, int tableID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_Book_AllBooking(bookID, fromDate, toDate, flagConfirm, flagActive, zoneID, tableID);
                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public int insertBooking(DateTime fromDate, DateTime toDate, string bookTime, string memcardID, int custID, int tableID, string bookName, string bookTel, string bookDesc,float bookDeposit)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_Book_InsertBooking(fromDate, toDate, bookTime, memcardID, custID, tableID, bookName, bookTel, bookDesc, bookDeposit, Login.userName);

             return (int)strQuery;

         }

         public int updsBooking(int bookID, DateTime fromDate, DateTime toDate, string bookTime, string memcardID, int custID, int tableID, string bookName, string bookTel, string bookDesc, float bookDeposit, int bookConfirm, int bookActive)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_Book_UpdateBooking(bookID, fromDate, toDate, bookTime, memcardID, custID, tableID, bookName, bookTel, bookDesc, bookDeposit, bookConfirm, bookActive, Login.userName);

             return (int)strQuery;

         }

         public int updsBooking_Flag(int bookID, int flagBook, string bookType)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_Book_UpdateBooking_Flag(bookID, flagBook, bookType, Login.userName);

             return (int)strQuery;

         }

         public List<Supplier> getPC_Supplier(int supplierID, string supplierTaxID, string supplierTelNo)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_Supplier(supplierID, supplierTaxID, supplierTelNo);

             List<Supplier> result = new List<Supplier>();

             foreach (var r in strQuery)
             {
                 result.Add(new Supplier((int)r.SupplierID, r.SupplierName, r.SupplierCode, r.SupplierCompanyName, r.SupplierAddr, r.SupplierTaxID, r.SupplierTelNo, r.SupplierFaxNo, (float)r.SupplierPercentGP, r.Remark, r.FlagUse));
             }

             return result;
         }

         public DataTable getSum_ProductSupplier(int supplierID, string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_SalesProductBySupplier(supplierID, fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getSum_SalesProductByBill( string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 db.CommandTimeout = 300;

                 var strQuery = db.Sp_App_Sum_SalesProductByBill(this.branchID, fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public DataTable getPC_SupplierData(int inven, string suplierTaxID, string suplierTelNo)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_PC_Supplier(inven, suplierTaxID, suplierTelNo);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public DataTable getSum_SalesByMemCard(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_Sum_SalesByMemCard(this.branchID, fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getSum_SalesByMemCard_Total(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_Sum_SalesByMemCard_Total(this.branchID, fromDate, toDate);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public int insertPC_Supplier(string supplierName, string suplierCode, string suplierCompanyName, string suplierAddr, string suplierTaxID, string suplierTelNo, string suplierFaxNo, float suplierPercentGP, string remark, string flagUse)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_InsertSupplier(supplierName, suplierCode, suplierCompanyName, suplierAddr, suplierTaxID, suplierTelNo, suplierFaxNo, suplierPercentGP, remark, flagUse);

             return (int)strQuery;

         }

         public int updatePC_Supplier(int supplierID, string supplierName, string suplierCode, string suplierCompanyName, string suplierAddr, string suplierTaxID, string suplierTelNo, string suplierFaxNo, float suplierPercentGP, string remark, string flagUse)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_updateSupplier(supplierID, supplierName, suplierCode, suplierCompanyName, suplierAddr, suplierTaxID, suplierTelNo, suplierFaxNo, suplierPercentGP, remark, flagUse);

             return (int)strQuery;

         }

         // New PR PO


         public DataTable getPC_DataPRHeader(string PRCode, int status)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_PC_PRHeader(PRCode, status);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getPC_DataPRDetail(string PRCode, int storeID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_PC_PRDetail(PRCode, storeID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


       


        

         public string getPC_NewPRCode(string date)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_NextPR(Login.userID.ToString());

             string result = "";

             foreach (var r in strQuery)
             {
                 result = r.NextNo;
                 //result.Add(new Prom((int)r.PromID, r.PromCode, r.PromName, r.PromCheckitem, (int)r.PromCountItem, (float)r.PromAmountBill, r.PromAutoUse, (int)r.ProductGroupID, r.PromDatefrom, r.PromDateTo, r.Promtime1, r.Promtime2, r.Promtime3));
             }

             return result;
         }

         public int instPC_NewPR(string prCode, DateTime prDocDate, DateTime prDate, string quoNo, string obj, string prRemark, int sup1, int sup2, int sup3, string prBy, int storeID, string dateAddStock, float qty, float amt, float vat, string remark, string barcode, string addstocktype)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_InsertNewPR(prCode, prDocDate, prDate, quoNo, obj, prRemark, sup1, sup2, sup3, prBy, Login.userName, storeID, dateAddStock, qty, amt, vat, remark, barcode, addstocktype, Login.userName);

             return (int)strQuery;

         }


         public int updsPC_PRHeader(string prCode, DateTime prDocDate, DateTime prDate, string quoNo, string obj, string prRemark, int sup1, int sup2, int sup3, string prBy)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_UpdatePRHeader(prCode, prDocDate, prDate, quoNo, obj, prRemark, sup1, sup2, sup3, prBy, Login.userName);

             return (int)strQuery;

         }


         public int updatePC_PRDetail(string prCode, int addStockID, int storeID, float qty, float amt, float vat, string remark, string storeType)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_UpdatePRDetail(prCode, addStockID, storeID, qty, amt, vat, remark, storeType, Login.userStatus, Login.userName);

             return (int)strQuery;

         }


         public int updatePC_PRHeaderApproved(string prCode, string remarkApp, string approvedBy, int approvedStatus)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_updatePRHeaderApproved(prCode, remarkApp, approvedBy, approvedStatus);

             return (int)strQuery;

         }


         // PO

         public DataTable getPC_DataPRDetail_Ref(string PRCode, int storeID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_PC_PRDetail_Ref(PRCode, storeID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getPC_DataPOHeader(string POCode, int status)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_PC_POHeader(POCode, status);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public string getPC_NewPOCode(string date)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_NextPO(Login.userID.ToString());

             string result = "";

             foreach (var r in strQuery)
             {
                 result = r.NextNo;
                 //result.Add(new Prom((int)r.PromID, r.PromCode, r.PromName, r.PromCheckitem, (int)r.PromCountItem, (float)r.PromAmountBill, r.PromAutoUse, (int)r.ProductGroupID, r.PromDatefrom, r.PromDateTo, r.Promtime1, r.Promtime2, r.Promtime3));
             }

             return result;
         }


         public int instPC_NewPO(string poCode, DateTime poDocDate, DateTime poDate, string prRef, string poRemark, int poSupID, string poTermPayment, string poLevel, int poVATType, float poDiscount, string poBy, int storeID, string dateAddStock, float qty, float price, float amt, float vat, string remark, string barcode, string addstocktype, float storeconvertRate)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_InsertNewPO(poCode, poDocDate, poDate, prRef, poRemark, poSupID, poTermPayment, poLevel, poVATType, poDiscount, poBy, Login.userName, storeID, dateAddStock, qty, price, amt, vat, remark, barcode, addstocktype, storeconvertRate, Login.userName);

             return (int)strQuery;

         }


         public int updsPC_POHeader(string poCode, DateTime poDocDate, DateTime poDate, string prRef, string poRemark, int poSupID, string poTermPayment, string poLevel, int poVATType, float poDiscount, string poBy)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_UpdatePOHeader(poCode, poDocDate, poDate, prRef, poRemark, poSupID, poTermPayment, poLevel, poVATType, poDiscount, poBy, Login.userName);

             return (int)strQuery;

         }


         public DataTable getPC_DataPODetail(string poCode, int storeID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_PC_PODetail(poCode, storeID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public int updatePC_POHeaderApproved(string poCode, string remarkApp, string approvedBy, int approvedStatus)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_updatePOHeaderApproved(poCode, remarkApp, approvedBy, approvedStatus);

             return (int)strQuery;

         }

         public int updatePC_PODetail(string poCode, int addStockID, int storeID, float oldqty, float qty, float price, float amt, float vat, string remark, string storeType, float storeconvertRate)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_UpdatePODetail(poCode, addStockID, storeID, oldqty, qty, price, amt, vat, remark, storeType, storeconvertRate, Login.userStatus, Login.userName);

             return (int)strQuery;

         }

         // GR

         public string getPC_NewGRCode(string date)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_NextGR(Login.userID.ToString());

             string result = "";

             foreach (var r in strQuery)
             {
                 result = r.NextNo;
                 //result.Add(new Prom((int)r.PromID, r.PromCode, r.PromName, r.PromCheckitem, (int)r.PromCountItem, (float)r.PromAmountBill, r.PromAutoUse, (int)r.ProductGroupID, r.PromDatefrom, r.PromDateTo, r.Promtime1, r.Promtime2, r.Promtime3));
             }

             return result;
         }

         public DataTable getPC_DataGRHeader(string GRCode, int status)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_PC_GRHeader(GRCode, status);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public int instPC_NewGR(string grCode, DateTime grDate, string grRemark, int grINVID, int poSupID, string poRef, string grBy, int storeID, string dateAddStock, float qty, float price, float amt, float vat, string remark, string barcode, string addstocktype, float storeconvertRate)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_InsertNewGR(grCode, grDate, grRemark, grINVID, poSupID, poRef, grBy, Login.userName, storeID, dateAddStock, qty, price, amt, vat, remark, barcode, addstocktype, storeconvertRate, Login.userName);

             return (int)strQuery;

         }


         public DataTable getPC_DataGRDetail(string grCode, int storeID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_PC_GRDetail(grCode, storeID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public DataTable getPC_DataPODetail_Ref(string POCode, int storeID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_PC_PODetail_Ref(POCode, storeID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();
             }

             return result;
         }

         public DataTable getPC_Rpt_PRHeader(DateTime fromDate, DateTime toDate, int status, int supplierID)
         {
             DataTable result = null;
             try
             {
                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_PC_Rpt_PRHeader(fromDate, toDate, status, supplierID);
                 result = ClassConvert.LINQToDataTable(strQuery);
             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }
             return result;
         }

         public DataTable getPC_Rpt_PRDetail(DateTime fromDate, DateTime toDate, int status, int supplierID)
         {
             DataTable result = null;
             try
             {
                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_PC_Rpt_PRDetail(fromDate, toDate, status, supplierID);
                 result = ClassConvert.LINQToDataTable(strQuery);
             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }
             return result;
         }

         public DataTable getPC_Rpt_POHeader(DateTime fromDate, DateTime toDate, int status, int supplierID)
         {
             DataTable result = null;
             try
             {
                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_PC_Rpt_POHeader(fromDate, toDate, status, supplierID);
                 result = ClassConvert.LINQToDataTable(strQuery);
             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }
             return result;
         }

         public DataTable getPC_Rpt_PODetail(DateTime fromDate, DateTime toDate, int status, int supplierID)
         {
             DataTable result = null;
             try
             {
                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_PC_Rpt_PODetail(fromDate, toDate, status, supplierID);
                 result = ClassConvert.LINQToDataTable(strQuery);
             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }
             return result;
         }


         public DataTable getPC_Rpt_GRHeader(DateTime fromDate, DateTime toDate, int status, int supplierID)
         {
             DataTable result = null;
             try
             {
                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_PC_Rpt_GRHeader(fromDate, toDate, status, supplierID);
                 result = ClassConvert.LINQToDataTable(strQuery);
             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }
             return result;
         }

         public DataTable getPC_Rpt_GRDetail(DateTime fromDate, DateTime toDate, int status, int supplierID)
         {
             DataTable result = null;
             try
             {
                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_PC_Rpt_GRDetail(fromDate, toDate, status, supplierID);
                 result = ClassConvert.LINQToDataTable(strQuery);
             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }
             return result;
         }


       

         // TF

         public int updsTFHeader(string poOrderNo, int whsIDIN, int whsIDOut, string addType, string tfDate)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_Inven_UpdatePOHeader(poOrderNo, whsIDIN, whsIDOut, addType, tfDate);

             return (int)strQuery;

         }



         public int updatePC_GRHeaderApproved(string grCode, string remarkApp, string approvedBy, int approvedStatus)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_updateGRHeaderApproved(grCode, remarkApp, approvedBy, approvedStatus);

             return (int)strQuery;

         }


         public int updatePC_GRHeader(string grCode, DateTime grDate, string grRemark, string grPO_Ref, int grInvID, int grSupID)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PC_updateGRHeader(grCode, grDate, grRemark, grPO_Ref, grInvID, grSupID, Login.userName);

             return (int)strQuery;

         }

        //-- Add Group Cat

         public int instNewGroupCat(string groupCatName, string groupCatNameTH, string groupCatNameEN, int discountGroupID, string groupCatColour, int groupCatSort, string groupCatFlagUse)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_InsertNewGroupCat( groupCatNameTH, groupCatNameEN, discountGroupID ,groupCatColour,groupCatSort,groupCatFlagUse);

             return (int)strQuery;

         }

         public int updsGroupCat(int groupCatID, string groupCatName, string groupCatNameTH, string groupCatNameEN, int discountGroupID, string groupCatColour, int groupCatSort, string groupCatFlagUse)
        {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_UpdateGroupCat(groupCatID, groupCatNameTH, groupCatNameEN, discountGroupID, groupCatColour, groupCatSort, groupCatFlagUse);

            return (int)strQuery;

         }

         public List<SalesToday> getSalesByday_FromTo(int typeID, string fromDate, string toDate)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_SalesByDay_FromTo(this.branchID, typeID, fromDate, toDate);

             List<SalesToday> result = new List<SalesToday>();

             foreach (var r in strQuery)
             {
                 result.Add(new SalesToday(r.LableDate, r.LableName, (float)r.LableUnit, (float)r.LableSales));
             }

             return result;

         }

        /// PL DATA
        /// 
         public DataTable getPL_PLTransaction(string plDate ,string accountNo , int sign)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext(); 
                 var strQuery = db.Sp_App_PL_PLTransaction(plDate, accountNo, sign); 
                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public int instNewPLTransaction(string plDate, int accountNo ,string accountDesc ,string remark,float amount)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PL_insertPLTransaction(plDate,accountNo,accountDesc,remark,amount,Login.userName);

             return (int)strQuery;

         }

         public List<Account> getPL_AllAccName(int sign)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PL_AllGLAccount(sign);

             List<Account> result = new List<Account>();

             foreach (var r in strQuery)
             {
                 result.Add(new Account((int)r.AccountNo,r.AccountName,r.AccountName));
             }

             return result;

         }


         public List<Account> getPL_AllAccDesc(int accountNo)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_PL_AllGLAccDesc(accountNo);

             List<Account> result = new List<Account>();

             foreach (var r in strQuery)
             {
                 result.Add(new Account((int)r.AccountNo,r.AccountDesc,r.AccountDesc));
             }

             return result;

         }


         public DataTable getPL_Sum_PLSummary(string fromDate,string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_PL_Sum_PLSummary(this.branchID, fromDate, toDate);
                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public DataTable getPL_Sum_PLSummary_Detail(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_PL_Sum_PLSummary_Detail(this.branchID, fromDate, toDate);
                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }


         public DataTable getPL_Sum_PLTransaction(string fromDate, string toDate)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_PL_Sum_PLTransaction(this.branchID, fromDate, toDate);
                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getAllPaymentByPayType(int trnID)
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();

                 var strQuery = db.Sp_App_AllPaymentType(trnID);

                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public List<OrderStatus> getOrderStatus(int orderStatus, int tableID, int kitchenID)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_OrderStatus(orderStatus, tableID, kitchenID);

             List<OrderStatus> result = new List<OrderStatus>();

             foreach (var r in strQuery)
             {
                 result.Add(new OrderStatus((int)r.TrnID, r.OrderQ, r.OrderBarcode, (int)r.TableID, r.TableName, (int)r.TableZoneID, r.ZoneName, r.OrderName, r.OrderRemark, (float)r.OrderAmt, (float)r.OrderCal, (DateTime)r.GetOrderDatetime, r.GetOrderBy, r.CookOrderBy, (DateTime)r.CookOrderDatetime, (int)r.KitchenID));
             }

             return result;
         }

         public int updsOrderStatus(int orderStatus, string orderBarcode)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_UpdateOrderStatus(orderStatus, orderBarcode, Login.userName);

             return (int)strQuery;

         }


         public string getNextMemCard()
         {

             //  Cloud Direct
             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);

             var strQuery = db.Sp_MC_NextMemCardID(this.groupRestID);

             string result = "";


             foreach (var r in strQuery)
             {
                 result = r.NextMemCardID;
             }

             return result;

         }

         public int getcashDrawerStatus(int statusType)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_CashDrawerStatus(Login.userName, statusType);

             return (int)strQuery;

         }

         public List<TableStayOn> getAllTableStayON(int tableID)
         {

             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_AllTableStayOn(tableID);

             List<TableStayOn> result = new List<TableStayOn>();

             foreach (var r in strQuery)
             {
                 result.Add(new TableStayOn((int)r.TableID, r.TableName, r.StayonName));
             }

             return result;

         }

         public int updsTableStayOn(int tableID, string username)
         {
             DataRestDataContext db = new DataRestDataContext();

             var strQuery = db.Sp_App_UpdateTableStayOn(tableID, username);

             return (int)strQuery;

         }

         public DataTable getAllTableStatus()
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_AllTableStatus(this.branchID);
                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getAllTableOrder()
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_AllTableORDER(this.branchID);
                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getAllTableOrder_Today()
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_AllTableORDER_Today(this.branchID);
                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public DataTable getAllTableMoveLog()
         {
             DataTable result = null;

             try
             {

                 DataRestDataContext db = new DataRestDataContext();
                 var strQuery = db.Sp_App_AllTableMoveLog(this.branchID);
                 result = ClassConvert.LINQToDataTable(strQuery);

             }
             catch (Exception ex)
             {
                 result = new DataTable();

             }

             return result;
         }

         public int instMemCard_Point(string memCardID, int adjPoint, float payAmount, int trnID)
         {
             //  Cloud Direct
             DataRestDataContext db = new DataRestDataContext(global::AppRest.Properties.Settings.Default.CFS_POS_Local_WPConnectionString);


             var strQuery = db.Sp_MC_InsertPoint(this.groupRestID, this.restID, memCardID, adjPoint, payAmount, Login.userName, "LOCAL", trnID);

             return (int)strQuery;

         }


        public int instNewCustomer(string custCode, string taxID, string title, string name, string tel, string address, string status, string flagUse)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_InsertNewCustomer(custCode, taxID, title, name, tel, address, status, flagUse, Login.userName);

            return (int)strQuery;

        }

        public int updsCustomer(int custID,string custCode, string taxID, string title, string name, string tel, string address, string status, string flagUse)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_UpdateCustomer(custID,custCode, taxID, title, name, tel, address, status, flagUse, Login.userName);

            return (int)strQuery;

        }

        public DataTable getStaffOrder_Header(int tableID , int staffID , int Status)
        {
            DataTable result = null;

            try
            {

                DataRestDataContext db = new DataRestDataContext();

                var strQuery = db.Sp_App_StaffOrderHistory_Header(tableID, staffID, Status);

                result = ClassConvert.LINQToDataTable(strQuery);

            }
            catch (Exception ex)
            {
                result = new DataTable();

            }

            return result;
        }

        public DataTable getStaffOrder_Detail(int tableID, int staffID, int Status ,int OrderNo)
        {
            DataTable result = null;

            try
            {

                DataRestDataContext db = new DataRestDataContext();

                var strQuery = db.Sp_App_StaffOrderHistory_Detail(tableID, staffID, Status, OrderNo);

                result = ClassConvert.LINQToDataTable(strQuery);

            }
            catch (Exception ex)
            {
                result = new DataTable();

            }

            return result;
        }

        public int delStaffOrderConfirm(int table , int OrdersubID , int StaffID ,string reamrk)
        {
            DataRestDataContext db = new DataRestDataContext();

            var strQuery = db.Sp_App_StaffDeleteComfirmOrder(table,  OrdersubID,  StaffID,  reamrk);

            return (int)strQuery;

        }

        public int delCashDrawerShift(string cashDate, int cashShiftID, string cashDesc, float cashAmount, string cashRemark, string cashUseby)
        {
            DataRestDataContext db = new DataRestDataContext();

            db.CommandTimeout = 300;

            var strQuery = db.Sp_App_DeleteCashDrawerShift(cashDate, cashShiftID, cashDesc, cashAmount, cashRemark, pOSIDConfig, cashUseby, Login.userName);


            return (int)strQuery;

        }
        public DataTable getProductList(int catID)
        {
            DataTable result = null;

            try
            {

                DataRestDataContext db = new DataRestDataContext();

                var strQuery = db.Sp_App_ProductByCat_List(catID, 0, 0);

                result = ClassConvert.LINQToDataTable(strQuery);

            }
            catch (Exception ex)
            {
                result = new DataTable();

            }

            return result;
        }

        public DataTable getAllStoreTake_Ending(int branchid)
        {
            DataTable result = null;

            try
            {

                DataRestDataContext db = new DataRestDataContext();

                var strQuery = db.Sp_App_Inven_AllStore_Ending(0, 0, "000", branchid);

                result = ClassConvert.LINQToDataTable(strQuery);

            }
            catch (Exception ex)
            {
                result = new DataTable();

            }

            return result;
        }

        public DataTable getSum_StockReport_Ending(string fromDate, string toDate, int invID)
        {
            DataTable result = null;

            try
            {

                DataRestDataContext db = new DataRestDataContext();


                db.CommandTimeout = 300;

                var strQuery = db.Sp_App_Sum_StockReport_Ending(this.branchID, toDate);

                result = ClassConvert.LINQToDataTable(strQuery);

            }
            catch (Exception ex)
            {
                result = new DataTable();

            }

            return result;
        }

        public DataTable getAllStoreSuplier_Search(int storeCatID, int supplierID)
        {
            DataTable result = null;

            try
            {

                DataRestDataContext db = new DataRestDataContext();

                var strQuery = db.Sp_App_AllStoreSuplier_Search(storeCatID, 0, supplierID);

                result = ClassConvert.LINQToDataTable(strQuery);

            }
            catch (Exception ex)
            {
                result = new DataTable();

            }

            return result;
        }


    }
}
