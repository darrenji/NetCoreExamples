using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    /// <summary>
    /// Value Object封装了参数的约束，是参数约束的最小单元，这样使用Value Object的领域在参数约束这块就省事了
    /// </summary>
    public class ClassifiedAddId : Value<ClassifiedAddId>
    { 
        private readonly Guid _value;

        public ClassifiedAddId(Guid value)
        {
            _value = value;
        }
    }
}
