using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TotalNetCore.Common
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 枚举
            //var results = GetEnumList<BreakerCodeEnum>();
            //foreach (var result in results)
            //{
            //    if(result.ToString()=="OneP")
            //    {
            //        Console.WriteLine("1P");
            //    }
            //    else if(result.ToString() == "TwoP")
            //    {
            //        Console.WriteLine("2P");
            //    }
            //    else if (result.ToString() == "ThreeP")
            //    {
            //        Console.WriteLine("3P");
            //    }
            //    else
            //    {
            //        Console.WriteLine("4P");
            //    }
            //} 
            #endregion

            var result = GetFibonacci(8);
            foreach(var item in result)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }

        #region 异步的本质

        #endregion

        #region 获取枚举值
        //private static List<T> GetEnumList<T>()
        //{
        //    T[] array = (T[])Enum.GetValues(typeof(T));
        //    List<T> list = new List<T>(array);
        //    return list;
        //}

        //private static List<TEnum> GetEnumList1<TEnum>() where TEnum : Enum
        //     => ((TEnum[])Enum.GetValues(typeof(TEnum))).ToList();


        //public enum BreakerCodeEnum
        //{
        //    OneP = 0,
        //    TwoP = 1,
        //    ThreeP = 2,
        //    FourP = 3
        //} 
        #endregion

        #region 异步的本质
        /*
         当使用async/await的时候，编译器会把当前方法编译成实现IAsyncStateMachine的接口的类
         各个异步方法变成了这个接口的字段
         依靠状态机机制来管理方法的线程

         如果用了async一定要用await,因为看到async就会产生状态机，没有await可能状态机失效

        async不要和void一起使用。因为异步方法的异常时放在了Task,如果用void，异步方法内部的异常就不会被捕捉到。

            异步方法加Async结尾
         */

        private static async Task<string> GetData()
        {
            return await Task.FromResult("");
        }

        private static async Task<string> AsyncTask()
        {
            //不建议使用async+await,因为已经在最后一行，没有接下来的动作或方法，不会进入状态机
            return await GetData();
        }

        private static Task<string> JustTask()
        {
            //立即返回结果
            return GetData();
        }

        private Task<string> ReturnTaskExceptionNotCaught()
        {
            try
            {
                return GetData();
            }
            catch (Exception ex)
            {
                //永远不会到这里，因为GetData（）会立即执行
                //放在using里也不推荐，因为using dispose掉以后，可能再传参
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        private static void GetAwaiterGetResultExample()
        {
            //不推荐，如果有异常会出现在AggregateException很难捕捉到
            string data = GetData().Result;

            //推荐使用
            data = GetData().GetAwaiter().GetResult();
        }

        private async Task ConfigureAwaitExample()
        {
            //在库中推荐使用这种方式，会在当前线程结束，不像面向客户端的异步需要再次回到原来请求来的那个线程
            var data = await GetData().ConfigureAwait(false);
        }
        #endregion

        #region 斐波那契数列
        private static IEnumerable<int> GetFibonacci(int n)
        {
            //特殊情况特殊处理
            if (n >= 1) yield return 1;

            int a = 1, b = 0;
            for(int i=2; i <= n; ++i)
            {
                int t = b;//b需要移动到a的位置，移动之前先把值交给临时变量
                b = a;//b移动到a的位置
                a = a + t;//a移动到新的位置

                yield return a;
            }
        }

        #endregion
    }

    #region 异步
    public class AsyncAwait
    {
        /*
         内部创建了<AsyncAwaitExampe>d_0的类
         包含了状态机

        private sealed class <AsyncAwaitExample>d_0:IAsyncStateMachine
        {
            private int <myVariable>5_1;//方法里的字段变成了类中的字段
            public int <>1_state; //int类型的字段维护状态
            public AsyncAwait.AsyncAwait <>4_this;//包含异步方法的类也变成了字段
            public AsyncTaskMethodBuilder <>t_builder;
            private TaskAwaiter <>u_1;

            private void MoveNext()
            {
                int num = this.<>1._state;
                try
                {
                    TaskAwaiter awaiter;
                    AsyncAwait.AsyncAwait.<AsyncAwatiExample>d_0d_;
                    switch(num)
                    {
                        case 0:
                            break;
                        case 1:
                            goto Label_00D6;
                        default:
                            this.<myVariable>5_1=0;
                            awaiter = this.<>4_this.DummyAsyncMethod().GetAwaiter();//每个异步方法都能获取到一个TaskAwaiter
                            if(awaiter.get_IsCompleted())
                            {
                                goto Label_007E;
                            }
                            this.<>1_state = num = 0;
                            this.<>u_1=awaiter;
                            d_=this;
                            this.<>t_builder.AwaitUnsafeOnCompleted<TaskAwaiter, AsyncAwait.AsyncAwait.<AsyncAwaitExample>d_0>(ref awaiter, ref d_);
                            return;
                    }
                    awaiter = this.<>u_1;
                    this.<>u_1=new TaskAwaiter();
                    this.<>1_state=num =-1;
                Label_007e:
                    Awaiter.GetResult();
                    Debug.WriteLine("after first awaiter");
                    this.<myVariable>5_1=1;
                }
                catch(Exception exctpion)
                {
                    this.<>1_state=-2;
                    this.<>t_builder.SetException(exception);
                    return;
                }
            }
        }
        */
        public async Task AsyncAwaitExample()
        {
            int myVariable = 0;

            await DummyAsyncMethod();
            Debug.WriteLine("after first await");
            myVariable = 1;

            await DummyAsyncMethod();
            Debug.WriteLine("after second await");
            myVariable = 2;
        }

        /*
         内部创建了<DummyAsyncMethod>d_1的类
         包含了状态机
        */
        public async Task DummyAsyncMethod()
        {

        }
    }
    #endregion

    #region 异步1
    public class StockPrices
    {
        public Dictionary<string, decimal> _stockPrices;

        //public async Task<decimal> GetStockPriceForAsync(string companyId)
        //{
        //    await InitializeMapIfNeededAsync();
        //    _stockPrices.TryGetValue(companyId, out var result);
        //    return result;
        //}

        //使用状态机
        public Task<decimal> GetStockPriceForAsync(string companyId)
        {
            var stateMachine = new GetStockPriceForAsync_StateMachine(this, companyId);
            stateMachine.Start();
            return stateMachine.Task;
        }

        public async Task InitializeMapIfNeededAsync()
        {
            if (_stockPrices != null)
            {
                return;
            }

            await Task.Delay(42);
            _stockPrices = new Dictionary<string, decimal> { { "MSFT", 42 } };
        }
    }

    //需要堆上空间
    //强耦合，只针对StockPrices
    public class GetStockPriceForAsync_StateMachine
    {
        enum State { Start, Step1 }

        private readonly StockPrices @this;
        private readonly string _companyId;
        private readonly TaskCompletionSource<decimal> _tcs;//需要堆空间
        private Task _initializeMapIfNeededTask;//每一个await的方法变成状态机的一个字段，需要堆空间
        private State _state = State.Start;

        public GetStockPriceForAsync_StateMachine(StockPrices @this, string companyId)
        {
            this.@this = @this;
            _companyId = companyId;
        }

        public void Start()
        {
            try
            {
                if (_state == State.Start)
                {
                    if (string.IsNullOrEmpty(_companyId))
                        throw new ArgumentNullException();

                    _initializeMapIfNeededTask = @this.InitializeMapIfNeededAsync();

                    //更改状态
                    _state = State.Step1;

                    //这里一个task接着一个task,是一种continuation passing style
                    //每个task任务的继续就是Start方法
                    _initializeMapIfNeededTask.ContinueWith(_ => Start());//如果task完成，就没有必要continuation
                }
                else if (_state == State.Step1)
                {
                    if (_initializeMapIfNeededTask.Status == TaskStatus.Canceled)
                    {
                        _tcs.SetCanceled();
                    }
                    else if (_initializeMapIfNeededTask.Status == TaskStatus.Faulted)
                    {
                        _tcs.SetException(_initializeMapIfNeededTask.Exception.InnerException);
                    }
                    else
                    {
                        @this._stockPrices.TryGetValue(_companyId, out var result);
                        _tcs.SetResult(result);//主线程，caller获取结果
                    }
                }
            }
            catch (Exception e)
            {
                _tcs.SetException(e);
            }
        }

        public Task<decimal> Task => _tcs.Task;
    }

    public struct _GetStockPriceForAsync_d__1 : IAsyncStateMachine
    {
        public StockPrices __this;
        public string companyId;
        public AsyncTaskMethodBuilder<decimal> __builder;
        public int __state;
        private TaskAwaiter __task1Awaiter;//有几个await就有几个TaskAwaiter

        public void MoveNext()
        {
            decimal result;
            try
            {
                TaskAwaiter awaiter;
                if(__state!=0)
                {
                    if(string.IsNullOrEmpty(companyId))
                    {
                        throw new ArgumentNullException();
                    }

                    awaiter = __this.InitializeMapIfNeededAsync().GetAwaiter();

                    if(!awaiter.IsCompleted)
                    {
                        __state = 0;
                        __task1Awaiter = awaiter;

                        __builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
                        return;
                    }
                }
                else //__state==0的情况
                {
                    awaiter = __task1Awaiter;
                    __task1Awaiter = default(TaskAwaiter);
                    __state = -1;
                }

                awaiter.GetResult();//结束对当前异步task的等待,抛出TaskCancelledException;task.Wait()和task.Result会抛出AggregateExctpion,这种类型的异常包括IO先骨干的或者内存计算相关的异常
                __this._stockPrices.TryGetValue(companyId, out result);
            }
            catch (Exception e)
            {
                __state = -2;
                __builder.SetException(e);
                return;
            }
            //成功
            __state = -2;
            __builder.SetResult(result);
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            __builder.SetStateMachine(stateMachine);
        }
    }
    #endregion

    #region async
    /*
     async从C# 5.0开始，编译器会生成一个枚举类型的状态机。
     状态机的接口是IAsyncStateMachine
     每一个await就对应一个TaskAwaiter
     AsyncTaskMethodBuilder会根据TaskAwaiter的执行情况来执行下一个action
     AsyncTaskMethodBuilder和TaskAwaiter都可以决定是否MoveNext
     一个await一个线程
     GetRestul()在当前线程上等待Task完成
     */
    #endregion

    #region Task和Thread
    /*
     Task从4.5开始，比较新
     Thread从1.1就开始有了
     Task的两员大将是async/await
     Task还可以处理线程池，返回主线程
     */
    #endregion

    #region yield return
    /*
     yield return返回的IEnumerable<T>遍历的时候不会全部加载到内存，而是用多少取多少
     */
    #endregion

    #region 协程 Co-Routines
    /*
     Cooperative Functions协作的函数，解决函数间如何协作
     一种情况是执行一个外部函数，再执行一个内部函数
     一种情况是两个函数交替执行，执行完一个函数中的某部分，再执行令一个函数的某部分，如此反复
     两个函数交替执行，还有可能带着参数
     */
    #endregion

    #region Stackless和Stackful
    /*
     stackless内存位置不固定，编译器生成代码，如闭包，只需创建一个带一个状态的状态机，需要自己写try cacth,需要定义一个状态机类型。C#是Stackless,会创建一个闭包和状态机，需要编译器生成代码

     stackful固定的栈内存，直接从原栈的位置执行，依赖CPU跳转位置实现，需要分配一个固定大小的栈内存4kb,递归异常处理。Go属于Stackful,每个协程需要分配一个固定大小的内存
     */
    #endregion
}
