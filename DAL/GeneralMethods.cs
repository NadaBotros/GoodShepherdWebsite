using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Net;
using System.Data;
namespace DAL
{
    public static class GeneralMethods
    {
        public static string ConvertToSearchWord(string word)
        {
            try
            {
                if (word == "")
                    return "";
                string x = "";
                foreach (char ch in word.ToCharArray())
                {
                    switch (ch)
                    {
                        case 'أ':
                            x += "[أ,آ,ا,إ]";
                            break;
                        case 'إ':
                            x += "[أ,آ,ا,إ]";
                            break;
                        case 'ا':
                            x += "[أ,آ,ا,إ]";
                            break;
                        case 'آ':
                            x += "[أ,آ,ا,إ]";
                            break;
                        case 'ؤ':
                            x += "[و,ؤ]";
                            break;
                        case 'و':
                            x += "[و,ؤ]";
                            break;
                        case 'ئ':
                            x += "[ي,ئ,ى]";
                            break;
                        case 'ى':
                            x += "[ي,ئ,ى]";
                            break;
                        case 'ي':
                            x += "[ي,ئ,ى]";
                            break;
                        case 'ة':
                            x += "[ه,ة]";
                            break;
                        case 'ه':
                            x += "[ه,ة]";
                            break;
                        default:
                            x += ch.ToString();
                            break;

                    }

                }
                return "%" + x + "%";
            }
            catch { return ""; }
        }
        public static string GetRecordInfo(object CreatedOn, object CreatedBy, object ModifiedOn, object ModifiedBy)
        {
            AdminManagement AdminManagementObj = new AdminManagement();
            string Data = "";
            Data += "<b>تم الاضافة<br>بواسطه </b>" + AdminManagementObj.LoadAdminName(CreatedBy.ToString()) + "&nbsp;";
            Data += "<b>فى : </b>" + ConvertToDateString(CreatedOn.ToString()) + "<br>";
            if (!string.IsNullOrEmpty(AdminManagementObj.LoadAdminName(ModifiedBy.ToString())))
            {
                Data += "<b>تم التعديل<br>بواسطه </b>" + AdminManagementObj.LoadAdminName(ModifiedBy.ToString()) + "&nbsp;";
                Data += "<b>فى : </b>" + ConvertToDateString(ModifiedOn.ToString());
            }
            return Data;
        }
        public static string ConvertToDateString(string Date)
        {
            try
            {
                DateTime dt = DateTime.Parse(Date);
                return dt.Day + "/" + dt.Month + "/" + dt.Year;
            }
            catch { return ""; }
        }
        public static string ConvertToArabicDateString(object Date)
        {
            try
            {
                DateTime dt = DateTime.Parse(Date.ToString());
                return dt.Day + "  " + MonthName(dt.Month) + "  " + dt.Year;
            }
            catch { return ""; }
        }
        public static string GetDate(object Year, object MonthNo)
        {
            try
            {
                switch (int.Parse(MonthNo.ToString()))
                {
                    case 1: return "يناير" + " " + Year;
                    case 2: return "فبراير" + " " + Year;
                    case 3: return "مارس" + " " + Year;
                    case 4: return "ابريل" + " " + Year;
                    case 5: return "مايو" + " " + Year;
                    case 6: return "يونيو" + " " + Year;
                    case 7: return "يوليو" + " " + Year;
                    case 8: return "اغسطس" + " " + Year;
                    case 9: return "سبتمبر" + " " + Year;
                    case 10: return "اكتوبر" + " " + Year;
                    case 11: return "نوفمبر" + " " + Year;
                    case 12: return "ديسمبر" + " " + Year;
                    default: return string.Empty;
                }
            }
            catch { return string.Empty; }
        }
        public static string MonthName(int m)
        {
            string day = "";
            if (m == 1)
                day += "يناير";
            else if (m == 2)
                day += "فبراير";
            else if (m == 3)
                day += "مارس";
            else if (m == 4)
                day += "ابريل";
            else if (m == 5)
                day += "مايو";
            else if (m == 6)
                day += "يونيو";
            else if (m == 7)
                day += "يوليو";
            else if (m == 8)
                day += "اغسطس";
            else if (m == 9)
                day += "سبتمبر";
            else if (m == 10)
                day += "اكتوبر";
            else if (m == 11)
                day += "نوفمبر";
            else if (m == 12)
                day += "ديسمبر";
            return day;
        }
        public static bool DeleteRestorVisible(object Active, string type)
        {
            try
            {
                return !(bool)Active == Convert.ToBoolean(type);
            }
            catch
            {
                return false;
            }
        }
        public static string GetYoutube(object Vurl)
        {
            if (string.IsNullOrEmpty(Vurl.ToString()))
                return string.Empty;
            else
            {
                try
                {
                    string CutStr = Vurl.ToString().Remove(0, Vurl.ToString().IndexOf("?v=") + 3);
                    string VideoYoutube = CutStr.Split('&')[0];
                    return "<iframe width=\"300\" height=\"225\" src=\"http://www.youtube.com/embed/" + VideoYoutube + "\" frameborder=\"0\" allowfullscreen></iframe>";
                }
                catch { return string.Empty; }
            }
        }
        public static string GetAudio(string AudioPath)
        {
            if (!string.IsNullOrEmpty(AudioPath))
            {
                StringBuilder media = new StringBuilder();
                media.AppendFormat("<audio controls><source src='{0}' type='audio/mpeg'>Your browser does not support the audio element.</audio>", AudioPath);
                return media.ToString();
            }
            return string.Empty;
        }
        public static string GetYoutubeBig(object Vurl)
        {
            if (string.IsNullOrEmpty(Vurl.ToString()))
                return string.Empty;
            else
            {
                try
                {
                    string CutStr = Vurl.ToString().Remove(0, Vurl.ToString().IndexOf("?v=") + 3);
                    string VideoYoutube = CutStr.Split('&')[0];
                    return "<iframe width=\"640\" height=\"480\" src=\"http://www.youtube.com/embed/" + VideoYoutube + "\" frameborder=\"0\" allowfullscreen></iframe>";
                }
                catch { return string.Empty; }
            }
        }
        public static string GetYoutubeThum(object Vurl)
        {
            if (string.IsNullOrEmpty(Vurl.ToString()))
                return string.Empty;
            else
            {
                try
                {
                    string CutStr = Vurl.ToString().Remove(0, Vurl.ToString().IndexOf("?v=") + 3);
                    string VideoYoutube = CutStr.Split('&')[0];
                    return "http://i1.ytimg.com/vi/" + VideoYoutube + "/default.jpg";
                }
                catch { return string.Empty; }
            }
        }
        public static string ArticleContent(string ArtId)
        {
            try
            {
                FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/system/backend/Articles/" + ArtId + ".dat"), FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string content = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                return content;
            }
            catch { return string.Empty; }
        }
        public static string FormatString(object str)
        {
            try
            {
                int len = 70;
                if (len != 0)
                {
                    if (str.ToString().Length >= len - 3)
                    {
                        int cut = len;
                        while (str.ToString()[cut] == ' ') { cut++; }
                        return (str.ToString().Substring(0, cut) + "...").Replace("\r\n", "<br />");
                    }
                    else
                    {
                        //more.Visible = false;
                        return str.ToString().Replace("\r\n", "<br />");
                    }
                }
                else { return str.ToString().Replace("\r\n", "<br />"); }
            }
            catch { return str.ToString(); }
        }
        public static string CutString(object Content, int CharNum)
        {
            string Result = "";
            if (Content != null && Content.ToString().Length > CharNum)
            {
                Result = Regex.Replace(Content.ToString(), "<.*?>", string.Empty);
                Result = Regex.Replace(Result, @"<img\s[^>]*>(?:\s*?</img>)?", "", RegexOptions.IgnoreCase);
                if (Result.Length > CharNum)
                {
                    Result = Result.Remove(CharNum);
                    int x = Result.LastIndexOf(' ');
                    Result = Result.Remove(x);
                    Result += " ..... ";
                }
            }
            else
            {
                Result = Content.ToString();
            }
            return Result;
        }
        public static string SendMessage(string mailTo, string mailFromDisplayName, string mailCc, string subject, string body, string EmailSignature)
        {
            // try
            {
                if (!string.IsNullOrEmpty(mailTo))
                {
                    string mailFrom = "info@.com";
                    SmtpClient client = new SmtpClient("relay-hosting.secureserver.net", 25);
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = false;
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(mailFrom, mailFromDisplayName);
                    mail.To.Add(mailTo);
                    if (!string.IsNullOrEmpty(mailCc))
                    {
                        mail.CC.Add(mailCc);
                    }
                    mail.Subject = subject;
                    mail.Body = "<table width=\"100%\"><tr><td align=\"left\" dir='ltr'><span style=\"font-size: 12pt;\">" + body + "</span></td></tr><tr><td align=\"center\">" + EmailSignature + "</td></tr></table>";

                    mail.IsBodyHtml = true;
                    client.Send(mail);
                }
                return mailTo;
            }
            // catch { return ""; }
        }
        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyz";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public static string CreateRandomName(int PasswordLength)
        {
            string _allowedChars = "abcdefghijkmnopqrstuvwxyz";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public static List<string> ConvertToPersonodes(string SearchWord)
        {
            return SearchWord.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();
        }
        public static DateTime GetEgyptTime()
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            return dateTime;
        }
        public static string MediaPlayer(string FilePath, int Width, int Height)
        {
            //http://flash-mp3-player.net/players/normal/documentation/

            if (string.IsNullOrEmpty(FilePath))
                return string.Empty;
            else              
                return string.Format("<audio id='player2' src='{0}' type='audio/mp3' controls='controls'></audio>", FilePath);
        }
        public static string ConvertToDate(object date)
        {
            if (date != null && !string.IsNullOrEmpty(date.ToString()))
                return DateTime.Parse(date.ToString()).ToString("dd/MM/yyyy");
            else
                return string.Empty;
        }
    }
}
