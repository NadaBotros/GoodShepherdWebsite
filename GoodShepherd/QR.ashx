<%@ WebHandler Language="C#" Class="QR" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Gma.QrCodeNet.Encoding;
using System.Drawing.Imaging;
using System.Drawing;
public class QR : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["QRText"] != null)
        {
            int width = 500;
            int height = 500;
            if (context.Request.QueryString["width"] != null)
                int.TryParse(context.Request.QueryString["width"], out width);
            if (context.Request.QueryString["height"] != null)
                int.TryParse(context.Request.QueryString["height"], out height);
            context.Response.ContentType = "image/jpeg";
            var QRText = context.Request.QueryString["QRText"];
            MemoryStream stream = new MemoryStream();
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = qrEncoder.Encode(QRText);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);

            Bitmap bmp = new Bitmap(width, height);
            var Grph = Graphics.FromImage(bmp);
            Grph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            Grph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Image img = Image.FromStream(stream);
            Grph.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height));

            bmp.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}