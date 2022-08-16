using System;
using System.Globalization;
using Common.Application.Contracts;

namespace Common.Application
{
    public static class Calendar
    {
        #region Init
        private static string[] En { get; } = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private static string[] Pn { get; } = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        #endregion

        #region Enums
        public enum PersianDayOfWeek
        {
            شنبه = 6,
            جمعه = 5,
            یکشنبه = 0,
            دوشنبه = 1,
            سه_شنبه = 2,
            چهارشنبه = 3,
            پنج_شنبه = 4,
        }
        #endregion

        public static string ToJalaliString(this DateTime date)
        {
            if (date == new DateTime()) return string.Empty;

            var pc = new PersianCalendar();
            var result = $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";
            return result;
        }

        public static string ToEnglishNumber(this string strNum)
        {
            var cash = strNum;
            for (var i = 0; i < 10; i++)
                cash = cash.Replace(Pn[i], En[i]);

            return cash;
        }

        public static string ToJalaliFullString(this DateTime date)
        {
            var pc = new PersianCalendar();
            return $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00} {date.Hour:00}:{date.Minute:00}:{date.Second:00}";
        }

        public static DateTime? FromJalaliString(this string InDate)
        {
            if (string.IsNullOrEmpty(InDate)) return null;

            var splitted = InDate.Split('/');
            if (splitted.Length < 3) return null;
            if (!int.TryParse(splitted[0].ToEnglishNumber(), out var year)) return null;
            if (!int.TryParse(splitted[1].ToEnglishNumber(), out var month)) return null;
            if (!int.TryParse(splitted[2].ToEnglishNumber(), out var day)) return null;

            var c = new PersianCalendar();
            return c.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public static string ConvertDigitsToPersian(this string self)
        {
            string result = string.Empty;

            for (int i = 0; i < self.Length; i++)
            {
                var each = self[i];

                if (int.TryParse("" + each, out int number)) result += Pn[number];
                else result += each;
            }

            return result;
        }

        public static DateTime ToGregorianDateTime(this string persianDate)
        {
            persianDate = persianDate.ToEnglishNumber();
            var day = Convert.ToInt32(persianDate.Substring(8, 2));
            var year = Convert.ToInt32(persianDate.Substring(0, 4));
            var month = Convert.ToInt32(persianDate.Substring(5, 2));
            return new DateTime(year, month, day, new PersianCalendar());
        }

        public static string ToMoney(this long myMoney) => myMoney.ToString("N0", CultureInfo.CreateSpecificCulture("fa-ir"));

        public static string ToMoney(this double myMoney) => myMoney.ToString("N0", CultureInfo.CreateSpecificCulture("fa-ir"));
    }
}