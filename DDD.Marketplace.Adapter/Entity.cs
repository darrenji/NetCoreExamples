using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.Marketplace.Adapter
{
    /// <summary>
    /// 领域的基类
    /// </summary>
    public abstract class Entity
    {
        //用来存放事件
        private readonly List<object> _events;

        protected Entity()
        {
            _events = new List<object>();
        }

        /// <summary>
        /// 基类的这个方法不仅触发领域事件，还检查输入参数，还把事件保存起来
        /// </summary>
        /// <param name="event"></param>
        protected void Apply(object @event)
        {
            When(@event);//改变领域的状态
            EnsureValidState();//确保输入
            _events.Add(@event);//把时间保存以便发给外界
        }

        protected abstract void When(object @event);

        public IEnumerable<object> GetChanges()
        {
            return _events.AsEnumerable();
        }

        public void ClearChanges()
        {
            _events.Clear();
        }

        protected abstract void EnsureValidState();
        
    }
}
