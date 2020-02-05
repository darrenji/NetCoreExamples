using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;


namespace TotalNetCore.Networking.StreamExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = AsyncMethodDemo().Result;//获取结果


            ComplexModel testModel = new ComplexModel();
            string testMessage = JsonConvert.SerializeObject(testModel);//序列化对象

            using (Stream ioStream = new FileStream(@"../stream_demo_file.txt", FileMode.OpenOrCreate))//文件流接到文件
            {
                using (StreamWriter sw = new StreamWriter(ioStream))//文件流交给StreamWriter
                {
                    sw.Write(testMessage);
                    sw.BaseStream.Seek(10, SeekOrigin.Begin);
                    sw.Write(testMessage);
                }
            }

            Console.WriteLine("Done!");
            Thread.Sleep(10000);
        }

        public static async Task<ResultObject> AsyncMethodDemo()
        {
            ResultObject result = new ResultObject();

            WebRequest request = WebRequest.Create("http://you.com");
            request.Method = "POST";

            //从请求中获取请求流
            Stream reqStream = request.GetRequestStream();

            using (StreamWriter sw = new StreamWriter(reqStream))//把流交给StreamWriter
            {
                sw.Write("Our test data query");
            }

            //从请求中获取响应
            var responseTask = request.GetResponseAsync();

            result.LocalResult = 27;
            var webResponse = await responseTask;

            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))//从响应中获取响应流，把流交给StreamReader
            {
                result.RequestResult = await sr.ReadToEndAsync();
            }

            return result;
        }
    }
}
