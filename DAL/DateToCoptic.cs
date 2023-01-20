using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DateToCoptic
    {
        public CopticDate Convert_To_Coptic(DateTime dtMelady)
        {
            int[] data = new int[3];
            int NewYear = 0, NewMonth = 0, NewDay = 0;
            int a = 0, b = 0, c = 0, d = 0, x = 0, y = 0, z = 0, h = 0;
            double t = 0;
            if (dtMelady.Month <= 2)
            {
                a = dtMelady.Month + 12;
                b = dtMelady.Year - 1;
            }
            else if (dtMelady.Month > 2)
            {
                a = dtMelady.Month;
                b = dtMelady.Year;
            }
            c = (int)(b / 100);
            d = (int)(b / 400);
            x = 2 - c + d;//ع
            y = (int)((b + 4716) * 365.25);
            z = (int)((a + 1) * 30.6001);
            h = dtMelady.Day + z + y + x - 1826553;
            t = h / 365.25;
            NewYear = (int)(t + 1);
            double k = (t % 1) * 12.175;
            NewMonth = (int)((k) + 1);
            double mm = (k % 1) * 30;
            if ((Math.Round((mm % 1), 1)) < 0.5)
            {
                NewDay = (int)mm;
            }
            else
            {
                NewDay = (int)(mm + 1);

            }
            if (NewDay == 0 && NewMonth == 1)
            {
                if ((NewYear - 3) % 4 == 0)
                    NewDay = 6;
                else
                    NewDay = 5;
                NewMonth = 13;
                NewYear--;
            }
            else if (NewDay == 0)
            {
                NewDay = 30;
                NewMonth--;
            }
            CopticDate dtCoptic = new CopticDate();
            dtCoptic.Day = NewDay;
            dtCoptic.Month = NewMonth;
            dtCoptic.Year = NewYear;
            dtCoptic.MonthName = get_Coptic_Month_Name(NewMonth);
            dtCoptic.Date = string.Format("{0} {1} {2} ش", NewDay, get_Coptic_Month_Name(NewMonth), NewYear);
            return dtCoptic;
        }
        public string get_Coptic_Month_Name(int m)
        {
            string day = "";
            if (m == 1)
                day += "توت";
            else if (m == 2)
                day += "بابة";
            else if (m == 3)
                day += "هاتور";
            else if (m == 4)
                day += "كيهك";
            else if (m == 5)
                day += "طوبة";
            else if (m == 6)
                day += "أمشير";
            else if (m == 7)
                day += "برمهات";
            else if (m == 8)
                day += "برمودة";
            else if (m == 9)
                day += "بشنس";
            else if (m == 10)
                day += "بؤونة";
            else if (m == 11)
                day += "أبيب";
            else if (m == 12)
                day += "مسري";
            else if (m == 13)
                day += "نسئ";
            return day;

        }
        public string get_Melady_Month_Name(int m)
        {
            string day = "";
            if (m == 1)
                day += "يناير";
            else if (m == 2)
                day += "فبراير";
            else if (m == 3)
                day += "مارس";
            else if (m == 4)
                day += "ابريل";
            else if (m == 5)
                day += "مايو";
            else if (m == 6)
                day += "يونيو";
            else if (m == 7)
                day += "يوليو";
            else if (m == 8)
                day += "اغسطس";
            else if (m == 9)
                day += "سبتمبر";
            else if (m == 10)
                day += "اكتوبر";
            else if (m == 11)
                day += "نوفمبر";
            else if (m == 12)
                day += "ديسمبر";
            return day;

        }
        public string CurrentDateMeladyAndCoptic(DateTime dt)
        {
            return string.Format("{0} | {1} {2} {3} م", Convert_To_Coptic(dt).Date, dt.Day, get_Melady_Month_Name(dt.Month), dt.Year);
        }
    }
    public class CopticDate
    {
        public int Day { set; get; }
        public string MonthName { set; get; }
        public int Month { set; get; }
        public int Year { set; get; }
        public string Date { set; get; }
    }
}
