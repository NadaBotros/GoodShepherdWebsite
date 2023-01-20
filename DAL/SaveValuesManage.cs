using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SaveValuesManage
    {
        #region Variable
        dbDataContext db;
        SaveValue _obj;
        #endregion
        #region Method
        public SaveValuesManage()
        { db = new dbDataContext(); }
        public bool Edit(int SaveValueId, string NewValue)
        {
            try
            {
                _obj = db.SaveValues.FirstOrDefault(lb => lb.DataId == SaveValueId);
                if (_obj != null)
                {
                    _obj.Value = NewValue;                   
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
        public string LoadById(int Id)
        {
           return db.SaveValues.FirstOrDefault(x => x.DataId == Id).Value;
        }
        #endregion
    }
}