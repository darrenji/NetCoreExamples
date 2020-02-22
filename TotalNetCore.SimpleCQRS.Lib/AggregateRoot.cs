using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.SimpleCQRS.Lib
{
    public abstract class AggregateRoot
    {
        //事件集合
        private readonly List<Event> _changes = new List<Event>();

        //都有Guiid主键,抽象属性需要重写
        public abstract Guid Id { get; }
        public int Version { get; internal set; }

        /// <summary>
        /// 获取所有未提交的变化
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        /// <summary>
        /// 清空所有的事件，标记提交
        /// </summary>
        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        /// <summary>
        /// 执行所有的历史事件
        /// </summary>
        /// <param name="history"></param>
        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach(var e in history)
            {
                ApplyChange(e, false);
            }
        }

        //Aggregate执行事件
        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            //Aggregate执行事件
            this.AsDynamic().Apply(@event);
            //把事件放在集合中
            if (isNew) _changes.Add(@event);
        }
    }
}
