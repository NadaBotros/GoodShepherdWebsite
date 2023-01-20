using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.Linq.SqlClient;
namespace DAL
{
    public class NewsTypesManagement
    {
        #region Variable
        dbDataContext db;
        NewsType _obj;
        #endregion
        #region Method
        public NewsTypesManagement()
        { db = new dbDataContext(); }
        public string Add(string NewsTypeName, string CreatedBy)
        {
            try
            {
                _obj = new NewsType();
                _obj.NewsTypeId = Guid.NewGuid();
                _obj.NewsTypeName = NewsTypeName;
                _obj.RecOrder = LoadItemsCount(true) + 1;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.NewsTypes.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.NewsTypeId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string NewsTypeId, string NewsTypeName, string ModifiedBy)
        {
            try
            {
                _obj = db.NewsTypes.FirstOrDefault(lb => lb.NewsTypeId == new Guid(NewsTypeId));
                if (_obj != null)
                {
                    _obj.NewsTypeName = NewsTypeName;
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
        public bool Delete(string NewsTypeId, string ModifiedBy)
        {
            try
            {
                _obj = db.NewsTypes.FirstOrDefault(ad => ad.NewsTypeId == new Guid(NewsTypeId));
                if (_obj != null)
                {
                    ShiftRecords((int)_obj.RecOrder, true);
                    _obj.RecOrder = LoadItemsCount(false) + 1;
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
        public bool Restore(string NewsTypeId, string ModifiedBy)
        {
            try
            {
                _obj = db.NewsTypes.FirstOrDefault(ad => ad.NewsTypeId == new Guid(NewsTypeId));
                if (_obj != null)
                {
                    ShiftRecords((int)_obj.RecOrder, false);
                    _obj.RecOrder = LoadItemsCount(true) + 1;
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
        public DataTable LoadByDeleteState(string Active)
        {
            try
            {
                var query = (from P in db.NewsTypes
                             where P.Active == bool.Parse(Active)
                             select P).Distinct();
                return query.ToDataTable("RecOrder", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public NewsType LoadById(string NewsTypeId)
        {
            try
            {
                if (!string.IsNullOrEmpty(NewsTypeId))
                {
                    _obj = db.NewsTypes.FirstOrDefault(lb => lb.NewsTypeId == new Guid(NewsTypeId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool CheckIfExists(string NewsTypeId, string NewsTypeName)
        {
            //try
            //{
                NewsTypeName = GeneralMethods.ConvertToSearchWord(NewsTypeName);
                if (!string.IsNullOrEmpty(NewsTypeId))
                {
                    _obj = db.NewsTypes.FirstOrDefault(lb => lb.NewsTypeId != new Guid(NewsTypeId) && SqlMethods.Like(lb.NewsTypeName,NewsTypeName) &&lb.Active==true );
                    if (_obj != null)
                        return true;
                }
                else
                {
                    _obj = db.NewsTypes.FirstOrDefault(lb => SqlMethods.Like(lb.NewsTypeName, NewsTypeName) && lb.Active == true);
                    if (_obj != null)
                        return true;
                }
                return false;
            //}
            //catch
            //{
            //    return false;
            //}
        }
        #endregion
        #region ShiftRecord
        public int LoadItemsCount(bool DS)
        {
            try
            {
                return (from Obj in db.NewsTypes
                        where Obj.Active == DS
                        select Obj.NewsTypeId).Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public NewsType LoadByOrder(int Order, bool DS)
        {
            return db.NewsTypes.FirstOrDefault(t => t.RecOrder == Order && t.Active == DS);
        }
        public string ReOrder(string NewsTypeId, bool Incre, string ModifiedBy, bool DS)
        {
            // try
            {
                NewsType Obj = db.NewsTypes.FirstOrDefault(o => o.NewsTypeId == new Guid(NewsTypeId));
                if (Obj != null)
                {

                    //Calc Current and New Order
                    int CurrentOrder = int.Parse(Obj.RecOrder.ToString());
                    int NewOrder = 1;

                    if (Incre) { NewOrder = CurrentOrder - 1; }
                    else { NewOrder = CurrentOrder + 1; }

                    //Update the Up / Down Record
                    NewsType obj2 = LoadByOrder(NewOrder, DS);
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
                    return obj2.NewsTypeId.ToString();
                }
                return string.Empty;
            }
            //catch (Exception ex)
            //{
            //    return string.Empty;
            //}
        }
        public bool ShiftRecords(int currentOrder, bool DS)// delete & restore
        {
            try
            {
                int RowsCount = LoadItemsCount(DS);
                NewsType[] ObjArr = new NewsType[RowsCount - currentOrder];
                for (int k = 0, i = currentOrder + 1; i <= RowsCount; i++, k++)
                {
                    ObjArr[k] = LoadByOrder(i, DS);
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
