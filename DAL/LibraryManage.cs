using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
namespace DAL
{
    public class LibraryManage
    {
        #region Variable
        dbDataContext db;
        Library _obj;
        #endregion
        #region Method
        public LibraryManage()
        { db = new dbDataContext(); }
        public string Add(string ParentItemId, string LibraryType, string ItemTitle, string ItemLink, string CreatedBy)
        {
            try
            {
                _obj = new Library();
                _obj.LibraryItemId = Guid.NewGuid();
                if (!string.IsNullOrEmpty(ParentItemId))
                    _obj.ParentItemId = new Guid(ParentItemId);
                _obj.LibraryType = LibraryType;
                _obj.ItemLink = ItemLink;
                _obj.ItemTitle = ItemTitle;
                _obj.RecOrder = LoadItemsCount(true, ParentItemId, LibraryType) + 1;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Libraries.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.LibraryItemId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string LibraryItemId, string ParentItemId, string ItemTitle, string ItemLink, string ModifiedBy)
        {
            //try
            {
                _obj = db.Libraries.FirstOrDefault(lb => lb.LibraryItemId == new Guid(LibraryItemId));
                if (_obj != null)
                {
                    if (!string.IsNullOrEmpty(ParentItemId))
                        _obj.ParentItemId = new Guid(ParentItemId);
                    else
                        _obj.ParentItemId = null;

                    _obj.ItemLink = ItemLink;
                    _obj.ItemTitle = ItemTitle;
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
        public bool Delete(string LibraryItemId, string ModifiedBy)
        {
            try
            {
                _obj = db.Libraries.FirstOrDefault(ad => ad.LibraryItemId == new Guid(LibraryItemId));
                if (_obj != null)
                {
                    ShiftRecords((int)_obj.RecOrder, _obj.ParentItemId.ToString(), _obj.LibraryType, true);
                    _obj.RecOrder = LoadItemsCount(false, _obj.ParentItemId.ToString(), _obj.LibraryType) + 1;
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
        public bool Restore(string LibraryItemId, string ModifiedBy)
        {
            try
            {
                _obj = db.Libraries.FirstOrDefault(ad => ad.LibraryItemId == new Guid(LibraryItemId));
                if (_obj != null)
                {
                    ShiftRecords((int)_obj.RecOrder, _obj.ParentItemId.ToString(), _obj.LibraryType, false);
                    _obj.RecOrder = LoadItemsCount(false, _obj.ParentItemId.ToString(), _obj.LibraryType) + 1;
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
        public DataTable LoadByDeleteState(string Active, string ParentItemId, string LibraryType)
        {
            try
            {
                if (!string.IsNullOrEmpty(ParentItemId))
                {
                    var query = (from x in db.Libraries
                                 where x.Active == bool.Parse(Active) && x.LibraryType == LibraryType && x.ParentItemId == new Guid(ParentItemId)
                                 select x).Distinct();
                    return query.ToDataTable("RecOrder", "ASC");
                }
                else
                {
                    var query = (from x in db.Libraries
                                 where x.Active == bool.Parse(Active) && x.LibraryType == LibraryType && x.ParentItemId == null
                                 select x).Distinct();
                    return query.ToDataTable("RecOrder", "ASC");
                }
            }
            catch
            {
                return null;
            }
        }
        public Library LoadById(string LibraryItemId)
        {
            try
            {
                if (!string.IsNullOrEmpty(LibraryItemId))
                {
                    _obj = db.Libraries.FirstOrDefault(lb => lb.LibraryItemId == new Guid(LibraryItemId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetPath(string Id, DataTable libraries)
        {           
            if (string.IsNullOrEmpty(Id))
            {
                libraries.Rows.Add("", "القسم الرئيسي","", libraries.Rows.Count);
                return libraries;
            }
            else
            {
                Library ent = LoadById(Id);
                if (ent != null)
                {
                    libraries.Rows.Add(ent.LibraryItemId.ToString(), ent.ItemTitle,ent.ParentItemId, libraries.Rows.Count);
                    GetPath(ent.ParentItemId.ToString(), libraries);
                }                
                return libraries;
            }
           
        }
        public DataTable GetPath(string Id)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LibraryItemId", typeof(string));
            dt.Columns.Add("ItemTitle", typeof(string));
            dt.Columns.Add("ParentItemId", typeof(string));
            dt.Columns.Add("Order", typeof(int));
            GetPath(Id, dt);
            DataView dv = dt.DefaultView;
            dv.Sort = "order DESC";
            DataTable sortedDT = dv.ToTable();            
            return sortedDT;
        }
        #endregion
        #region ShiftRecord
        public int LoadItemsCount(bool Active, string ParentItemId, string LibraryType)
        {
            try
            {
                if (!string.IsNullOrEmpty(ParentItemId))
                {
                    return (from x in db.Libraries
                            where x.Active == Active && x.LibraryType == LibraryType && x.ParentItemId == new Guid(ParentItemId)
                            select x.LibraryItemId).Count();
                }
                else
                {
                    return (from x in db.Libraries
                            where x.Active == Active && x.LibraryType == LibraryType && x.ParentItemId == null
                            select x.LibraryItemId).Count();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public Library LoadByOrder(int Order, string ParentItemId, string LibraryType, bool DS)
        {
            if (!string.IsNullOrEmpty(ParentItemId))
                return db.Libraries.FirstOrDefault(t => t.RecOrder == Order && t.Active == DS && t.LibraryType == LibraryType && t.ParentItemId == new Guid(ParentItemId));
            else
                return db.Libraries.FirstOrDefault(t => t.RecOrder == Order && t.Active == DS && t.LibraryType == LibraryType && t.ParentItemId == null);
        }
        public string ReOrder(string LibraryItemId, bool Incre, string ModifiedBy, bool DS)
        {
            // try
            {
                Library Obj = db.Libraries.FirstOrDefault(o => o.LibraryItemId == new Guid(LibraryItemId));
                if (Obj != null)
                {

                    //Calc Current and New Order
                    int CurrentOrder = int.Parse(Obj.RecOrder.ToString());
                    int NewOrder = 1;

                    if (Incre) { NewOrder = CurrentOrder - 1; }
                    else { NewOrder = CurrentOrder + 1; }

                    //Update the Up / Down Record
                    Library obj2 = LoadByOrder(NewOrder, Obj.ParentItemId.ToString(), Obj.LibraryType, DS);
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
                    return obj2.LibraryItemId.ToString();
                }
                return string.Empty;
            }
            //catch (Exception ex)
            //{
            //    return string.Empty;
            //}
        }
        public bool ShiftRecords(int currentOrder, string ParentItemId, string LibraryType, bool DS)// delete & restore
        {
            try
            {
                int RowsCount = LoadItemsCount(DS, ParentItemId, LibraryType);
                Library[] ObjArr = new Library[RowsCount - currentOrder];
                for (int k = 0, i = currentOrder + 1; i <= RowsCount; i++, k++)
                {
                    ObjArr[k] = LoadByOrder(i, ParentItemId, LibraryType, DS);
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
