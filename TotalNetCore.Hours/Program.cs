using System;
using System.Collections.Generic;
using System.Linq;

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

            //string temp = "2020-2-10 00:01:00";

            //Console.WriteLine(DateTime.Parse(temp).Hour);

            //string temp = "00:59:20";
            //Console.WriteLine(DateTime.Parse(temp).Minute);

            //Console.WriteLine(string.Join(',', GetPastTwelveMonthsStr()));

            //Console.WriteLine($"startTime:{GetPastTwelveMonthsRange().Item1},endTime:{GetPastTwelveMonthsRange().Item2}");

            //Console.WriteLine(GetDayIndex(DateTime.Parse("2020-03-13 00:12:12")));

            //Console.WriteLine((short)DateTime.Now.DayOfWeek);

            //var temp = GetCats().Where(t => t.Week == (short)DateTime.Now.DayOfWeek).ToList();

            Guid temp = new Guid("0c78072c-89ff-47c4-bd51-5c07a14aaf28");
            Console.WriteLine(temp.ToString());

            Console.ReadKey();

        }

        /// <summary>
        /// 获取过去12个月
        /// </summary>
        /// <returns></returns>
        private static List<string> GetPastTwelveMonthsStr()
        {
            var now = DateTime.Now;
            List<string> result = new List<string>();
            for(int i=0; i<12; i++)
            {
                var pastSomeMonth = now.AddMonths(-(12 - i-1));
                result.Add(pastSomeMonth.Month.ToString());
            }

            return result;
        }

        /// <summary>
        /// 获取过去60分钟
        /// </summary>
        /// <returns></returns>
        private static List<string> GetPastSixtyMinutesStr()
        {
            var now = DateTime.Now;
            List<string> result = new List<string>();
            for(int i=0;i<60;i++)
            {
                var pastSomeMinute = now.AddMinutes(-(60-i - 1));
                result.Add(pastSomeMinute.Minute.ToString());
            }

            return result;
        }

        /// <summary>
        /// 获取过去7天的字符串
        /// </summary>
        /// <returns></returns>
        private static List<string> GetPastSevenDaysStr()
        {
            var now = DateTime.Now;
            List<string> result = new List<string>();
            for(int i=0; i<7;i++)
            {
                var pastSomeDay = now.AddDays(-(7 - i));
                result.Add(GetWeekStrFromEnum(pastSomeDay.DayOfWeek));
            }
            return result;
        }

        private static string GetWeekStrFromEnum(DayOfWeek dayOfWeek)
        {
            switch(dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "一";
                case DayOfWeek.Tuesday:
                    return "二";
                case DayOfWeek.Wednesday:
                    return "三";
                case DayOfWeek.Thursday:
                    return "四";
                case DayOfWeek.Friday:
                    return "五";
                case DayOfWeek.Saturday:
                    return "六";
                default:
                    return "日";
            }
        }


        /// <summary>
        /// 获取过去的30天字符串
        /// </summary>
        private static List<string> GetPastThirtyDaysStrs()
        {
            var now = DateTime.Now;
            List<string> result = new List<string>();
            for(int i=0; i<30;i++)
            {
                var pastSomeDay = now.AddDays(-(30 - i));
                result.Add($"{pastSomeDay.Month}.{pastSomeDay.Day}");
            }


            return result;
        }

     /// <summary>
     /// 获取过去的24小时
     /// </summary>
     /// <returns></returns>
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

        /// <summary>
        /// 获取从昨天凌晨开始到当前小时
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 获取昨天零点到当前小时的时间区间
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 获取过去12个月的开始时间和结束时间
        /// </summary>
        /// <returns></returns>
        private static (string, string) GetPastTwelveMonthsRange()
        {
            var thisMonth = DateTime.Now;
            var tempThisMonth = thisMonth.AddMonths(-1);
            var thisMonthLastDay = thisMonth.AddDays(1 - thisMonth.Day).AddMonths(1).AddDays(-1);
            var twelveMonthAgo = DateTime.Now.AddMonths(-11);

            string thisMonthYear = thisMonth.Year.ToString();
            string thisMonthMonth = thisMonth.Month < 10 ? $"{0}{thisMonth.Month.ToString()}" : thisMonth.Month.ToString();
            string thisMonthLastDayStr = thisMonthLastDay.Day < 10 ? $"{0}{thisMonthLastDay.Day.ToString()}" : thisMonthLastDay.Day.ToString();

            string twelveMonthAgoYear = twelveMonthAgo.Year.ToString();
            string twelveMonthAgoMonth = twelveMonthAgo.Month < 10 ? $"{0}{twelveMonthAgo.Month.ToString()}" : twelveMonthAgo.Month.ToString();

            string startTime = $"{twelveMonthAgoYear}-{twelveMonthAgoMonth}-01 00:00:00.000";
            string endTime = $"{thisMonthYear}-{thisMonthMonth}-{thisMonthLastDayStr} 23:59:59.000";

            return (startTime, endTime);
        }

        /// <summary>
        /// 获取过去30天的开始时间和结束时间
        /// </summary>
        /// <returns></returns>
        private static (string, string) GetPastThirtyDaysRange()
        {
            var today = DateTime.Now;
            var yesterday = today.AddDays(-1);
            var thirtyDaysAgo = today.AddDays(-30);

            string yesterdayYear = yesterday.Year.ToString();
            string yesterdayMonth = yesterday.Month < 10 ? $"{0}{yesterday.Month.ToString()}" : yesterday.Month.ToString();
            string yesterdayDay = yesterday.Day < 10 ? $"{0}{yesterday.Day.ToString()}" : yesterday.Day.ToString();

            string thirtyDaysAgoYear = thirtyDaysAgo.Year.ToString();
            string thirtyDaysAgoMonth = thirtyDaysAgo.Month < 10 ? $"{0}{thirtyDaysAgo.Month.ToString()}" : thirtyDaysAgo.Month.ToString();
            string thirtyDaysAgoDay = thirtyDaysAgo.Day < 10 ? $"{0}{thirtyDaysAgo.Day.ToString()}" : thirtyDaysAgo.Day.ToString();

            string startTime = $"{thirtyDaysAgoYear}-{thirtyDaysAgoMonth}-{thirtyDaysAgoDay} 00:00:00.000";
            string endTime = $"{yesterdayYear}-{yesterdayMonth}-{yesterdayDay} 23:59:59.000";

            return (startTime, endTime);
        }

        /// <summary>
        /// 获取过去7天的时间范围
        /// </summary>
        /// <returns></returns>
        private static (string, string) GetPastSevenDaysRange()
        {
            var today = DateTime.Now;
            var yesterday = today.AddDays(-1);
            var sevenDaysAgo = today.AddDays(-7);

            string yesterdayYear = yesterday.Year.ToString();
            string yesterdayMonth = yesterday.Month < 10 ? $"{0}{yesterday.Month.ToString()}" : yesterday.Month.ToString();
            string yesterdayDay = yesterday.Day < 10 ? $"{0}{yesterday.Day.ToString()}" : yesterday.Day.ToString();

            string sevenDaysAgoYear = sevenDaysAgo.Year.ToString();
            string sevenDaysAgoMonth = sevenDaysAgo.Month < 10 ? $"{0}{sevenDaysAgo.Month.ToString()}" : sevenDaysAgo.Month.ToString();
            string sevenDaysAgoDay = sevenDaysAgo.Day < 10 ? $"{0}{sevenDaysAgo.Day.ToString()}" : sevenDaysAgo.Day.ToString();

            string startTime = $"{sevenDaysAgoYear}-{sevenDaysAgoMonth}-{sevenDaysAgoDay} 00:00:00.000";
            string endTime = $"{yesterdayYear}-{yesterdayMonth}-{yesterdayDay} 23:59:59.000";


            return (startTime, endTime);

        }

        /// <summary>
        /// 获取过去60分钟的时间范围
        /// </summary>
        /// <returns></returns>
        private static (string, string) GetPastSixtyMinutesRange()
        {
            var thisMinute = DateTime.Now;
            var sixtyMinutesAgo = thisMinute.AddMinutes(-59);

            string thisMinuteYear = thisMinute.Year.ToString();
            string thisMinuteMonth = thisMinute.Month < 10 ? $"{0}{thisMinute.Month.ToString()}" : thisMinute.Month.ToString();
            string thisMinuteDay = thisMinute.Day < 10 ? $"{0}{thisMinute.Day.ToString()}" : thisMinute.Day.ToString();
            string thisMinuteHour = thisMinute.Hour < 10 ? $"{0}{thisMinute.Hour.ToString()}" : thisMinute.Hour.ToString();
            string thisMinuteMinute = thisMinute.Minute < 10 ? $"{0}{thisMinute.Minute.ToString()}" : thisMinute.Minute.ToString();

            string sixtyMinutesAgoYear = sixtyMinutesAgo.Year.ToString();
            string sixtyMinutesAgoMonth = sixtyMinutesAgo.Month < 10 ? $"{0}{sixtyMinutesAgo.Month.ToString()}" : sixtyMinutesAgo.Month.ToString();
            string sixtyMinutesAgoDay = sixtyMinutesAgo.Day < 10 ? $"{0}{sixtyMinutesAgo.Day.ToString()}" : sixtyMinutesAgo.Day.ToString();
            string sixtyMinutesAgoHour = sixtyMinutesAgo.Hour < 10 ? $"{0}{sixtyMinutesAgo.Hour.ToString()}" : sixtyMinutesAgo.Hour.ToString();
            string sixtyMinutesAgoMinute = sixtyMinutesAgo.Minute < 10 ? $"{0}{sixtyMinutesAgo.Minute.ToString()}" : sixtyMinutesAgo.Minute.ToString();

            string endTime = $"{thisMinuteYear}-{thisMinuteMonth}-{thisMinuteDay} {thisMinuteHour}:{thisMinuteMinute}:59.000";
            string startTime = $"{sixtyMinutesAgoYear}-{sixtyMinutesAgoMonth}-{sixtyMinutesAgoDay} {sixtyMinutesAgoHour}:{sixtyMinutesAgoMinute}:00.000";


            return (startTime, endTime);

        }

        /// <summary>
        /// 获取某天在数组中的索引
        /// </summary>
        /// <param name="ts">从时序数据库而来的时间，不是今天就是昨天</param>
        /// <returns></returns>
        private static int GetDayIndex(DateTime ts)
        {
            var today = DateTime.Now;
            var yesterday = today.AddDays(-1);
            if(ts.Day==yesterday.Day)//昨天
            {
                return ts.Hour;
            }
            else
            {
                return ts.Hour + 24;
            }
        }

        private static List<Cat> GetCats()
        {
            return new List <Cat>{
                new Cat{Id=1, Week=5},
                new Cat{ Id=2, Week=6}
            };
        }
    }

    public class Cat
    {
        public int Id { get; set; }
        public short Week { get; set; }
    }
}
