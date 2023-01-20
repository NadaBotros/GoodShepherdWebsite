using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class CSVExporter
    {
        static PersonManagement personManagement = new PersonManagement();
        public static void WriteToCSV(List<Prg_Person> personList)
        {
            string attachment = "attachment; filename=PersonList.csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            WriteColumnName();
            foreach (Prg_Person person in personList)
            {
                WriteUserInfo(person);
            }
            HttpContext.Current.Response.End();
        }

        private static void WriteUserInfo(Prg_Person person)
        {
            var stringBuilder = new StringBuilder();
            AddComma(person.PersonName, stringBuilder);
            AddComma(person.Email, stringBuilder);
            AddComma(person.Prg_Family.HomePhone, stringBuilder);
            AddComma(person.MobileNo1, stringBuilder);
            AddComma(person.MobileNo2, stringBuilder);
            //AddComma(personManagement.ConvertToFullAddress(person.Prg_Family.Prg_Area.Prg_City.CityName,
            //    person.Prg_Family.Prg_Area.AreaName, person.Prg_Family.StreetName, person.Prg_Family.BuildingNextTo, person.Prg_Family.BuildingNo,
            //    person.Prg_Family.FloorNo, person.Prg_Family.FlatNo, person.Prg_Family.AddressNotes), stringBuilder);
            AddComma(GeneralMethods.ConvertToDate(person.BirthDate), stringBuilder);
            AddComma(GeneralMethods.ConvertToDate(person.Prg_Family.MarriageDate), stringBuilder);
            HttpContext.Current.Response.Write(stringBuilder.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        private static void AddComma(string value, StringBuilder stringBuilder)
        {
            if (!string.IsNullOrEmpty(value))
                stringBuilder.Append(value.Replace(',', '-'));
            stringBuilder.Append(",");
        }

        private static void WriteColumnName()
        {
            string columnNames = "Name,E-mail Address,Home Phone,Mobile Phone,Other Phone,Birthday,Marriage Date";
            HttpContext.Current.Response.Write(columnNames);
            HttpContext.Current.Response.Write(Environment.NewLine);
        }
    }
}
