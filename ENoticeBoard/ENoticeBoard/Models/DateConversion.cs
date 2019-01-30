using System;
using System.Linq;

namespace ENoticeBoard.Models
{
    public class DateConversion
    {
        private static readonly BaseDataEntities BaseData = new BaseDataEntities();
        // ReSharper disable once InconsistentNaming
        private static readonly DateTime publishedDate = new DateTime(2019, 01, 30);

        public static DateTime PublishedDate()
        {
            return publishedDate;
        }

        public static string CurrentPeriod()
        {
            var currentPeriod=BaseData.FinancialCalendars
                .Where(x=>x.CurrentPeriod==true)
                .Select(x => x.FinancialPeriod)
                .FirstOrDefault();
            return currentPeriod;
        }

        public static string CurrentYear()
        {
            var currentYear= BaseData.FinancialCalendars
                .Where(x=>x.CurrentYear==true)
                .Select(x => x.FinancialYear)
                .FirstOrDefault();
            return currentYear;
        }
    }
}