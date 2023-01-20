using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend
{
    public partial class ImportFromYoutubeChannel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        string cutText(string orgStr, string start, string end, bool Format)
        {
            int f = -1, s = -1;
            if (orgStr.IndexOf(start) == -1)
            {
                s = -1; f = -1;
                return string.Empty;
            }
            else
            {
                s = orgStr.IndexOf(start) + start.Length;//-1
            }
            f = orgStr.IndexOf(end, s, orgStr.Length - s); //- start.Length //- end.Length;
            // f = orgStr.IndexOf(end, s + start.Length, orgStr.Length - s - start.Length);
            string tagHTML = "";
            if (f != -1 && s != -1)
            {
                tagHTML = orgStr.Substring(s, f - s);//+ end.Length
                if (tagHTML != "" && Format == false)
                    tagHTML = System.Text.RegularExpressions.Regex.Replace(tagHTML, "<[^>]*>", string.Empty);
            }

            return tagHTML;

        }
        public void ExtractURLs()
        {
            string str = EditorCode.Content.Replace(Environment.NewLine, "");
            string RegexPattern = "<a[^>]*? href=\"(?<url>.*?)\"[^>]*?>(?<text>.*?)</a>";
            string msg = string.Empty;
            System.Text.RegularExpressions.MatchCollection matches
                = System.Text.RegularExpressions.Regex.Matches(str, RegexPattern,
                                                               System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            LibraryFilesManage libraryFilesManage = new LibraryFilesManage();
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                if (match.Value.Contains("pl-video-title-link yt-uix-tile-link yt-uix-sessionlink spf-link"))
                {
                    string href = "http://www.youtube.com" + match.Groups["url"].Value.Split('&')[0];
                    string title = match.Groups["text"].Value.Replace("DVD", "");
                    if (!title.Contains("الفيديو المحذوف") && !string.IsNullOrEmpty(href) && !title.Contains("Deleted"))
                        libraryFilesManage.Add(txtCatId.Text, title, "", "", "", "", "", href,
                                               Request.Cookies["UserWebsiteId"].Value);
                }
            }
            EditorCode.Content = txtCatId.Text = "";
            lblMessge.Text = msg;
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            ExtractURLs();
        }
    }
}