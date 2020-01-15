using System;

namespace TotalNetCore.SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDataContainer.Instance;
        }
    }
}
