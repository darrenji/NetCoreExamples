﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Adapter
{
    public static class ValueExtensions
    {
        public static void MustNotBeNull<T>(this Value<T> value) where T : Value<T>
        {
            if (value == null) throw new InvalidValueException(typeof(T), "cannot be null");
        }

        public static void MustBe<T>(this Value<T> value) where T : Value<T>
        {
            if (value == null) throw new InvalidValueException(typeof(T), "cannot be null");
        }
    }
}
