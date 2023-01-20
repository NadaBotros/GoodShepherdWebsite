using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.Linq.SqlClient;
namespace DAL
{
    public class AreaManagement
    {
        #region Variable
        dbDataContext db;
        Prg_Area _obj;
        #endregion
        #region Method
        public AreaManagement()
        { db = new dbDataContext(); }
        public string Add(string CityId, string AreaName, string CreatedBy)
        {
            try
            {
                _obj = new Prg_Area();
                _obj.AreaId = Guid.NewGuid();
                _obj.CityId = new Guid(CityId);
                _obj.AreaName = AreaName;
                _obj.RecOrder = LoadItemsCount(true, _obj.CityId.ToString()) + 1;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Prg_Areas.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.AreaId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string AreaId, string CityId, string AreaName, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Areas.FirstOrDefault(lb => lb.AreaId == new Guid(AreaId));
                if (_obj != null)
                {
                    _obj.AreaName = AreaName;
                    _obj.CityId = new Guid(CityId);
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
        public bool Delete(string AreaId, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Areas.FirstOrDefault(ad => ad.AreaId == new Guid(AreaId));
                if (_obj != null)
                {
                    ShiftRecords((int)_obj.RecOrder, _obj.CityId.ToString(), true);
                    _obj.RecOrder = LoadItemsCount(false, _obj.CityId.ToString()) + 1;
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
        public bool Restore(string AreaId, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Areas.FirstOrDefault(ad => ad.AreaId == new Guid(AreaId));
                if (_obj != null)
                {
                    ShiftRecords((int)_obj.RecOrder, _obj.CityId.ToString(), false);
                    _obj.RecOrder = LoadItemsCount(true, _obj.CityId.ToString()) + 1;
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
        public DataTable LoadByDeleteState(string CityId, string Active)
        {
            try
            {
                var query = (from P in db.Prg_Areas
                             where P.Active == bool.Parse(Active) && P.CityId == new Guid(CityId)
                             select P).Distinct();
                return query.ToDataTable("AreaName", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadByDeleteState(List<string> CitiesId)
        {
            try
            {
                var query = (from P in db.Prg_Areas
                             where P.Active ==true && CitiesId.Contains(P.CityId.ToString()) 
                             select P).Distinct();
                return query.ToDataTable("AreaName", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public Prg_Area LoadById(string AreaId)
        {
            try
            {
                if (!string.IsNullOrEmpty(AreaId))
                {
                    _obj = db.Prg_Areas.FirstOrDefault(lb => lb.AreaId == new Guid(AreaId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool CheckIfExists(string AreaId, string CityId, string AreaName)
        {
            //try
            //{
            AreaName = GeneralMethods.ConvertToSearchWord(AreaName);
            if (!string.IsNullOrEmpty(AreaId))
            {
                _obj = db.Prg_Areas.FirstOrDefault(lb => lb.AreaId != new Guid(AreaId) && lb.CityId != new Guid(CityId) && SqlMethods.Like(lb.AreaName, AreaName) && lb.Active == true);
                if (_obj != null)
                    return true;
            }
            else
            {
                _obj = db.Prg_Areas.FirstOrDefault(lb => SqlMethods.Like(lb.AreaName, AreaName) && lb.CityId == new Guid(CityId) && lb.Active == true);
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
        public int LoadItemsCount(bool DS, string CityId)
        {
            try
            {
                return (from Obj in db.Prg_Areas
                        where Obj.Active == DS && Obj.CityId == new Guid(CityId)
                        select Obj.AreaId).Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public Prg_Area LoadByOrder(int Order, string CityId, bool DS)
        {
            return db.Prg_Areas.FirstOrDefault(t => t.RecOrder == Order && t.CityId == new Guid(CityId) && t.Active == DS);
        }
        public string ReOrder(string AreaId, bool Incre, string ModifiedBy, bool DS)
        {
            // try
            {
                Prg_Area Obj = db.Prg_Areas.FirstOrDefault(o => o.AreaId == new Guid(AreaId));
                if (Obj != null)
                {

                    //Calc Current and New Order
                    int CurrentOrder = int.Parse(Obj.RecOrder.ToString());
                    int NewOrder = 1;

                    if (Incre) { NewOrder = CurrentOrder - 1; }
                    else { NewOrder = CurrentOrder + 1; }

                    //Update the Up / Down Record
                    Prg_Area obj2 = LoadByOrder(NewOrder, Obj.CityId.ToString(), DS);
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
                    return obj2.AreaId.ToString();
                }
                return string.Empty;
            }
            //catch (Exception ex)
            //{
            //    return string.Empty;
            //}
        }
        public bool ShiftRecords(int currentOrder, string CityId, bool DS)// delete & restore
        {
            try
            {
                int RowsCount = LoadItemsCount(DS, CityId);
                Prg_Area[] ObjArr = new Prg_Area[RowsCount - currentOrder];
                for (int k = 0, i = currentOrder + 1; i <= RowsCount; i++, k++)
                {
                    ObjArr[k] = LoadByOrder(i, CityId, DS);
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
