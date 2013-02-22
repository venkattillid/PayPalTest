﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Paypaltillidtest
{
    public partial class Handler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string postUrl  = "https://sandbox.paypal.com/cgi-bin/webscr";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(postUrl);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            string ipnPost = strRequest;
            strRequest += "&cmd=_notify-validate";
            req.ContentLength = strRequest.Length;

            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://url:port#"));
            //req.Proxy = proxy;

            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();

            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            Response.Write(strResponse);
            // logging ipn messages... be sure that you give write permission to process executing this code
          /*  string logPathDir = ResolveUrl("Messages");
            string logPath = string.Format("{0}\\{1}.txt", Server.MapPath(logPathDir), DateTime.Now.Ticks);
            File.WriteAllText(logPath, ipnPost);
            //*/

            if (strResponse == "VERIFIED")
            {
                //check the payment_status is Completed
                //check that txn_id has not been previously processed
                //check that receiver_email is your Primary PayPal email
                //check that payment_amount/payment_currency are correct
                //process payment
            }
            else if (strResponse == "INVALID")
            {
                //log for manual investigation
            }
            else
            {
                //log response/ipn data for manual investigation
            }
        }
    }
}