using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TotalNetCore.Networking.SocketsAndPorts
{
    class Program
    {
        static void Main(string[] args)
        {
            string server = "localhost";
            int port = 5000;
            string path = "/weatherforecast";

            Socket socket = null;
            IPEndPoint endpoint = null;//远程IPEndPoint
            var host = Dns.GetHostEntry(server);//IPHostEntry

            foreach (var address in host.AddressList)//IPAddress
            {
                socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                endpoint = new IPEndPoint(address, port);
                socket.ConnectAsync(endpoint).Wait();
                if (socket.Connected)
                {
                    break;
                }
            }

            //到这里，Socket建立连接
            var message = GetRequestMessage(server, port, path);//获取字符串
            var messageBytes = Encoding.ASCII.GetBytes(message);//字符串转换成字节数组
            var segment = new ArraySegment<byte>(messageBytes);//发送ArraySegment

            //发送请求
            socket.SendAsync(segment, SocketFlags.None).Wait();

            //接收
            var receiveSeg = new ArraySegment<byte>(new byte[512], 0, 512);//接收也是ArraySegment
            socket.ReceiveAsync(receiveSeg, SocketFlags.None).Wait();
            string receivedMessage = Encoding.ASCII.GetString(receiveSeg);//转换成字符串

            foreach (var line in receivedMessage.Split("\r\n"))
            {
                Console.WriteLine(line);
            }

            socket.Disconnect(false);//关闭连接
            socket.Dispose();//处理IDisposable
            Thread.Sleep(10000);
        }

        private static string GetRequestMessage(string server, int port, string path)
        {
            var message = $"GET {path} HTTP/1.1\r\n";
            message += $"Host: {server}:{port}\r\n";
            message += "cache-control: no-cache\r\n";
            message += "\r\n";
            return message;
        }
    }
}
