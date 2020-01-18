using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DDD.Marketplace.Domain
{
    public class ClassifiedAdTitle:Value<ClassifiedAdTitle>
    {
        private readonly string _value;

        private ClassifiedAdTitle(string value)
        {
            if(value.Length > 100)
            {
                throw new ArgumentOutOfRangeException("Title cannot be longer than 100 characters", nameof(value));
            }
            _value = value;
        }

        //这里体现的是工厂模式，所谓的工厂就是产生实例
        public static ClassifiedAdTitle FromString(string title) =>
            new ClassifiedAdTitle(title);

        public static ClassifiedAdTitle FromHtml(string htmlTitle)
        {
            var supportedTagsReplaced = htmlTitle
                .Replace("<i>", "*")
                .Replace("</i>", "*")
                .Replace("<b>", "**")
                .Replace("</b>", "**");

            return new ClassifiedAdTitle(Regex.Replace(supportedTagsReplaced,"<.*?>", string.Empty));
        }
    }
}
