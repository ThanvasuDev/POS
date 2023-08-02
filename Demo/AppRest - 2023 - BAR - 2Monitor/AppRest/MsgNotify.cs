using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Configuration;
using System.Net.Security;

namespace AppRest
{
    public static class MsgNotify
    {
        // MsgNotify.lineNotify

        public static void lineNotify(string msg)
        {

       //    string token = "P6ZU5SnhSFqzGMGQlPraDozfO1ypeB2sXHliGzJPzc5";

            /* generate ToKEN With line https://notify-bot.line.me/my/  */
            try
            {
                  string token = ConfigurationSettings.AppSettings["LINEToken"].ToString();

                msg = msg.Replace("%"," percent.");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var postData = string.Format("message={0}", msg);
                var data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers.Add("Authorization", "Bearer "+token);

                using (var stream = request.GetRequestStream())stream.Write(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                 // .Show(ex.ToString());
            }
        }
    }


    
}
