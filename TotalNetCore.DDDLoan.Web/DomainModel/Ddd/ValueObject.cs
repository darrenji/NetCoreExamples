using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.DomainModel.Ddd
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        //抽象方法，必须在子类中override实现
        protected abstract IEnumerable<object> GetAttributesToIncludeInEqualityCheck();

        /// <summary>
        /// 重写Object的判断相等的方法
        /// 这里比较的是T泛型，需要把Object转换成T
        /// </summary>
        public override bool Equals(object other)
        {
            return Equals(other as T);
        }

        //虚方法，子类不一定通过override实现
        //没有实现则实现基类方法
        //实现则实现子类方法
        public virtual bool Equals(T other)
        {
            if(other==null)
            {
                return false;
            }

            //SequenceEqual判断集合中的每个元素是否相等
            return GetAttributesToIncludeInEqualityCheck().SequenceEqual(other.GetAttributesToIncludeInEqualityCheck());
        }

        /// <summary>
        /// 遍历集合中的每个元素产生新的hash
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hash = 17;
            foreach(var obj in this.GetAttributesToIncludeInEqualityCheck())
            {
                hash = hash * 31 + (obj == null ? 0 : obj.GetHashCode());
            }
            return hash;
        }


        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !(left == right);
        }
    }
}
