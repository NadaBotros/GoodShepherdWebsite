using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DAL;
using System.Net.NetworkInformation;
namespace DrHisham
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                Application["ActiveUsers"] = 0;
            }
            catch { }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            try
            {
                //string BrowserName, Version, IPAddress,  url, CountryName = "", CountryCode = "", ImageUrl = string.Empty;
                //VisitorManagement _obj = new VisitorManagement();
         
                //IPAddress = HttpContext.Current.Request.UserHostAddress;
                //BrowserName = HttpContext.Current.Request.Browser.Browser;
                //Version = HttpContext.Current.Request.Browser.Version;
                //url = "http://ip2country.sourceforge.net/ip2c.php?format=XML&ip=" + IPAddress;
                //#region Country
                //System.Net.WebRequest myRequest = System.Net.WebRequest.Create(url);
                //myRequest.Timeout = 12000;
                //System.Net.WebResponse myResponse = myRequest.GetResponse();
                //System.IO.Stream rssStream = myResponse.GetResponseStream();
                //System.Xml.XmlDocument rssDoc = new System.Xml.XmlDocument();
                //rssDoc.Load(rssStream);
                //System.Xml.XmlNodeList rssItems = rssDoc.SelectNodes("lookup");
                //for (int i = rssItems.Count - 1; i >= 0; i--)
                //{
                //    #region node
                //    System.Xml.XmlNode rssDetail;
                //    rssDetail = rssItems.Item(i).SelectSingleNode("country_name");
                //    if (rssDetail != null)
                //    {
                //        CountryName = rssDetail.InnerText;

                //    }
                //    rssDetail = rssItems.Item(i).SelectSingleNode("country_code");
                //    if (rssDetail != null)
                //    {
                //        CountryCode = rssDetail.InnerText;
                //        ImageUrl = CountryCode.ToLower() + ".png";
                //    }
                //    #endregion
                //}
                //#endregion
                //string id = _obj.Add(CountryName, CountryCode, BrowserName, Version, IPAddress,  ImageUrl, 1);
                //Session["Loginuser"] = id;

                //--------------------------
                try
                {
                    Application.Lock();
                    Application["ActiveUsers"] = Convert.ToInt32(Application["ActiveUsers"]) + 1;
                    Application.UnLock();
                }
                catch { }
            }
            catch { }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                Application.Lock();
                Application["ActiveUsers"] = Convert.ToInt32(Application["ActiveUsers"]) - 1;              
                Application.UnLock();
            }
            catch { }
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}