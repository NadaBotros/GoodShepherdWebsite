using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
namespace DAL
{
    public class HobbiesManagement
    {
        #region Variable
        dbDataContext db;
        Prg_Hobby _obj;
        #endregion
        #region Method
        public HobbiesManagement()
        { db = new dbDataContext(); }
        public string Add(string HobbyName, string CreatedBy)
        {
            try
            {
                _obj = new Prg_Hobby();
                _obj.HobbyId = Guid.NewGuid();
                _obj.HobbyName = HobbyName;
                _obj.RecOrder = LoadItemsCount(true) + 1;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Prg_Hobbies.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.HobbyId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string HobbyId, string HobbyName, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Hobbies.FirstOrDefault(lb => lb.HobbyId == new Guid(HobbyId));
                if (_obj != null)
                {
                    _obj.HobbyName = HobbyName;
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
        public bool Delete(string HobbyId, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Hobbies.FirstOrDefault(ad => ad.HobbyId == new Guid(HobbyId));
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
        public bool Restore(string HobbyId, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Hobbies.FirstOrDefault(ad => ad.HobbyId == new Guid(HobbyId));
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
                var query = (from P in db.Prg_Hobbies
                             where P.Active == bool.Parse(Active)
                             select P).Distinct();
                return query.ToDataTable("RecOrder", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public Prg_Hobby LoadById(string HobbyId)
        {
            try
            {
                if (!string.IsNullOrEmpty(HobbyId))
                {
                    _obj = db.Prg_Hobbies.FirstOrDefault(lb => lb.HobbyId == new Guid(HobbyId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool CheckIfExists(string HobbyId, string HobbyName)
        {
            //try
            //{
                HobbyName = GeneralMethods.ConvertToSearchWord(HobbyName);
                if (!string.IsNullOrEmpty(HobbyId))
                {
                    _obj = db.Prg_Hobbies.FirstOrDefault(lb => lb.HobbyId != new Guid(HobbyId) && SqlMethods.Like(lb.HobbyName,HobbyName) &&lb.Active==true );
                    if (_obj != null)
                        return true;
                }
                else
                {
                    _obj = db.Prg_Hobbies.FirstOrDefault(lb => SqlMethods.Like(lb.HobbyName, HobbyName) && lb.Active == true);
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
                return (from Obj in db.Prg_Hobbies
                        where Obj.Active == DS
                        select Obj.HobbyId).Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public Prg_Hobby LoadByOrder(int Order, bool DS)
        {
            return db.Prg_Hobbies.FirstOrDefault(t => t.RecOrder == Order && t.Active == DS);
        }
        public string ReOrder(string HobbyId, bool Incre, string ModifiedBy, bool DS)
        {
            // try
            {
                Prg_Hobby Obj = db.Prg_Hobbies.FirstOrDefault(o => o.HobbyId == new Guid(HobbyId));
                if (Obj != null)
                {

                    //Calc Current and New Order
                    int CurrentOrder = int.Parse(Obj.RecOrder.ToString());
                    int NewOrder = 1;

                    if (Incre) { NewOrder = CurrentOrder - 1; }
                    else { NewOrder = CurrentOrder + 1; }

                    //Update the Up / Down Record
                    Prg_Hobby obj2 = LoadByOrder(NewOrder, DS);
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
                    return obj2.HobbyId.ToString();
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
                Prg_Hobby[] ObjArr = new Prg_Hobby[RowsCount - currentOrder];
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
