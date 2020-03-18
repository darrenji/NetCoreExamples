using Microsoft.Extensions.FileProviders;
using System;

namespace TotalNetCore.ByMrXiao.文件提供程序
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileProvider provider1 = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);

            //var contents = provider1.GetDirectoryContents("/");

            #region 项目目录下的内容
            //foreach(var item in contents)
            //{
            //    //读取文件流
            //    //var fileStream = item.CreateReadStream();
            //    Console.WriteLine(item.Name);
            //}


            #endregion

            #region 读取嵌入式文件
            IFileProvider provider2 = new EmbeddedFileProvider(typeof(Program).Assembly);
            //var html = provider2.GetFileInfo("emb.html");
            #endregion


            #region 组合文件提供程序
            IFileProvider provider = new CompositeFileProvider(provider1, provider2);
            var contents = provider.GetDirectoryContents("/");

            foreach (var item in contents)
            {
                Console.WriteLine(item.Name);
            }
            #endregion

            //Console.ReadKey();

        }
    }
}
