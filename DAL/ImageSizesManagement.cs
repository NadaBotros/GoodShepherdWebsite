using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DAL
{
    public class ImagesSizesManagement
    {
        #region Variables
        dbDataContext db;
        ImagesSize _Imgsize;
        #endregion
        public ImagesSizesManagement()
        {
            db = new dbDataContext();
        }
        #region Methods
        public string Add(string Width, string Height, string FolderName, string Section, string Description, string CurvedCorners, string CornerRadius, string ResizeHeight, string ResizeWidth, string ConvertToGrayScale, string AllowCrop, string CreatedBy)
        {
            try
            {
                _Imgsize = new ImagesSize();
                Guid id = Guid.NewGuid();
                _Imgsize.Width = int.Parse(Width);
                _Imgsize.Height = int.Parse(Height);
                _Imgsize.FolderName = FolderName;
                _Imgsize.Section = Section;
                _Imgsize.Description = Description;
                _Imgsize.CurvedCorners = Convert.ToBoolean(CurvedCorners);
                if (!string.IsNullOrEmpty(CornerRadius))
                {
                    _Imgsize.CornerRadius = Convert.ToInt32(CornerRadius);
                }
                else
                {
                    _Imgsize.CornerRadius = 0;
                }
                _Imgsize.ResizeHeight = Convert.ToBoolean(ResizeHeight);
                _Imgsize.ResizeWidth = Convert.ToBoolean(ResizeWidth);
                _Imgsize.ConvertToGrayScale = Convert.ToBoolean(ConvertToGrayScale);
                _Imgsize.AllowCrop = Convert.ToBoolean(AllowCrop);
                _Imgsize.CreatedBy = new Guid(CreatedBy);
                _Imgsize.CreatedOn = DateTime.Now;
                _Imgsize.Active = true;
                db.ImagesSizes.InsertOnSubmit(_Imgsize);
                db.SubmitChanges();
                return id.ToString();
            }
            catch (Exception ex) { return string.Empty; }
        }
        public bool Edit(string Width, string Height, string FolderName, string Section, string Description, string CurvedCorners, string CornerRadius, string ResizeHeight, string ResizeWidth, string ConvertToGrayScale, string AllowCrop, string ModifiedBy, string ImagesSizeId)
        {
            try
            {
                _Imgsize = db.ImagesSizes.SingleOrDefault(ad => ad.ImagesSizeId == int.Parse(ImagesSizeId));
                if (_Imgsize != null)
                {
                    _Imgsize.Width = int.Parse(Width);
                    _Imgsize.Height = int.Parse(Height);
                    _Imgsize.FolderName = FolderName;
                    _Imgsize.Section = Section;
                    _Imgsize.Description = Description;
                    _Imgsize.AllowCrop = Convert.ToBoolean(AllowCrop);
                    _Imgsize.CurvedCorners = Convert.ToBoolean(CurvedCorners);
                    if (!string.IsNullOrEmpty(CornerRadius))
                        _Imgsize.CornerRadius = Convert.ToInt32(CornerRadius);
                    _Imgsize.ResizeHeight = Convert.ToBoolean(ResizeHeight);
                    _Imgsize.ResizeWidth = Convert.ToBoolean(ResizeWidth);
                    _Imgsize.ConvertToGrayScale = Convert.ToBoolean(ConvertToGrayScale);
                    _Imgsize.ModifiedBy = new Guid(ModifiedBy);
                    _Imgsize.ModifiedOn = DateTime.Now;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { return false; }
        }
        public bool Delete(string ImagesSizeId, string ModifiedBy)
        {
            try
            {
                _Imgsize = db.ImagesSizes.SingleOrDefault(ad => ad.ImagesSizeId == int.Parse(ImagesSizeId));
                if (_Imgsize != null)
                {
                    _Imgsize.Active = false;
                    _Imgsize.ModifiedOn = DateTime.Now;
                    _Imgsize.ModifiedBy = new Guid(ModifiedBy);
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Restore(string ImagesSizeId, string ModifiedBy)
        {
            try
            {
                _Imgsize = db.ImagesSizes.SingleOrDefault(ad => ad.ImagesSizeId == int.Parse(ImagesSizeId));
                if (_Imgsize != null)
                {
                    _Imgsize.Active = true;
                    _Imgsize.ModifiedOn = DateTime.Now;
                    _Imgsize.ModifiedBy = new Guid(ModifiedBy);
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ImagesSize LoadById(string ImagesSizeId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ImagesSizeId))
                {
                    _Imgsize = db.ImagesSizes.SingleOrDefault(ad => ad.ImagesSizeId == int.Parse(ImagesSizeId));
                    return _Imgsize;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadByDeleteState(string DS)
        {
            try
            {
                bool ds = bool.Parse(DS);
                var query = (from c in db.ImagesSizes
                             where c.Active == ds
                             select c).Distinct();
                return query.ToDataTable("Section", "ASC");

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable LoadSections()
        {
            var query = (from c in db.ImagesSizes
                         select new { c.Section }).Distinct();
            return query.ToDataTable();
        }
        #endregion
    }
}

