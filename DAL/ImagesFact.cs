using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Data;
using System.Drawing.Drawing2D;
using System.Web;
using DAL;
namespace DAL
{
    public class ImagesFact
    {
        public ImagesFact() { }
        public static Bitmap CovertToGrayScale(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
      {
         new float[] {.3f, .3f, .3f, 0, 0},
         new float[] {.59f, .59f, .59f, 0, 0},
         new float[] {.11f, .11f, .11f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
      });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
        public static bool ResizeWithCropResizeImage(string OldImageName, string NewImageName, string section)
        {
            if (!string.IsNullOrEmpty(section))
            {

                #region Target Dimensions
                dbDataContext db = new dbDataContext();
                DataTable dtDimensions = null;
                var query = (from sizes in db.ImagesSizes where sizes.Section == section select sizes).Distinct();
                dtDimensions = query.ToDataTable();
                #endregion Target Dimensions
                #region Delete Old Images
                //Delete Org Size.
                if (!string.IsNullOrEmpty(OldImageName))
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath("~/Images/ActualSize/") + OldImageName))
                        File.Delete(HttpContext.Current.Server.MapPath("~/Images/ActualSize/") + OldImageName);
                    //Delete All Versions
                    for (int i = 0; i < dtDimensions.Rows.Count; i++)
                    {
                        if (File.Exists(HttpContext.Current.Server.MapPath("~/Images/Colorful/" + dtDimensions.Rows[i]["FolderName"].ToString() + "/" + OldImageName)))
                            File.Delete(HttpContext.Current.Server.MapPath("~/Images/Colorful/" + dtDimensions.Rows[i]["FolderName"].ToString() + "/" + OldImageName));
                        if (File.Exists(HttpContext.Current.Server.MapPath("~/Images/GrayScale/" + dtDimensions.Rows[i]["FolderName"].ToString() + "/" + OldImageName)))
                            File.Delete(HttpContext.Current.Server.MapPath("~/Images/GrayScale/" + dtDimensions.Rows[i]["FolderName"].ToString() + "/" + OldImageName));
                    }
                }
                #endregion Delete Old Images
                #region Vars
                string ImageSourceUrl = HttpContext.Current.Server.MapPath("~/Images/ActualSize/") + NewImageName;
                int quality = 100;
                System.Drawing.Image ImageSourse = System.Drawing.Image.FromFile(ImageSourceUrl);
                int ImageOrgWidth = ImageSourse.Width;
                int ImageOrgHeight = ImageSourse.Height;
                int ImageNewHeight = ImageOrgHeight;
                int ImageNewWidth = ImageOrgWidth;
                string imgType = Path.GetExtension(ImageSourceUrl).Substring(1);
                int targetDim = -1;
                #endregion Vars
                if (dtDimensions != null)
                {
                    for (int i = 0; i < dtDimensions.Rows.Count; i++)
                    {
                        if (dtDimensions.Rows[i]["ResizeWidth"].ToString() == "False" || dtDimensions.Rows[i]["ResizeHeight"].ToString() == "False")
                        {
                            #region Width
                            if (dtDimensions.Rows[i]["ResizeWidth"].ToString() == "True" && dtDimensions.Rows[i]["ResizeHeight"].ToString() == "False")
                            {
                                targetDim = int.Parse(dtDimensions.Rows[i]["width"].ToString());

                                if (targetDim < ImageOrgWidth)
                                {
                                    ImageNewWidth = targetDim;
                                    float ratio = (float)ImageOrgWidth / (float)ImageNewWidth;
                                    ImageNewHeight = (int)(ImageOrgHeight / ratio);
                                }
                                else { }
                            }
                            #endregion Width
                            #region Height
                            if (dtDimensions.Rows[i]["ResizeWidth"].ToString() == "False" && dtDimensions.Rows[i]["ResizeHeight"].ToString() == "True")
                            {
                                targetDim = int.Parse(dtDimensions.Rows[i]["height"].ToString());
                                if (targetDim < ImageOrgHeight)
                                {
                                    ImageNewHeight = targetDim;
                                    float ratio = (float)ImageOrgHeight / (float)ImageNewHeight;
                                    ImageNewWidth = (int)(ImageOrgWidth / ratio);
                                }

                            }
                            #endregion Height
                            #region Generate image
                            using (Bitmap result = new Bitmap(ImageNewWidth, ImageNewHeight))
                            {
                                using (Graphics grphs = Graphics.FromImage(result))
                                {
                                    grphs.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                    grphs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                    grphs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                    grphs.DrawImage(ImageSourse, 0, 0, result.Width, result.Height);

                                    bool RoundCorner = bool.Parse(dtDimensions.Rows[i]["CurvedCorners"].ToString());
                                    int CornerRadius = 0;
                                    if (!string.IsNullOrEmpty(dtDimensions.Rows[i]["CornerRadius"].ToString()))
                                    {
                                        CornerRadius = int.Parse(dtDimensions.Rows[i]["CornerRadius"].ToString());
                                    }
                                    if (RoundCorner)
                                    {
                                        #region corner
                                        string NewFilename = Path.GetFileNameWithoutExtension(ImageSourceUrl) + ".png";

                                        GraphicsPath gpTopRight = new GraphicsPath();

                                        //Top Right
                                        gpTopRight.AddArc(result.Width - CornerRadius * 2, 0, CornerRadius * 2, CornerRadius * 2, 270, 90);
                                        gpTopRight.AddLine(result.Width - CornerRadius, 0, result.Width, 0);
                                        gpTopRight.AddLine(result.Width, 0, result.Width, CornerRadius);
                                        grphs.ExcludeClip(new Region(gpTopRight));

                                        //Top Left
                                        GraphicsPath gpTopLeft = new GraphicsPath();
                                        gpTopLeft.AddArc(0, 0, CornerRadius * 2, CornerRadius * 2, 180, 90);
                                        //gpTopLeft.AddLine(0, 0, CornerRadius * 2, 0);
                                        gpTopLeft.AddLine(0, 0, 0, CornerRadius * 2);
                                        grphs.ExcludeClip(new Region(gpTopLeft));

                                        //Bottom Left
                                        GraphicsPath gpBottomLeft = new GraphicsPath();
                                        gpBottomLeft.AddArc(0, result.Height - CornerRadius * 2, CornerRadius * 2, CornerRadius * 2, 90, 90);
                                        //gpBottomLeft.AddLine(CornerRadius * 2, 0,0, result.Height);
                                        gpBottomLeft.AddLine(0, result.Height - CornerRadius * 2, 0, result.Height);
                                        grphs.ExcludeClip(new Region(gpBottomLeft));

                                        //Bootom Right
                                        GraphicsPath gpBottomRight = new GraphicsPath();
                                        gpBottomRight.AddArc(result.Width - CornerRadius * 2, result.Height - CornerRadius * 2, CornerRadius * 2, CornerRadius * 2, 0, 90);

                                        gpBottomRight.AddLine(result.Width - CornerRadius * 2, result.Height, result.Width, result.Height);
                                        gpBottomRight.AddLine(result.Width, result.Height, result.Width, result.Height - CornerRadius * 2);
                                        grphs.ExcludeClip(new Region(gpBottomRight));
                                        #endregion
                                    }
                                }
                                // check the quality passed in
                                if ((quality < 0) || (quality > 100))
                                {
                                    string error = string.Format("quality must be 0, 100", quality);
                                    throw new ArgumentOutOfRangeException(error);
                                }

                                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                                imgType = imgType.ToLower() == "jpg" ? "jpeg" : imgType.ToLower();
                                string lookupKey = "image/" + imgType;
                                var jpegCodec = ImageCodecInfo.GetImageEncoders().Where(inc => inc.MimeType.Equals(lookupKey)).FirstOrDefault();
                                //create a collection of EncoderParameters and set the quality parameter
                                var encoderParams = new EncoderParameters(1);
                                encoderParams.Param[0] = qualityParam;
                                //save the image using the codec and the encoder parameter

                                string SaveFolder = "~/Images/";
                                if (dtDimensions.Rows[i]["ConvertToGrayScale"].ToString() == "True")
                                    SaveFolder += "grayscale";

                                SaveFolder = HttpContext.Current.Server.MapPath(SaveFolder);
                                if (!Directory.Exists(SaveFolder + "/" + dtDimensions.Rows[i]["FolderName"].ToString()))
                                    Directory.CreateDirectory(SaveFolder + "/" + dtDimensions.Rows[i]["FolderName"].ToString());

                                if (dtDimensions.Rows[i]["ConvertToGrayScale"].ToString() == "True")
                                {
                                    Bitmap GrayResult = CovertToGrayScale(result);
                                    GrayResult.Save(SaveFolder + "/" + dtDimensions.Rows[i]["FolderName"].ToString() + "/" + NewImageName, jpegCodec, encoderParams);
                                }
                                else
                                {
                                    result.Save(SaveFolder + "/" + dtDimensions.Rows[i]["FolderName"].ToString() + "/" + NewImageName, jpegCodec, encoderParams);
                                }
                            }
                            #endregion Generate image
                        }
                        else
                        {
                            #region Crop
                            int dbHeight = int.Parse(dtDimensions.Rows[i]["height"].ToString());
                            int dbWidth = int.Parse(dtDimensions.Rows[i]["width"].ToString());
                            #region Size
                            float RateW = (float)ImageOrgWidth / (float)dbWidth;
                            float RateH = (float)ImageOrgHeight / (float)dbHeight;
                            bool ResizeWithWidth = false;
                            if (dtDimensions.Rows[i]["AllowCrop"].ToString() == "True")
                            {
                                if (RateW <= RateH)
                                {
                                    ImageNewWidth = (int)(ImageOrgWidth / RateW);
                                    ImageNewHeight = (int)(ImageOrgHeight / RateW);
                                    ResizeWithWidth = true;
                                }
                                else
                                {
                                    ImageNewWidth = (int)(ImageOrgWidth / RateH);
                                    ImageNewHeight = (int)(ImageOrgHeight / RateH);
                                }
                            }
                            else
                            {
                                if (RateW >= RateH)
                                {
                                    ImageNewWidth = (int)(ImageOrgWidth / RateW);
                                    ImageNewHeight = (int)(ImageOrgHeight / RateW);
                                    ResizeWithWidth = true;
                                }
                                else
                                {
                                    ImageNewWidth = (int)(ImageOrgWidth / RateH);
                                    ImageNewHeight = (int)(ImageOrgHeight / RateH);
                                }
                            }
                            #endregion Size
                            #region Generate image
                            using (Bitmap result = new Bitmap(dbWidth, dbHeight))
                            {
                                Graphics grphs = Graphics.FromImage(result);
                                string lookupKey = string.Empty;
                                string NewFilename = Path.GetFileName(ImageSourceUrl);

                                grphs.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                grphs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                grphs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                bool RoundCorner = bool.Parse(dtDimensions.Rows[i]["CurvedCorners"].ToString());
                                int CornerRadius = 0;
                                if (!string.IsNullOrEmpty(dtDimensions.Rows[i]["CornerRadius"].ToString()))
                                {
                                    CornerRadius = int.Parse(dtDimensions.Rows[i]["CornerRadius"].ToString());
                                }
                                lookupKey = "image/png";
                                if (RoundCorner)
                                {
                                    #region corner
                                    NewFilename = Path.GetFileNameWithoutExtension(ImageSourceUrl) + ".png";

                                    GraphicsPath gpTopRight = new GraphicsPath();

                                    //Top Right
                                    gpTopRight.AddArc(result.Width - CornerRadius * 2, 0, CornerRadius * 2, CornerRadius * 2, 270, 90);
                                    gpTopRight.AddLine(result.Width - CornerRadius, 0, result.Width, 0);
                                    gpTopRight.AddLine(result.Width, 0, result.Width, CornerRadius);
                                    grphs.ExcludeClip(new Region(gpTopRight));

                                    //Top Left
                                    GraphicsPath gpTopLeft = new GraphicsPath();
                                    gpTopLeft.AddArc(0, 0, CornerRadius * 2, CornerRadius * 2, 180, 90);
                                    //gpTopLeft.AddLine(0, 0, CornerRadius * 2, 0);
                                    gpTopLeft.AddLine(0, 0, 0, CornerRadius * 2);
                                    grphs.ExcludeClip(new Region(gpTopLeft));

                                    //Bottom Left
                                    GraphicsPath gpBottomLeft = new GraphicsPath();
                                    gpBottomLeft.AddArc(0, result.Height - CornerRadius * 2, CornerRadius * 2, CornerRadius * 2, 90, 90);
                                    //gpBottomLeft.AddLine(CornerRadius * 2, 0,0, result.Height);
                                    gpBottomLeft.AddLine(0, result.Height - CornerRadius * 2, 0, result.Height);
                                    grphs.ExcludeClip(new Region(gpBottomLeft));

                                    //Bootom Right
                                    GraphicsPath gpBottomRight = new GraphicsPath();
                                    gpBottomRight.AddArc(result.Width - CornerRadius * 2, result.Height - CornerRadius * 2, CornerRadius * 2, CornerRadius * 2, 0, 90);

                                    gpBottomRight.AddLine(result.Width - CornerRadius * 2, result.Height, result.Width, result.Height);
                                    gpBottomRight.AddLine(result.Width, result.Height, result.Width, result.Height - CornerRadius * 2);
                                    grphs.ExcludeClip(new Region(gpBottomRight));
                                    #endregion
                                }
                                if (dtDimensions.Rows[i]["AllowCrop"].ToString() == "True")
                                {
                                    float ratio = (float)ImageOrgWidth / (float)ImageNewWidth;
                                    int yDifference = 0, xDifference = 0;
                                    int virtualHeight = (int)(ImageOrgHeight / ratio);
                                    int virtualWidth = (int)(ImageOrgWidth / ratio);
                                    if (virtualHeight >= ImageNewHeight)
                                    {
                                        yDifference = (virtualHeight - ImageNewHeight) / 2;
                                    }
                                    else
                                    {
                                        ratio = (float)ImageOrgHeight / (float)ImageNewHeight;
                                        virtualWidth = (int)(ImageOrgWidth / ratio);
                                        virtualHeight = (int)(ImageOrgHeight / ratio);
                                        if (virtualWidth >= ImageNewWidth)
                                        {
                                            xDifference = (virtualWidth - ImageNewWidth) / 2;
                                        }
                                    }
                                    grphs.DrawImage(ImageSourse, new Rectangle(0, 0, result.Width, result.Height), xDifference * ratio, yDifference * ratio, (result.Width * ratio), (result.Height * ratio), GraphicsUnit.Pixel);

                                }
                                else
                                {
                                    if (ResizeWithWidth)
                                    {
                                        int diff = (dbHeight - ImageNewHeight) / 2;
                                        grphs.DrawImage(ImageSourse, 0, diff, ImageNewWidth, ImageNewHeight);
                                    }
                                    else
                                    {
                                        int diff = (dbWidth - ImageNewWidth) / 2;
                                        grphs.DrawImage(ImageSourse, diff, 0, ImageNewWidth, ImageNewHeight);
                                    }
                                }

                                // check the quality passed in
                                if ((quality < 0) || (quality > 100))
                                {
                                    string error = string.Format("quality must be 0, 100", quality);
                                    throw new ArgumentOutOfRangeException(error);
                                }
                                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                                var jpegCodec = ImageCodecInfo.GetImageEncoders().Where(inc => inc.MimeType.Equals(lookupKey)).FirstOrDefault();
                                //create a collection of EncoderParameters and set the quality parameter
                                var encoderParams = new EncoderParameters(1);
                                encoderParams.Param[0] = qualityParam;
                                //save the image using the codec and the encoder parameter              
                                string SaveFolder = "~/Images/";
                                if (dtDimensions.Rows[i]["ConvertToGrayScale"].ToString() == "True")
                                    SaveFolder += "grayscale";

                                SaveFolder = HttpContext.Current.Server.MapPath(SaveFolder);
                                if (!Directory.Exists(SaveFolder + "/" + dtDimensions.Rows[i]["FolderName"].ToString()))
                                    Directory.CreateDirectory(SaveFolder + "/" + dtDimensions.Rows[i]["FolderName"].ToString());

                                if (dtDimensions.Rows[i]["ConvertToGrayScale"].ToString() == "True")
                                {
                                    Bitmap GrayResult = CovertToGrayScale(result);
                                    GrayResult.Save(SaveFolder + "/" + dtDimensions.Rows[i]["FolderName"].ToString() + "/" + NewImageName, jpegCodec, encoderParams);
                                }
                                else
                                {
                                    result.Save(SaveFolder + "/" + dtDimensions.Rows[i]["FolderName"].ToString() + "/" + NewImageName, jpegCodec, encoderParams);
                                }
                            }
                            #endregion Generate image
                            #endregion Crop Square
                        }

                    }
                }
            }
            return true;
        }

    }
}