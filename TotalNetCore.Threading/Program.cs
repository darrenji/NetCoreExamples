using System;
using System.Diagnostics;
using System.Threading;

namespace TotalNetCore.Threading
{
    class Program
    {
        static int _value;
        static long _value1;
        static object _locker = new object();
        static int _test;
        const int _max = 10000;
        static void Main(string[] args)
        {
            /*
             Interlocked
             用于多线程，当多个线程共享变量，Innterlocked的作用相当于Lock
             Interlocked.Add
             */

            #region Interlocked.Add
            //Thread thread1 = new Thread(new System.Threading.ThreadStart(A));
            //Thread thread2 = new Thread(new System.Threading.ThreadStart(A));
            //thread1.Start();
            //thread2.Start();
            //thread1.Join();
            //thread2.Join();
            //Console.WriteLine(Program._value);
            //Console.ReadKey();
            #endregion

            #region Increment,Decrement
            //Thread thread1 = new Thread(new ThreadStart(B));
            //Thread thread2 = new Thread(new ThreadStart(B));
            //thread1.Start();
            //thread2.Start();
            //thread1.Join();
            //thread2.Join();
            //Console.WriteLine(Program._value);
            //Console.ReadKey();
            #endregion

            #region Exchange, CompareExchange
            //Thread thread1 = new Thread(new ThreadStart(C));
            //thread1.Start();
            //thread1.Join();

            //Console.WriteLine(Interlocked.Read(ref Program._value1));
            //Console.ReadKey();
            #endregion

            #region 测试性能
            var s1 = Stopwatch.StartNew();
            for(int i=0;i<_max;i++)
            {
                lock(_locker)
                {
                    _test++;
                }
            }
            s1.Stop();

            var s2 = Stopwatch.StartNew();
            for(int i=0;i<_max;i++)
            {
                Interlocked.Increment(ref _test);
            }
            s2.Stop();

            Console.WriteLine(s1.Elapsed.TotalMilliseconds);
            Console.WriteLine(s2.Elapsed.TotalMilliseconds);
            Console.ReadKey();
            #endregion
        }

        static void A()
        {
            Interlocked.Add(ref Program._value, 1);
        }

        static void B()
        {
            Interlocked.Increment(ref Program._value);
            Interlocked.Decrement(ref Program._value);
            Interlocked.Decrement(ref Program._value);
        }

        static void C()
        {
            Interlocked.Exchange(ref Program._value1, 10);//替换为10

            //结果返回原始值10，但是共享的变量值已经发生了改变
            //如果第一个参数值和第三个参数值相等，把共享变量改成第二个参数值
            long result = Interlocked.CompareExchange(ref Program._value1, 20, 10);
            Console.WriteLine(result);
        }
    }
}
