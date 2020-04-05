using System;
using System.Collections;
using System.Collections.Generic;

namespace TotalNetCore.FunctinalProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
     

            #region 函数组合
            //Func<int, int> square = (x) => x * x;
            //Func<int, int> negative = x => x * -1;
            //Func<int, string> toString = x => x.ToString();
            //Func<int, string> result = toString.Compose(negative).Compose(square);
            //Console.WriteLine(result(2));
            #endregion

            Console.ReadKey();
        }



        /*
         让可读性增强。通常读代码的时间是写代码时间的10倍。
         更容易测试。没有隐藏的状态，没有依赖。
         更容易调试。
         面向对象是Imperative programming，命令式编程。
         函数式编程是declarative  programming，声明式编程。
         函数式编程方法名更有意义。
         函数式编程线程安全，适合并发。

         学习曲线稍高，没有很多资料，但状况在改变。
         组合函数比较困难。
         不能转换状态，每次都是拷贝复制。
         immutable value和recursion更占空间。
         */

        #region immutable types

         /// <summary>
         /// 可变
         /// </summary>
        public class Rectangle
        {
            public int Length { get; set; }
            public int Height { get; set; }

            public void Grow(int length, int height)
            {
                Length += length;
                Height += height;
            }
        }

        /// <summary>
        /// 不可变
        /// </summary>
        public class ImmutableRectange
        {
            int Length { get; }
            int Height { get; }

            public ImmutableRectange(int length, int height)
            {
                Length = length;
                Height = height;
            }

            public ImmutableRectange Grow(int length, int height)
            {
                return new ImmutableRectange(Length + length, Height + height);
            }
        }
        #endregion

        #region 表达式和语句

        /// <summary>
        /// 语句
        /// </summary>
        /// <param name="hour"></param>
        /// <returns></returns>
        static string GetStr(int hour)
        {
            string result = string.Empty;//创建变量
            if(hour<12)
            {
                result = "morning";
            }
            else
            {
                result = "afternoon";
            }

            return result;
        }

        static string GetStrNew(int hour)
        {
            return hour < 12 ? "morning" : "afternoon";
        }
        #endregion


        #region yield return按需取集合中的数据
        static IEnumerable<int> GreaterThan(List<int> lst,int gt)
        {
            //一般的遍历会把集合全部加载到内存中,遍历所有集合元素
            //使用yield return，只是把符合条件的加载到内存中
            foreach(var item in lst)
            {
                if (item > gt) yield return item;
            }
        }
        #endregion

    }

}
