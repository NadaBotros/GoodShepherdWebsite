using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
namespace DAL
{
    public class CityManagement
    {
        #region Variable
        dbDataContext db;
        Prg_City _obj;
        #endregion
        #region Method
        public CityManagement()
        { db = new dbDataContext(); }
        public string Add(string CityName, string CreatedBy)
        {
            try
            {
                _obj = new Prg_City();
                _obj.CityId = Guid.NewGuid();
                _obj.CityName = CityName;
                _obj.RecOrder = LoadItemsCount(true) + 1;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Prg_Cities.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.CityId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string CityId, string CityName, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Cities.FirstOrDefault(lb => lb.CityId == new Guid(CityId));
                if (_obj != null)
                {
                    _obj.CityName = CityName;
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
        public bool Delete(string CityId, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Cities.FirstOrDefault(ad => ad.CityId == new Guid(CityId));
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
        public bool Restore(string CityId, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Cities.FirstOrDefault(ad => ad.CityId == new Guid(CityId));
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
                var query = (from P in db.Prg_Cities
                             where P.Active == bool.Parse(Active)
                             select P).Distinct();
                return query.ToDataTable("RecOrder", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public Prg_City LoadById(string CityId)
        {
            try
            {
                if (!string.IsNullOrEmpty(CityId))
                {
                    _obj = db.Prg_Cities.FirstOrDefault(lb => lb.CityId == new Guid(CityId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool CheckIfExists(string CityId, string CityName)
        {
            //try
            //{
                CityName = GeneralMethods.ConvertToSearchWord(CityName);
                if (!string.IsNullOrEmpty(CityId))
                {
                    _obj = db.Prg_Cities.FirstOrDefault(lb => lb.CityId != new Guid(CityId) && SqlMethods.Like(lb.CityName,CityName) &&lb.Active==true );
                    if (_obj != null)
                        return true;
                }
                else
                {
                    _obj = db.Prg_Cities.FirstOrDefault(lb => SqlMethods.Like(lb.CityName, CityName) && lb.Active == true);
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
                return (from Obj in db.Prg_Cities
                        where Obj.Active == DS
                        select Obj.CityId).Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public Prg_City LoadByOrder(int Order, bool DS)
        {
            return db.Prg_Cities.FirstOrDefault(t => t.RecOrder == Order && t.Active == DS);
        }
        public string ReOrder(string CityId, bool Incre, string ModifiedBy, bool DS)
        {
            // try
            {
                Prg_City Obj = db.Prg_Cities.FirstOrDefault(o => o.CityId == new Guid(CityId));
                if (Obj != null)
                {

                    //Calc Current and New Order
                    int CurrentOrder = int.Parse(Obj.RecOrder.ToString());
                    int NewOrder = 1;

                    if (Incre) { NewOrder = CurrentOrder - 1; }
                    else { NewOrder = CurrentOrder + 1; }

                    //Update the Up / Down Record
                    Prg_City obj2 = LoadByOrder(NewOrder, DS);
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
                    return obj2.CityId.ToString();
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
                Prg_City[] ObjArr = new Prg_City[RowsCount - currentOrder];
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
