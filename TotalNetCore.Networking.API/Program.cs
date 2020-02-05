using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TotalNetCore.Networking.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)//Host ����Kestrel Web server;IHostBuilder��Kestrel��һ��ʵ��
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseUrls(new string[] { "http://[::]:80","https://[::]:443", "http://[::]:65432", "https://[::]:65431"});
                    //������������ȼ��ǣ�Program > lauchSettings.json
                })
                
            ;

        //Kestrel WebServer
        //a cross-platform server
        //aquare a designated port so it can listen for inbound request
        //supports HTTP/HTTPS, Web-Socket, Unix sockets, HTTP/2
        //run as an edge server
        //run behind a reverse proxy, such ad IIS,Nginx
    }
}
