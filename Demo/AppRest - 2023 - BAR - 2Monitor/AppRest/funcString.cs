using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO.Ports;
using System.Configuration;
using System.Drawing;

namespace AppRest
{
    static class  FuncString
    {
        public static string GetFixedLengthString(string input, int length)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(input))
            {
                result = new string(' ', length);
            }
            else if (input.Length > length)
            {
                result = input.Substring(0, length);
            }
            else
            {
                result = input.PadRight(length);
            }

            return result;
        }

        public static Boolean IsNumeric(Object Expression)
        {

            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }

        public static string Right(this string sValue, int iMaxLength)
        {
            //Check if the value is valid
            if (string.IsNullOrEmpty(sValue))
            {
                //Set valid empty string as string could be null
                sValue = string.Empty;
            }
            else if (sValue.Length > iMaxLength)
            {
                //Make the string no longer than the max length
                sValue = sValue.Substring(sValue.Length - iMaxLength, iMaxLength);
            }

            //Return the string
            return sValue;
        }

        public static DateTime ToDateTime(this string s, string format = "ddMMyyyy", string cultureString = "tr-TR")
        {
            try
            {
                var r = DateTime.ParseExact(
                    s: s,
                    format: format,
                    provider: CultureInfo.GetCultureInfo(cultureString));
                return r;
            }
            catch (FormatException)
            {
                throw;
            } 
        }

        public static DateTime ToDateTime(this string s,
                    string format, CultureInfo culture)
        {
            try
            {
                var r = DateTime.ParseExact(s: s, format: format,
                                        provider: culture);
                return r;
            }
            catch (FormatException)
            {
                throw;
            } 

        }


        public static List<string> WordWrap(string input, int maxCharacters)
        {
            List<string> lines = new List<string>();

            if (!input.Contains(" ") && !input.Contains("\n"))
            {
                int start = 0;
                while (start < input.Length)
                {
                    lines.Add(input.Substring(start, Math.Min(maxCharacters, input.Length - start)));
                    start += maxCharacters;
                }
            }
            else
            {
                string[] paragraphs = input.Split('\n');

                foreach (string paragraph in paragraphs)
                {
                    string[] words = paragraph.Split(' ');

                    string line = "";
                    foreach (string word in words)
                    {
                        if ((line + word).Length > maxCharacters)
                        {
                            lines.Add(line.Trim());
                            line = "";
                        }

                        line += string.Format("{0} ", word);
                    }

                    if (line.Length > 0)
                    {
                        lines.Add(line.Trim());
                    }
                }
            }
            return lines;
        }

        public static string TextBetween(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }


        public static string displayPOSMonitorSecLine(string strLine1 ,string strLine2)
        {
            string result = "";
            SerialPort sport = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);

            string strline = "";
            string flagDisplaySecMonitor = ConfigurationSettings.AppSettings["MonitorSecLine"].ToString();


            try
            {
                if (flagDisplaySecMonitor == "Y")
                {
                    strline = strLine2.PadRight(20) + strLine1.PadRight(20);

                    sport.Open();
                    sport.Write(strline);
                    sport.Close();
                }
            }
            catch (Exception ex)
            {

                result = ex.Message;
                sport.Close();
            }

            return result;
        }

       

    }
}
