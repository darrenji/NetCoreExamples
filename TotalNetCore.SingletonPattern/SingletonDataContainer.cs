using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TotalNetCore.SingletonPattern
{
    public class SingletonDataContainer : ISingletonContainer
    {
        private Dictionary<string, int> _capitals = new Dictionary<string, int>();

        //私有构造函数
        private SingletonDataContainer()
        {
            Console.WriteLine("初始化单例");

            var elements = File.ReadAllLines("capitals.txt");
            for(int i=0;i<elements.Length;i+=2)
            {
                _capitals.Add(elements[i], int.Parse(elements[i + 1]));
            }
        }
        public int GetPopulation(string name)
        {
            return _capitals[name];
        }

        private static SingletonDataContainer instance = new SingletonDataContainer();//初始化私有构造函数

        public static SingletonDataContainer Instance => instance;
    }
}
