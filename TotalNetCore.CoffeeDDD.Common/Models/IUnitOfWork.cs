using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}
