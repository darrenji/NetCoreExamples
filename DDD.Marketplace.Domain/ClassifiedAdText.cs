using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class ClassifiedAdText : Value<ClassifiedAdText>
    {
        public string Value { get; }
        internal ClassifiedAdText(string text) => Value = text;

        public static ClassifiedAdText FromString(string text) =>
            new ClassifiedAdText(text);

        public static implicit operator string(ClassifiedAdText text) => text.Value;
    }
}
