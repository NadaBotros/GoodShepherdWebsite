using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Sound : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["speakerId"] != null)
        {
            SpeakersManage obj = new SpeakersManage();
            Speaker ent = obj.LoadById(Request.QueryString["speakerId"].ToString());
            if (ent != null)
            {
                lblTitle.Text = Page.Title = "عظات الاجتماع | " + ent.SpeakerName;
                ;
            }
        }
    }

    public string FileSize(string FileName)
    {
        try
        {
            if (FileName.Length > 3)
            {
                if (File.Exists(Server.MapPath("~/files/audio/" + FileName)))
                {
                    FileInfo fnotes = new FileInfo(Server.MapPath("~/files/audio/" + FileName));
                    long size = fnotes.Length;
                    double newsize = 0;
                    if (size < 1024)
                        return size + " بايت";
                    else if (size >= 1024 && size < (1024*1024 - 1))
                    {
                        newsize = size/(1024.0);
                        return Math.Round(newsize, 1) + " ك بايت";
                    }
                    else if (size >= 1024*1024 && size < (1024*1024*1024 - 1))
                    {
                        newsize = size/(1024.0*1024.0);
                        return Math.Round(newsize, 1) + " ميجا بايت";
                    }
                    else if (size >= 1024*1024*1024)
                    {
                        newsize = size/(1024.0*1024.0*1024.0);
                        return Math.Round(newsize, 1) + " جيجا بايت";
                    }
                    else
                        return string.Empty;
                }
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
}