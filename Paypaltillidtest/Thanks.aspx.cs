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

                Label1.Text = txToken;
                Label2.Text = authToken;

               /*
                string url = "https://sandbox.paypal.com/cgi-bin/webscr";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

               
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = query.Length;

               
                StreamWriter stOut = new StreamWriter(req.GetRequestStream(),
                                         System.Text.Encoding.ASCII);
                stOut.Write(query);
                stOut.Close();

               
                StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
                string strResponse = stIn.ReadToEnd();
                stIn.Close();

               
                Label2.Text = strResponse;

               
                if (strResponse.StartsWith("SUCCESS"))
                {
                  
                    Label1.Text = strResponse;
                }
                else
                {
                    Label1.Text = "Oooops, something went wrong...";
                }*/
            }
        }
    }
}