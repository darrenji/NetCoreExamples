using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Adapter
{
    public static class StringTools
    {
        public static bool IsEmpty(this string value) => string.IsNullOrWhiteSpace(value);
    }
}
