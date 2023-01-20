using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
namespace DAL
{
    public class SiteTreeManage
    {
        #region Variable
        dbDataContext db;
        SiteTree _obj;
        #endregion
        #region Method
        public SiteTreeManage()
        { db = new dbDataContext(); }
        public string Add(string ParentSiteTreeId, string PageTitle, string PageFile, string CreatedBy)
        {
            try
            {
                _obj = new SiteTree();
                _obj.SiteTreeId = Guid.NewGuid();
                if (!string.IsNullOrEmpty(ParentSiteTreeId))
                    _obj.ParentSiteTreeId = new Guid(ParentSiteTreeId);

                _obj.PageFile = PageFile;
                _obj.PageTitle = PageTitle;
                _obj.RecOrder = LoadItemsCount(true, ParentSiteTreeId) + 1;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.SiteTrees.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.SiteTreeId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string SiteTreeId, string ParentSiteTreeId, string PageTitle, string PageFile, string ModifiedBy)
        {
            //try
            {
                _obj = db.SiteTrees.FirstOrDefault(lb => lb.SiteTreeId == new Guid(SiteTreeId));
                if (_obj != null)
                {
                    if (!string.IsNullOrEmpty(ParentSiteTreeId))
                        _obj.ParentSiteTreeId = new Guid(ParentSiteTreeId);
                    else
                        _obj.ParentSiteTreeId = null;

                    _obj.PageFile = PageFile;
                    _obj.PageTitle = PageTitle;
                    _obj.ModifiedBy = new Guid(ModifiedBy);
                    _obj.ModifiedOn = DateTime.Now;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            //catch
            //{
            //    return false;
            //}
        }
        public bool Delete(string SiteTreeId, string ModifiedBy)
        {
            try
            {
                _obj = db.SiteTrees.FirstOrDefault(ad => ad.SiteTreeId == new Guid(SiteTreeId));
                if (_obj != null)
                {
                    ShiftRecords((int)_obj.RecOrder, _obj.ParentSiteTreeId.ToString(), true);
                    _obj.RecOrder = LoadItemsCount(false, _obj.ParentSiteTreeId.ToString()) + 1;
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
        public bool Restore(string SiteTreeId, string ModifiedBy)
        {
            try
            {
                _obj = db.SiteTrees.FirstOrDefault(ad => ad.SiteTreeId == new Guid(SiteTreeId));
                if (_obj != null)
                {
                    ShiftRecords((int)_obj.RecOrder, _obj.ParentSiteTreeId.ToString(), false);
                    _obj.RecOrder = LoadItemsCount(false, _obj.ParentSiteTreeId.ToString()) + 1;
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
        public DataTable LoadAllPages()
        {
            var query = (from x in db.SiteTrees
                         where x.Active == true && x.PageFile != null
                         select x).Distinct();
            return query.ToDataTable("ParentSiteTreeId,RecOrder", "ASC");
        }
        public DataTable LoadByDeleteState(string Active, string ParentSiteTreeId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ParentSiteTreeId))
                {
                    var query = (from x in db.SiteTrees
                                 where x.Active == bool.Parse(Active) && x.ParentSiteTreeId == new Guid(ParentSiteTreeId)
                                 select x).Distinct();
                    return query.ToDataTable("RecOrder", "ASC");
                }
                else
                {
                    var query = (from x in db.SiteTrees
                                 where x.Active == bool.Parse(Active) && x.ParentSiteTreeId == null
                                 select x).Distinct();
                    return query.ToDataTable("RecOrder", "ASC");
                }
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadByDeleteState(string Active, string ParentSiteTreeId, string adminId)
        {
            try
            {

                var query = (from x in db.SiteTrees
                             where x.Active == bool.Parse(Active) && x.ParentSiteTreeId == new Guid(ParentSiteTreeId) && (db.Admins.Any(z => z.IsAdministrator == true && z.UserId == new Guid(adminId)) || db.AdminPages.Any(y => y.AdminId == new Guid(adminId) && y.PageId == x.SiteTreeId))
                             select x).Distinct();
                return query.ToDataTable("RecOrder", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public SiteTree LoadById(string SiteTreeId)
        {
            try
            {
                if (!string.IsNullOrEmpty(SiteTreeId))
                {
                    _obj = db.SiteTrees.FirstOrDefault(lb => lb.SiteTreeId == new Guid(SiteTreeId));
                    return _obj;
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
        public int LoadItemsCount(bool Active, string ParentSiteTreeId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ParentSiteTreeId))
                {
                    return (from x in db.SiteTrees
                            where x.Active == Active && x.ParentSiteTreeId == new Guid(ParentSiteTreeId)
                            select x.SiteTreeId).Count();
                }
                else
                {
                    return (from x in db.SiteTrees
                            where x.Active == Active && x.ParentSiteTreeId == null
                            select x.SiteTreeId).Count();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public SiteTree LoadByOrder(int Order, string ParentSiteTreeId, bool DS)
        {
            if (!string.IsNullOrEmpty(ParentSiteTreeId))
                return db.SiteTrees.FirstOrDefault(t => t.RecOrder == Order && t.Active == DS && t.ParentSiteTreeId == new Guid(ParentSiteTreeId));
            else
                return db.SiteTrees.FirstOrDefault(t => t.RecOrder == Order && t.Active == DS && t.ParentSiteTreeId == null);
        }
        public string ReOrder(string SiteTreeId, bool Incre, string ModifiedBy, bool DS)
        {
            // try
            {
                SiteTree Obj = db.SiteTrees.FirstOrDefault(o => o.SiteTreeId == new Guid(SiteTreeId));
                if (Obj != null)
                {

                    //Calc Current and New Order
                    int CurrentOrder = int.Parse(Obj.RecOrder.ToString());
                    int NewOrder = 1;

                    if (Incre) { NewOrder = CurrentOrder - 1; }
                    else { NewOrder = CurrentOrder + 1; }

                    //Update the Up / Down Record
                    SiteTree obj2 = LoadByOrder(NewOrder, Obj.ParentSiteTreeId.ToString(), DS);
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
                    return obj2.SiteTreeId.ToString();
                }
                return string.Empty;
            }
            //catch (Exception ex)
            //{
            //    return string.Empty;
            //}
        }
        public bool ShiftRecords(int currentOrder, string ParentSiteTreeId, bool DS)// delete & restore
        {
            try
            {
                int RowsCount = LoadItemsCount(DS, ParentSiteTreeId);
                SiteTree[] ObjArr = new SiteTree[RowsCount - currentOrder];
                for (int k = 0, i = currentOrder + 1; i <= RowsCount; i++, k++)
                {
                    ObjArr[k] = LoadByOrder(i, ParentSiteTreeId, DS);
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
