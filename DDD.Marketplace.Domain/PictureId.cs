using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class PictureId : Value<PictureId>
    {
        public PictureId(Guid value)
        {
            Value = value;
        }

        public  Guid Value { get; }
    }
}
