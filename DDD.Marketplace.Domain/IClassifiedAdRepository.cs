using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Marketplace.Domain
{
    public interface IClassifiedAdRepository
    {
        Task<ClassifiedAd> Load(ClassifiedAddId id);
        Task Add(ClassifiedAd entity);
        Task<bool> Exists(ClassifiedAddId id);
    }
}
