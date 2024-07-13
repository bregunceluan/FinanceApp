using FinanceApp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Core.Libraries.Extensions;

public static class DateTimeExtensions
{
    public static DateTime GetFirstDayOfMonth(this DateTime date, int? year = null, int? month = null)
    {
        try
        {
            //todo criar teste
            return new DateTime(year ?? date.Year, month ?? date.Month, 1);
        }
        catch (Exception e)
        {
            throw new DateTimeException("Failed to calculete the date.", e);
        }
    }

    public static DateTime GetLastDayOfMonth(this DateTime date, int? year = null, int? month = null)
    {
        try
        {
            return new DateTime(year ?? date.Year, month ?? date.Month, 1)
                .AddMonths(1)
                .AddDays(-1);
        }
        catch (Exception e)
        {
            throw new DateTimeException("Failed to calculete the date.",e);
        }
    }
}
