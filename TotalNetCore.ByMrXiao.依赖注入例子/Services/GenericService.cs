using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.ByMrXiao.依赖注入例子.Services
{
    public interface IGenericService<T>
    {

    }
    public class GenericService<T> : IGenericService<T>
    {
        public T Data { get; private set; }

        public GenericService(T data)
        {
            this.Data = data;
        }
    }
}
