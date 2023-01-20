using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace GoodShepherd
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnChangeImages_Click(object sender, EventArgs e)
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/images/temp"));
            if (filePaths.Count() > 0)
            {
                foreach (var filePath in filePaths)
                {
                    Guid id = Guid.NewGuid();
                    string studentCode = Path.GetFileName(filePath).Split('.')[0];
                    string ImageFile = "Person" + id.ToString().Replace("-", "") +
                                       System.IO.Path.GetExtension(filePath);
                    File.Move(filePath, Server.MapPath("~/Images/ActualSize/" + ImageFile));
                    DAL.ImagesFact.ResizeWithCropResizeImage("", ImageFile, "Person");
                    PersonManagement obj = new PersonManagement();
                    obj.ChangeUserImageByCode(studentCode, ImageFile);
                }
            }
        }
    }
}