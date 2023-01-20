using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DAL
{
    public class PageSectionManagement
    {
        #region Variable
        dbDataContext db;
        PageSection _obj;
        #endregion
        #region Method
        public PageSectionManagement()
        { db = new dbDataContext(); }
        public string Add(string PageId, string SectionName, string ImageFile, string SectionContent, string VideoUrl, string CreatedBy)
        {
            try
            {
                _obj = new PageSection();
                _obj.PageSectionId = Guid.NewGuid();
                _obj.PageId = int.Parse(PageId);
                _obj.SectionName = SectionName;
                _obj.SectionContent = SectionContent;
                _obj.ImageFile = ImageFile;
                _obj.VideoUrl = VideoUrl;
                _obj.RecOrder = LoadItemsCount(true, PageId) + 1;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.PageSections.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.PageSectionId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string PageSectionId, string PageId, string SectionName, string ImageFile, string SectionContent, string VideoUrl, string ModifiedBy)
        {
            try
            {
                _obj = db.PageSections.FirstOrDefault(lb => lb.PageSectionId == new Guid(PageSectionId));
                if (_obj != null)
                {
                    _obj.PageId = int.Parse(PageId);
                    _obj.SectionName = SectionName;
                    _obj.SectionContent = SectionContent;
                    if (!string.IsNullOrEmpty(ImageFile))
                        _obj.ImageFile = ImageFile;
                    _obj.VideoUrl = VideoUrl;

                    _obj.ModifiedBy = new Guid(ModifiedBy);
                    _obj.ModifiedOn = DateTime.Now;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(string PageSectionId, string ModifiedBy)
        {
            try
            {
                _obj = db.PageSections.FirstOrDefault(ad => ad.PageSectionId == new Guid(PageSectionId));
                if (_obj != null)
                {
                    ShiftRecords((int)_obj.RecOrder, _obj.PageId.ToString(), true);
                    _obj.RecOrder = LoadItemsCount(false, _obj.PageId.ToString()) + 1;
                    _obj.Active = false;
                    _obj.ModifiedBy = new Guid(ModifiedBy);
                    _obj.ModifiedOn = DateTime.Now;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Restore(string PageSectionId, string ModifiedBy)
        {
            try
            {
                _obj = db.PageSections.FirstOrDefault(ad => ad.PageSectionId == new Guid(PageSectionId));
                if (_obj != null)
                {
                    ShiftRecords((int)_obj.RecOrder, _obj.PageId.ToString(), false);
                    _obj.RecOrder = LoadItemsCount(true, _obj.PageId.ToString()) + 1;
                    _obj.Active = true;
                    _obj.ModifiedBy = new Guid(ModifiedBy);
                    _obj.ModifiedOn = DateTime.Now;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public DataTable LoadByDeleteState(string Active, string PageId)
        {
            try
            {
                var query = (from P in db.PageSections
                             where P.Active == bool.Parse(Active) && P.PageId == int.Parse(PageId)
                             select P).Distinct();
                return query.ToDataTable("RecOrder", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadPages()
        {
            try
            {
                var query = (from P in db.Pages
                             select P).Distinct();
                return query.ToDataTable();
            }
            catch
            {
                return null;
            }
        }
        public PageSection LoadById(string PageSectionId)
        {
            try
            {
                if (!string.IsNullOrEmpty(PageSectionId))
                {
                    _obj = db.PageSections.FirstOrDefault(lb => lb.PageSectionId == new Guid(PageSectionId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public Page LoadPageId(string pageId)
        {
            try
            {
                if (!string.IsNullOrEmpty(pageId))
                {
                  Page  page = db.Pages.FirstOrDefault(lb => lb.PageId == int.Parse(pageId));
                  return page;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region ShiftRecord
        public int LoadItemsCount(bool DS, string PageId)
        {
            try
            {
                return (from Obj in db.PageSections
                        where Obj.Active == DS && Obj.PageId == int.Parse(PageId)
                        select Obj.PageSectionId).Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public PageSection LoadByOrder(int Order, string PageId, bool DS)
        {
            return db.PageSections.FirstOrDefault(t => t.RecOrder == Order && t.PageId == int.Parse(PageId) && t.Active == DS);
        }
        public string ReOrder(string PageSectionId, bool Incre, string ModifiedBy, bool DS)
        {
            try
            {
                PageSection Obj = db.PageSections.FirstOrDefault(o => o.PageSectionId == new Guid(PageSectionId));
                if (Obj != null)
                {

                    //Calc Current and New Order
                    int CurrentOrder = int.Parse(Obj.RecOrder.ToString());
                    int NewOrder = 1;

                    if (Incre) { NewOrder = CurrentOrder - 1; }
                    else { NewOrder = CurrentOrder + 1; }

                    //Update the Up / Down Record
                    PageSection obj2 = LoadByOrder(NewOrder, Obj.PageId.ToString(), DS);
                    if (obj2 != null)
                    {
                        obj2.RecOrder = CurrentOrder;
                        obj2.ModifiedOn = DateTime.Now;
                        obj2.ModifiedBy = new Guid(ModifiedBy);
                    }
                    else
                    {
                        if (Incre) { CurrentOrder--; }
                        else { CurrentOrder++; }
                    }

                    //Update this record
                    Obj.RecOrder = NewOrder;
                    Obj.ModifiedBy = new Guid(ModifiedBy);
                    Obj.ModifiedOn = DateTime.Now;

                    db.SubmitChanges();
                    return obj2.PageSectionId.ToString();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public bool ShiftRecords(int currentOrder, string PageId, bool DS)// delete & restore
        {
            try
            {
                int RowsCount = LoadItemsCount(DS, PageId);
                PageSection[] ObjArr = new PageSection[RowsCount - currentOrder];
                for (int k = 0, i = currentOrder + 1; i <= RowsCount; i++, k++)
                {
                    ObjArr[k] = LoadByOrder(i, PageId, DS);
                    if (ObjArr[k] != null)
                        ObjArr[k].RecOrder = ObjArr[k].RecOrder - 1;
                }
                db.SubmitChanges();
                return true;
            }
            catch { return true; }
        }
        #endregion
    }
}
