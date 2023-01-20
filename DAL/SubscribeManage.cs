using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SubscribeManage
    {
        dbDataContext db;
        public SubscribeManage()
        {
            db = new dbDataContext();
        }
        public bool Add(string Email)
        {
            if (!db.Subscribes.Any(x => x.EMail == Email))
            {
                Subscribe obj = new Subscribe();
                obj.MailId = Guid.NewGuid();
                obj.EMail = Email;
                db.Subscribes.InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            else
                return false;
        }
    }
}
