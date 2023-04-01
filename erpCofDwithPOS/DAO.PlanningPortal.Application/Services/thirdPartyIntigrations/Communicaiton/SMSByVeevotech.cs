using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Web;

namespace DAO.PlanningPortal.Application.Services.thirdPartyIntigrations.Communicaiton;
internal static class SMSByVeevotech
{
    public static void MapadnSend(string receiver, string textmessage)
    {
        string APIKey = "9cdea4412052b7e6e126f077344ee219";

        //string receiver = "923099025332";
        string sender = "8583";
        //string textmessage = "This is test SMS from VT API";
        string jsonResponse = SendSMS(APIKey, receiver, sender, textmessage);
        var c = jsonResponse;
        //Console.Read(); //if you want to keep console window open, in-case debuging in visual studio
    }
    public static string SendSMS(string APIKey, string receiver, string sender, string textmessage)
    {
        String URI = "https://api.veevotech.com/sendsms?" +
        "hash=" + APIKey +
        "&receivenum=" + receiver +
        "&sendernum=" + sender +
        "&textmessage=" + Uri.UnescapeDataString(textmessage); // Visual Studio 10-15 
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


