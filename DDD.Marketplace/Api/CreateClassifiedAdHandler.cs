using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Marketplace.Contracts;
using DDD.Marketplace.Domain;

namespace DDD.Marketplace.Api
{
    public class CreateClassifiedAdHandler : IHandleCommand<Contracts.ClassifiedAds.V1.Create>
    {
        private readonly IEntityStore _store;

        public Task Handle(ClassifiedAds.V1.Create command)
        {
            var classifiedAd = new DDD.Marketplace.Domain.ClassifiedAd(new ClassifiedAddId(command.Id), new UserId(command.OwnerId));

            //感觉领域模型和实体模型之间，在实际情况下会有转换
            return _store.Save(classifiedAd);
        }
    }
}
