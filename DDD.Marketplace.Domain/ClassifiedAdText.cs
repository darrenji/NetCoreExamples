using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class ClassifiedAdText : Value<ClassifiedAdText>
    {
        public string Value { get; }

        //构造函数用来给某个属性或字段赋上初始值
        internal ClassifiedAdText(string text) => Value = text;

        //通过internal构造函数实例化
        public static ClassifiedAdText FromString(string text) =>
            new ClassifiedAdText(text);

        //重写隐式的操作符
        public static implicit operator string(ClassifiedAdText text) => text.Value;
    }
}
