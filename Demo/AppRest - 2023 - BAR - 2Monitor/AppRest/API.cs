using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Configuration;
using System.Data;
//using Newtonsoft.Json;

namespace AppRest
{
    class API
    {
        string urlAPI = ConfigurationSettings.AppSettings["AIgenURL"].ToString(); 

        public string AImemRegisterStart(string memCardID)
        {
            Root rt = new Root();
            string result = "";

            try
            {
                string url = urlAPI + "/registration/start?reference_id=" + memCardID;


                var request = (HttpWebRequest)WebRequest.Create(url); 

                request.Method = "GET"; 
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

               // rt = JsonConvert.DeserializeObject<Root>(responseString); 
                result = rt.data.job_id;
   

                return result;
            }
            catch (Exception ex)
            { 
                return ex.Message;
            }
        }




        public Root AImemRegister(string jobID)
        {
            Root rt = new Root(); 

            try
            {
                string url = urlAPI + "/registration?job_id=" + jobID;

                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

               // rt = JsonConvert.DeserializeObject<Root>(responseString);


                return rt;
            }
            catch (Exception ex)
            {
                return rt;
            }
        }


        public string AImemIdentificationStart()
        {
            Root rt = new Root();
            string result = "";

            try
            {
                string url = urlAPI + "/identification/start";


                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

              //  rt = JsonConvert.DeserializeObject<Root>(responseString);
                result = rt.data.job_id;


                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Root AImemIdentification(string jobID)
        {
            Root rt = new Root();

            try
            {
                string url = urlAPI + "/identification?job_id=" + jobID;

                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

              //  rt = JsonConvert.DeserializeObject<Root>(responseString);


                return rt;
            }
            catch (Exception ex)
            {
                return rt;
            }
        }
  

    }
}
