using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace TotalNetCore.ByMrXiao.配置
{
    class Program
    {
        static void Main(string[] args)
        {
            //IConfigurationBuilder用来构建
            //Build后获得IConfigurationRoot

            #region 内存字典配置
            //IConfigurationBuilder builder = new ConfigurationBuilder();
            //builder.AddInMemoryCollection(new Dictionary<string, string> {
            //    { "key1","value1"},
            //    { "key2","value2"},
            //    { "section1:key3","value3"},
            //     { "section2:key4","value4"},
            //      { "section3:key5","value5"}
            //});

            //IConfigurationRoot configurationRoot = builder.Build();

            ////Console.WriteLine(configurationRoot["key1"]);
            ////Console.WriteLine(configurationRoot["key2"]);

            //IConfigurationSection section = configurationRoot.GetSection("section1");
            //Console.WriteLine(section["key3"]); 
            #endregion

            #region 命令行配置
            //var builder = new ConfigurationBuilder();

            ////通常写法
            ////builder.AddCommandLine(args);

            //#region 命令替换

            ////用命令行中k1的值替换CommandLineKey1的值
            //var mapper = new Dictionary<string, string> { { "-k1","CommandLineKey1"} };
            //builder.AddCommandLine(args, mapper);
            //#endregion

            //var configurationRoot = builder.Build();

            //Console.WriteLine($"CommanLineKey1:{configurationRoot["CommandLineKey1"]}");
            //Console.WriteLine($"CommandLineKey2:{configurationRoot["CommandLineKey2"]}");
            //Console.ReadKey();
            #endregion

            #region 环境变量
            //var builder = new ConfigurationBuilder();
            //builder.AddEnvironmentVariables();

            //var configurationRoot = builder.Build();


            //Console.WriteLine($"Key1:{configurationRoot["Key1"]}");


            #region 分层键
            //var section = configurationRoot.GetSection("Section1");
            //Console.WriteLine($"Key3:{section["Key3"]}");

            #endregion

            #region 多层分层
            //var section = configurationRoot.GetSection("Section2:Section3");
            //Console.WriteLine($"K4:{section["Key4"]}");
            #endregion

            #region 前缀过滤
            //var builder = new ConfigurationBuilder();
            //builder.AddEnvironmentVariables("D_");

            //var configurationRoot = builder.Build();
            //Console.WriteLine($"Key8:{configurationRoot["Key8"]}");
            #endregion
            #endregion

            #region 读取文件
            //var builder = new ConfigurationBuilder();
            //builder.AddJsonFile("appsettings.json", optional:false, reloadOnChange:true);
            //builder.AddIniFile("appsettings.ini");//后添加的会覆盖原先的
            //builder.AddJsonFile("appsettings.Development.json");

            //var configurationRoot = builder.Build();

            //Console.WriteLine($"Key1:{configurationRoot["Key1"]}");
            //Console.WriteLine($"Key2:{configurationRoot["Key2"]}");
            //Console.WriteLine($"Key3:{configurationRoot["Key3"]}");
            //Console.ReadKey();

            //Console.WriteLine($"Key1:{configurationRoot["Key1"]}");
            //Console.WriteLine($"Key2:{configurationRoot["Key2"]}");
            //Console.WriteLine($"Key3:{configurationRoot["Key3"]}");
            //Console.ReadKey();


            //Console.WriteLine($"Key8:{configurationRoot["Key8"]}");
            //Console.ReadKey();

            //Console.WriteLine($"Key2:{configurationRoot["Key2"]}");
            //Console.ReadKey();
            #endregion
        }
    }
}
