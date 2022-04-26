using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AstraRekrutacja.DateEvaluation
{
    static public class WorkleaveEvaluation
    {
        static public (DateTime, DateTime) workleaveSpanP = (new DateTime(2022,02,01), new DateTime(2022, 02, 06));

        //Count last day of work leave (In case that work leave ends at friday, then friday is returned as last day of work leave)
        static public DateTime LastDayOfWorkleave(DateTime start, int howManyDays)
        {
            DateTime evaluatedDay = start;
            int daysLeft = howManyDays;

            //Iterate through days
            while (daysLeft > 0)
            {
                //If weekend, skip day till work day
                if (evaluatedDay.DayOfWeek == DayOfWeek.Saturday || evaluatedDay.DayOfWeek == DayOfWeek.Sunday)
                {
                    evaluatedDay = evaluatedDay.AddDays(1);
                }
                else
                {
                    daysLeft--;
                    evaluatedDay = evaluatedDay.AddDays(1);
                }
            }
            return evaluatedDay;
        }

        static public int HowManyDaysOfWorkleave(DateTime start, DateTime end, int howManyDays)
        {
            int result = howManyDays;

            //Iterate through every day
            foreach (DateTime day in EachDay(start, end))
            {
                //If weekend, add an additional free day
                if(day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                    result++;
            }

            return result;
        }

        static public bool IsInDataRange(DateTime start, DateTime end)
        {
            //start smaller or same as max span || end bigger or same as min span
            if (start <= workleaveSpanP.Item2 || workleaveSpanP.Item1 <= end)
                return true;
            else
                return false;
        }

        static public char WorkleaveSpan(DateTime start, DateTime end)
        {
            //Check if all time span is inside of "workleaveSpanP"
            if ((workleaveSpanP.Item1 <= start) && (end <= workleaveSpanP.Item2))
                return 'P'; //All days inside time span
            else
                return 'C'; //Partially inside time span
        }

        static public IEnumerable<DateTime> EachDay(DateTime start, DateTime end)
        {
            for (var day = start.Date; day.Date <= end.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}