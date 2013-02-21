using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace Paypaltillidtest
{
    public partial class Thanks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string authToken = "tA-JzPTWCw3GxN6vB56OQ1FYvLNuigCR2L-KnbcJk0yJTJ-K1Fspviquv4O";

                //read in txn token from querystring
                string txToken = Request.QueryString.Get("tx");


                string query = string.Format("cmd=_notify-synch&tx={0}&at={1}",
                                      txToken, authToken);

                // Create the request back
                string url = "https://sandbox.paypal.com/cgi-bin/webscr";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                // Set values for the request back
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = query.Length;

                // Write the request back IPN strings
                StreamWriter stOut = new StreamWriter(req.GetRequestStream(),
                                         System.Text.Encoding.ASCII);
                stOut.Write(query);
                stOut.Close();

                // Do the request to PayPal and get the response
                StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
                string strResponse = stIn.ReadToEnd();
                stIn.Close();

                // sanity check
                Label2.Text = strResponse;

                // If response was SUCCESS, parse response string and output details
                if (strResponse.StartsWith("SUCCESS"))
                {
                   /* PDTHolder pdt = PDTHolder.Parse(strResponse);
                    Label1.Text =
                        string.Format("Thank you {0} {1} [{2}] for your payment of {3} {4}!",
                        pdt.PayerFirstName, pdt.PayerLastName,
                        pdt.PayerEmail, pdt.GrossTotal, pdt.Currency);*/
                    Label1.Text = strResponse;
                }
                else
                {
                    Label1.Text = "Oooops, something went wrong...";
                }
            }
        }
    }
}