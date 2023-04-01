using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;


namespace DAO.PlanningPortal.Application.Services.thirdPartyIntigrations.Communicaiton
{
    public static class ShortMessageService
    {

        public static string MapAndSend(string receiver)
        {
            string MyUsername = "923099025332"; //Your Username At Sendpk.com 
            string MyPassword = "Test@0000"; //Your Password At Sendpk.com 
            string toNumber = receiver; //Recepient cell phone number with country code 
            string Masking = "SMS Alert"; //Your Company Brand Name 
            string MessageText = "SMS Sent using .Net";
            string jsonResponse = SendSMS(Masking, toNumber, MessageText, MyUsername, MyPassword);
            return jsonResponse;
            //Console.Read(); //to keep console window open if trying in visual studio 
        }
        public static string SendSMS(string Masking, string toNumber, string MessageText, string MyUsername, string MyPassword)
        {
            String URI = "http://sendpk.com" +
            "/api/sms.php?" +
            "username=" + MyUsername +
            "&password=" + MyPassword +
            "&sender=" + Masking +
            "&mobile=" + toNumber +
            "&message=" + Uri.UnescapeDataString(MessageText); // Visual Studio 10-15 
            //"//&message=" + System.Net.WebUtility.UrlEncode(MessageText);// Visual Studio 12 
            try
            {
                WebRequest req = WebRequest.Create(URI);
                WebResponse resp = req.GetResponse();
                var sr = new System.IO.StreamReader(resp.GetResponseStream());
                return sr.ReadToEnd().Trim();
            }
            catch (WebException ex)
            {
                var httpWebResponse = ex.Response as HttpWebResponse;
                if (httpWebResponse != null)
                {
                    switch (httpWebResponse.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return "404:URL not found :" + URI;
                            break;
                        case HttpStatusCode.BadRequest:
                            return "400:Bad Request";
                            break;
                        default:
                            return httpWebResponse.StatusCode.ToString();
                    }
                }
            }
            return null;
        }
    }
}