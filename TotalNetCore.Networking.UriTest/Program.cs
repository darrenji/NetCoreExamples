using System;
using System.Threading;

namespace TotalNetCore.Networking.UriTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var simpleUri = GetSimpleUri();
            Console.WriteLine(simpleUri.ToString());
            Console.ReadKey();
            Thread.Sleep(10000);
        }

        public static Uri GetSimpleUri() {
            var builder = new UriBuilder();
            builder.Scheme = "http";
            builder.Host = "baidu.com";
            return builder.Uri;
        }

        public static Uri GetSimpleUri_Constructor() {
            var builder = new UriBuilder("http", "baidu.com");
            return builder.Uri;
        }
    }

    
}
