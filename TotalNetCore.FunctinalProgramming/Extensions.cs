using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FunctinalProgramming
{

 
    public static class Extensions
    {
        /// <summary>
        /// 方法组合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TReturn1"></typeparam>
        /// <typeparam name="TReturn2"></typeparam>
        /// <param name="func1"></param>
        /// <param name="func2"></param>
        /// <returns></returns>
        public static Func<T, TReturn2> Compose<T, TReturn1, TReturn2>(this Func<TReturn1, TReturn2> func1, Func<T, TReturn1> func2)
        {
            //入参T，执行func2返回结果
            //func2的返回结果交给func1,得到最终的结果
            //效果上来讲，入参T，最终的返回结果是func1的返回结果
            return x => func1(func2(x));
        }

  
        /// <summary>
        /// 把函数作为方法参数
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
       public  static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            int count = 0;
            foreach (TSource element in source)
            {
                checked
                {
                    if (predicate(element))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// 链式
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>

        public static StringBuilder AppendWhen(this StringBuilder sb, string value, bool predicate)
        {
            return predicate ? sb.Append(value) : sb;
        }
    
    }



}
