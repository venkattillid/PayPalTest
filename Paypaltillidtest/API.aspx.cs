using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Paypaltillidtest.com.paypal.sandbox.www;

namespace Paypaltillidtest
{
    public partial class API : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void oneTimeButton_Click(object sender, EventArgs e)
        {// build request
            SetExpressCheckoutRequestDetailsType reqDetails = new SetExpressCheckoutRequestDetailsType();

            // TODO: Be sure to update hosting address in Web.Config!!
            string hostingOn = ConfigurationManager.AppSettings["HostingPrefix"];

            reqDetails.ReturnURL = hostingOn + "/Success.aspx";
            reqDetails.CancelURL = hostingOn + "/Cancel.aspx";
            reqDetails.NoShipping = "1";
            reqDetails.OrderTotal = new BasicAmountType()
            {
                currencyID = CurrencyCodeType.USD,
                Value = "10.00"
            };

            SetExpressCheckoutReq req = new SetExpressCheckoutReq()
            {
                SetExpressCheckoutRequest = new SetExpressCheckoutRequestType()
                {
                    Version = UtilPayPalAPI.Version,
                    SetExpressCheckoutRequestDetails = reqDetails
                }
            };

            // query PayPal and get token
            SetExpressCheckoutResponseType resp = UtilPayPalAPI.BuildPayPalWebservice().SetExpressCheckout(req);
            UtilPayPalAPI.HandleError(resp);

            // redirect user to PayPal
            Response.Redirect(string.Format("{0}?cmd=_express-checkout&token={1}",
                ConfigurationManager.AppSettings["PayPalSubmitUrl"], resp.Token));

        }
    }
    public class UtilPayPalAPI
    {
        public static string Version
        {
            get { return ConfigurationManager.AppSettings["APIVersion"]; }
        }

        public static PayPalAPIAASoapBinding BuildPayPalWebservice()
        {
            // more details on https://www.paypal.com/en_US/ebook/PP_APIReference/architecture.html
            UserIdPasswordType credentials = new UserIdPasswordType()
            {
                Username = ConfigurationManager.AppSettings["APIUsername"],
                Password = ConfigurationManager.AppSettings["APIPassword"],
                Signature = ConfigurationManager.AppSettings["APISignature"],
            };

            PayPalAPIAASoapBinding paypal = new PayPalAPIAASoapBinding();
            paypal.RequesterCredentials = new CustomSecurityHeaderType()
            {
                Credentials = credentials
            };

            return paypal;
        }

        internal static void HandleError(AbstractResponseType resp)
        {
            if (resp.Errors != null && resp.Errors.Length > 0)
            {
                // errors occured
                throw new Exception("Exception(s) occured when calling PayPal. First exception: " +
                    resp.Errors[0].LongMessage);
            }
        }
    }
}