
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace AppRest
{
    /// <summary>
    /// Summary description for ClassConvert
    /// </summary>
    public static class ClassConvert
    {

        public static void Map<T>(this IEnumerable<T> source, Action<T> func)
        {
            foreach (T i in source)
                func(i);
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            var dt = new DataTable();
            var properties = typeof(T).GetProperties();
            dt.Columns.AddRange(properties.Select(x => new DataColumn(x.Name, x.PropertyType)).ToArray());
            source.Select(x => dt.NewRow().ItemArray = properties.Select(y => y.GetValue(x, null)).ToArray()).Map(x => dt.Rows.Add(x));
            return dt;
        }

        public static DataTable JsonToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others 
                //will follow 

                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static string ConvertDateMMDDYYYYToYYYYMMDD(string strDate)
        {
            string result = "";
            string day = "";
            string month = "";
            string year = "";


            string[]  arrayStr = strDate.Split('/');

            if (arrayStr[0].Length == 1)
            {
                day = "0" + arrayStr[0];
            }
            else
            {
                day =  arrayStr[0];
            }

            if (arrayStr[1].Length == 1)
            {
                month = "0" + arrayStr[1];
            }
            else
            {
                month = arrayStr[1];
            }

            year = arrayStr[2];

            result = year + month + day;
                

            return result;
        }

    }
}
