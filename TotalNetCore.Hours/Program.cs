using System;
using System.Collections.Generic;

namespace TotalNetCore.Hours
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(string.Join(',', GetPastTwentyFourHourStrs()));

            //Console.WriteLine(GetPastTwentyFourHourRange().Item1);
            //Console.WriteLine(GetPastTwentyFourHourRange().Item2);

            //Console.WriteLine(string.Join(',', GetYesterdayAndTodayHourStrs()));
            //Console.WriteLine(GetYesterdayAndTodayHourRange().Item1);
            //Console.WriteLine(GetYesterdayAndTodayHourRange().Item2);

            string temp = "2020-2-10 00:01:00";

            Console.WriteLine(DateTime.Parse(temp).Hour);

            Console.ReadKey();

        }

      private static List<string> GetPastTwentyFourHourStrs()
        {
            var now = DateTime.Now;
            List<string> hourResult = new List<string>();
            for (var i = 0; i < 24; i++)
            {
                var hourStr = now.AddHours(-i).Hour.ToString();
                hourResult.Add(hourStr);
            }

            hourResult.Reverse();

            return hourResult;
        }

        private static List<string> GetYesterdayAndTodayHourStrs()
        {
            var now = DateTime.Now;
            List<string> hourResult = new List<string> { "1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24"};

            for(var i=1; i <= now.Hour; i++)
            {
                hourResult.Add(i.ToString());
            }

            return hourResult;
        }

        /// <summary>
        /// 获取过去24小时的的起始时间和结束时间字符串
        /// </summary>
        /// <returns></returns>
        private static (string,string)  GetPastTwentyFourHourRange()
        {
            var now = DateTime.Now;
            DateTime thisHour = now.AddHours(1);
            DateTime beforeHour = now.AddHours(-23);

            string thisYear = thisHour.Year.ToString();
            string thisMonth = thisHour.Month < 10 ? $"{0}{thisHour.Month.ToString()}" : thisHour.Month.ToString();
            string thisDay = thisHour.Day < 10 ? $"{0}{thisHour.Day.ToString()}" : thisHour.Day.ToString();
            string thisHourStr = thisHour.Hour > 10 ? thisHour.Hour.ToString() : $"0{thisHour.Hour.ToString()}";

            string beforeYear = beforeHour.Year.ToString();
            string beforeMonth = beforeHour.Month < 10 ? $"{0}{beforeHour.Month.ToString()}" : beforeHour.Month.ToString();
            string beforeDay = beforeHour.Day < 10 ? $"{0}{beforeHour.Day.ToString()}" : beforeHour.Day.ToString();
            string beforeHourStr = beforeHour.Hour > 10 ? beforeHour.Hour.ToString() : $"0{beforeHour.Hour.ToString()}";

            string startTime = $"{beforeYear}-{beforeMonth}-{beforeDay} {beforeHourStr}:00:00.000";
            string endTime = $"{thisYear}-{thisMonth}-{thisDay} {thisHourStr}:00:00.000";

            return (startTime, endTime);
        }


        private static (string, string) GetYesterdayAndTodayHourRange()
        {
            var now = DateTime.Now;
            var yesterday = now.AddDays(-1);
            string yesterdayYear = yesterday.Year.ToString();
            string yesterdayMonth = yesterday.Month < 10 ? $"{0}{yesterday.Month.ToString()}" : yesterday.Month.ToString();
            string yesterdayDay = yesterday.Day < 10 ? $"{0}{yesterday.Day.ToString()}" : yesterday.Day.ToString();

            string todayYear = now.Year.ToString();
            string todayMonth = now.Month < 10 ? $"{0}{now.Month.ToString()}" : now.Month.ToString();
            string todayDay = now.Day < 10 ? $"{0}{now.Day.ToString()}" : now.Day.ToString();
            string todayHour = now.Hour > 10 ? now.Hour.ToString() : $"0{now.Hour.ToString()}";

            string startTime = $"{yesterdayYear}-{yesterdayMonth}-{yesterdayDay} 00:00:00.000";
            string endTime = $"{todayYear}-{todayMonth}-{todayDay} {todayHour}:00:00.000";

            return (startTime, endTime);
        }
    }
}
