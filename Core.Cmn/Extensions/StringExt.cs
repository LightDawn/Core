using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Cmn.FarsiUtils;
using System.Globalization;

namespace Core.Cmn.Extensions
{
    public static class StringExt
    {
        public static DateTime ToDateTime(this string persianDateStr)
        {
            if (persianDateStr.Length == 6)
            {
                var strDate = string.Format("13{0}/{1}/{2}", persianDateStr.Substring(0, 2), persianDateStr.Substring(2, 2), persianDateStr.Substring(4, 2));
                DateTime dateTime = PersianDateConverter.ToGregorianDateTime(strDate);
                return dateTime;
            }
            else
            {
                var strDate = string.Format("{0}/{1}/{2}", persianDateStr.Substring(0, 4), persianDateStr.Substring(4, 2), persianDateStr.Substring(6, 2));
                DateTime dateTime = PersianDateConverter.ToGregorianDateTime(strDate);
                return dateTime;
            }
        }
        public static int ToPersianDate_8Num(this PersianDate date)
        {
            var persianDate = new PersianDate(date).ToString("d");
            int persianDate_8Num = int.Parse(persianDate.Replace("/", string.Empty));
            return persianDate_8Num;
        }

    
        public static string CurrectPersianChars(this string str)
        {
            return str.Replace("ك", "ک").Replace("ي", "ی").Replace("ﯼ", "ی").Replace("ى", "ی").Replace("ة", "ه");
        }

        public static string FormatWith(this string instance, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, instance, args);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

    }
}
