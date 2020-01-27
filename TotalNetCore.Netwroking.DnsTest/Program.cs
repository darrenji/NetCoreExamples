using System;
using System.Net;
using System.Threading;

namespace TotalNetCore.Netwroking.DnsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var domainEntry = Dns.GetHostEntry("baidu.com"); //接收域名
            //Console.WriteLine(domainEntry.HostName);
            //foreach (var ip in domainEntry.AddressList)
            //{
            //    Console.WriteLine(ip);
            //}
            //Console.ReadKey();

            var domainEntryByAddress = Dns.GetHostEntry("127.0.0.1");//接收IP
            Console.WriteLine(domainEntryByAddress.HostName);
            foreach(var ip in domainEntryByAddress.AddressList)
            {
                Console.WriteLine(ip);
            }
            Thread.Sleep(10000);
        }
    }
}
