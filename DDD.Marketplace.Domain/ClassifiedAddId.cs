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
            if(value==default)
            {
                throw new ArgumentNullException(nameof(value), "Classified Ad id cannot be empty");
            }
            _value = value;
        }

        //隐式转换，把object value赋值给一个值类型
        public static implicit operator Guid(ClassifiedAddId self) => self._value;
    }
}
