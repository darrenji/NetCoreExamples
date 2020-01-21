using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DDD.Marketplace.Domain
{
    public class ClassifiedAdTitle:Value<ClassifiedAdTitle>
    {
        public string Value { get; }

        internal ClassifiedAdTitle(string value)
        {
            //没有在构造函数里验证输入的有效性，因为不通过构造函数产生实例，而是通过工厂方法产生实例
            Value = value;
        }

        //这里体现的是工厂模式，所谓的工厂就是产生实例
        public static ClassifiedAdTitle FromString(string title)
        {
            CheckValidity(title);//在实例化之前先检查输入参数的有效性
            return new ClassifiedAdTitle(title);
        }
            

        public static ClassifiedAdTitle FromHtml(string htmlTitle)
        {
            var supportedTagsReplaced = htmlTitle
                .Replace("<i>", "*")
                .Replace("</i>", "*")
                .Replace("<b>", "**")
                .Replace("</b>", "**");

            var value = Regex.Replace(supportedTagsReplaced, "<.*?>",string.Empty);
            CheckValidity(value);
            return new ClassifiedAdTitle(value);
        }

        private static void CheckValidity(string value)
        {
            if(value.Length>100)
            {
                throw new ArgumentOutOfRangeException("Title cannot be longer than 100 characters", nameof(value));
            }
        }
    }
}
