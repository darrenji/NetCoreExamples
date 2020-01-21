using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Api
{
    public class Policy
    {
        public static Policy Handle<T>() where T : Exception
        {
            return new Policy();

        }

        public  RetryPolicy Retry()
        {
            Console.WriteLine("retrying");
            return new RetryPolicy();
        }
    }
}
