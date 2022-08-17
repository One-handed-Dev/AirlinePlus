using Common.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSection.Application.Contracts.Common
{
    public static class ShopUtility
    {
        public static ViewSimple[] GetPersianDayOfWeekArray() => new ViewSimple[]
        {
            new((long)DayOfWeek.Saturday, DayOfWeek.Saturday.ToFriendlyString()),
            new((long)DayOfWeek.Sunday, DayOfWeek.Sunday.ToFriendlyString()),
            new((long)DayOfWeek.Monday, DayOfWeek.Monday.ToFriendlyString()),
            new((long)DayOfWeek.Tuesday, DayOfWeek.Tuesday.ToFriendlyString()),
            new((long)DayOfWeek.Thursday, DayOfWeek.Thursday.ToFriendlyString()),
            new((long)DayOfWeek.Wednesday, DayOfWeek.Wednesday.ToFriendlyString()),
            new((long)DayOfWeek.Friday, DayOfWeek.Friday.ToFriendlyString()),
        };

        public static string ToFriendlyString(this DayOfWeek self) => self switch
        {
            DayOfWeek.Friday => "جمعه",
            DayOfWeek.Saturday => "شنبه",
            DayOfWeek.Sunday => "یک‌شنبه",
            DayOfWeek.Monday => "دوشنبه",
            DayOfWeek.Tuesday => "سه‌شنبه",
            DayOfWeek.Thursday => "پنج‌شنبه",
            DayOfWeek.Wednesday => "چهارشنبه",
        };
    }
}
