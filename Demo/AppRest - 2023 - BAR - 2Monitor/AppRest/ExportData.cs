using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace AppRest
{
    class ExportData
    {
        // TO USE:
        // 1) include COM reference to Microsoft Excel Object library
        // add namespace...
        // 2) using Excel = Microsoft.Office.Interop.Excel;

        static string exportFullPath = ConfigurationSettings.AppSettings["PathExportResultName"].ToString();

         
        public static void Excel_FromDataTable_DT(System.Windows.Forms.DataGridView dt)
        {
            try
            {

                // creating Excel Application  
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                // creating new WorkBook within Excel application  
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                // see the excel sheet behind the program  
                app.Visible = true;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  



                worksheet = (_Worksheet)workbook.Sheets["Sheet1"];
                worksheet = (_Worksheet)workbook.ActiveSheet; 
                // changing the name of active sheet  
                worksheet.Name = "Exported";
                // storing header part in Excel  
                for (int i = 1; i < dt.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dt.Columns[i - 1].HeaderText;
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dt.Rows[i].Cells[j].Value.ToString();
                    }
                }
                // save the application  
                //if (File.Exists(exportFullPath))
                //{
                //    File.Delete(exportFullPath);
                //}

                workbook.SaveAs(exportFullPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                // Exit from the application  
                app.Quit();

            }
            catch (Exception ex)
            {


            }
        }


        public static void Excel_FromDataTable_DTT(System.Windows.Forms.DataGridView dt)
        {
            try
            {

                //dt.RowHeadersVisible = false;
                //dt.SelectAll();
                //DataObject dataObj = dt.GetClipboardContent();
                //if (dataObj != null)
                //    Clipboard.SetDataObject(dataObj);

                //Microsoft.Office.Interop.Excel.Application xlexcel;
                //Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                //Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                //object misValue = System.Reflection.Missing.Value;
                //xlexcel = new Excel.Application();
                //xlexcel.Visible = true;
                //xlWorkBook = xlexcel.Workbooks.Add(misValue);
                //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                //Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                //CR.Select();
                //xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);          
    

            }
            catch (Exception ex)
            {


            }
        }








    }
}
