using System.Text;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Threading.Tasks;
using System;

namespace TotalNetCore.WebRequest.WebRequests
{
    class Program
    {
        //当向目标host发送请求的时候，如果一个group name还没有线程池，NET Core会创建一个连接线程池
        //下次有请求过来，就共用连接线程池
        private static readonly string FINANCE_CONN_GROUP = "financial_connection";
        private static readonly string REAL_ESTATE_CONN_GROUP = "real_estate_connection";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static void SubmitRealEstateRequest()
        {
           
            System.Net.WebRequest req = System.Net.WebRequest.Create("https://real-estate-detail.com/market/api");

            req.ConnectionGroupName = REAL_ESTATE_CONN_GROUP;

            var noCachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);//是否把响应缓存起来
            req.CachePolicy = noCachePolicy;

            req.AuthenticationLevel = AuthenticationLevel.MutualAuthRequired;
            req.Credentials = new NetworkCredential("test_user", "secure_and_safe_password");

            Stream reqStream = req.GetRequestStream();
            var messageString = "test";
            var messageBytes = Encoding.UTF8.GetBytes(messageString);
            reqStream.Write(messageBytes, 0, messageBytes.Length);
        }
    }
}
