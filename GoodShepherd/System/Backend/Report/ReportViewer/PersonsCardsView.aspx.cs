using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using iTextSharp.text.pdf;
using System.Diagnostics;
namespace System.Backend
{
    public partial class PersonsCardsView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lstFields"] != null)
            {
                List<string> lstIds = (List<string>)Session["lstFields"];
                PersonManagement personManagement = new PersonManagement();
                var personsInfo = personManagement.GetByIds(lstIds).ToList();
                StringBuilder str = new StringBuilder();
                int count = 0;
                if (personsInfo != null && personsInfo.Count > 0)
                {
                    foreach (var person in personsInfo)
                    {
                        string[] name = person.PersonName.Split(' ');
                        if (name.Length > 2)
                        {
                            #region Divs Header
                            if (count % 10 == 0 )
                                str.Append("<div class='break'>");
                            if (count % 2 == 0)
                                str.Append("<div class='dvRight'>");
                            else
                                str.Append("<div class='dvLeft'>");
                            #endregion
                            str.Append("<table dir='rtl' style='padding-top:20px;padding-right:5px;padding-left:5px' >");
                            str.Append("<tr><td>");
                            str.Append("<table celpadding='2' style='font-weight: bold' width='230px'>");

                            str.Append("<tr><td>الاسم : " + name[0] + " " + name[1] + " " + name[2] + "</td></tr>");
                            str.Append("<tr><td style='padding-top:8px'>الكود : " + person.PersonCode + "</td></tr>");
                            str.Append("</table>");
                            str.Append("</td><td>");
                            if (!string.IsNullOrEmpty(person.UserImage))
                                str.Append("<img src='../../../../Images/S100_100/" + person.UserImage +
                                           "' style=\"width: 64px;padding-bottom:5px; height: 80px\" />");
                            else
                            {
                                str.Append("<img src='../../../../Images/Blank.PNG' style=\"width: 64px;padding-bottom:5px; height: 80px\" />");
                            }
                            str.Append("</td></tr>");
                            str.Append("<tr><td>");
                            str.Append("<span style=\"font-size: 26pt; font-family: 'Free 3 of 9'\">*" + person.PersonCode +
                                       "*</span>");


                            str.Append("</td><td>");
                            str.Append(
                                "<img height='70px' src='../../../../QR.ashx?width=70&height=70&QRText=http://athd.me/f.aspx%3Fd%3D" +
                                person.PersonCode + "'/>");
                            str.Append(
                                "<tr><td colspan='2' dir='ltr' align='center' style='font-size:11px'>Scan the QR code to access your account directly via mobile</td></tr>");
                            str.Append("</tr></table>");
                            #region Divs Footer
                            str.Append("</div>");
                            if (count % 10 == 0)
                                str.Append("</div>");
                            count++;
                            #endregion

                        }
                        //System.Diagnostics.Debug.WriteLine(person.PersonCode);
                    }

                }
                ltrInfo.Text = str.ToString();
            }
        }



  
    }
}