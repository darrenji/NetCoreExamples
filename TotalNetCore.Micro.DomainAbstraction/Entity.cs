using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.Micro.DomainAbstraction
{
    /// <summary>
    /// 非泛型领域抽象基类
    /// </summary>
    public abstract class Entity : IEntity
    {
        public abstract object[] GetKeys();

        public override string ToString()
        {
            return $"[Entity:{GetType().Name}] Keys = {string.Join(",",GetKeys())}";
        }

        //领域内部维护着一个事件集合,这里没有初始化，有可能会是null
        private List<IDomainEvent> _domainEvents;

        /// <summary>
        /// 领域内事件以只读的形式对外开放
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomanEvents => _domainEvents.AsReadOnly();


        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }

    /// <summary>
    /// 泛型领域基类，泛的是主键类型的型
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        int? _requestedHashCode;
        /// <summary>
        /// 抽象基类的虚方法，让子类必须继承
        /// </summary>
        public virtual TKey Id { get; protected set; }

        /// <summary>
        /// 泛型抽象基类实现非泛型抽象基类的抽象方法
        /// </summary>
        /// <returns></returns>
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity<TKey> item = (Entity<TKey>)obj;

            if(item.IsTransient() || this.IsTransient())
            {
                return false;
            }
            else
            {
                return item.Id.Equals(this.Id);
            }
        }


        /// <summary>
        /// 对象是否是全新的，未持久化的
        /// </summary>
        /// <returns></returns>
        public bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(Id, default);
        }


        /// <summary>
        /// 重写object基类的方法
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if(!IsTransient())//如果不是全新的对象
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;
                return _requestedHashCode.Value;
            }
            else//如果是全新的对象
            {
                return base.GetHashCode();
            }
        }

        /// <summary>
        /// 重写object基类的方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[Entity:{GetType().Name}] Id={Id}";
        }

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            if(object.Equals(left,null))
            {
                return (Object.Equals(right, null)) ? true : false;
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }
}
