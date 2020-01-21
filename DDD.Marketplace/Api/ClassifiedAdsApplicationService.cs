using DDD.Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Api
{
    public class ClassifiedAdsApplicationService : IApplicationService
    {
        private readonly IEntityStore _store;
        private ICurrencyLookup _currencyLookup;

        public ClassifiedAdsApplicationService(IEntityStore store, ICurrencyLookup currencyLookup)
        {
            _store = store;
            _currencyLookup = currencyLookup;
        }
        public async  Task Handle(object command)
        {
            ClassifiedAd classifiedAd;
            switch(command)
            {
                case Contracts.ClassifiedAds.V1.Create cmd:
                    if(await _store.Exists<ClassifiedAd>(cmd.Id.ToString()))
                    {
                        throw new InvalidOperationException($"Entity with id {cmd.Id} already exists");
                    }

                    classifiedAd = new ClassifiedAd(new ClassifiedAddId(cmd.Id), new UserId(cmd.OwnerId));
                    await _store.Save(classifiedAd);
                    break;
                case Contracts.ClassifiedAds.V1.SetTitle cmd:
                    classifiedAd = await _store.Load<ClassifiedAd>(cmd.Id.ToString());
                    if (classifiedAd == null) throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");
                    classifiedAd.SetTitle(ClassifiedAdTitle.FromString(cmd.Title));
                    await _store.Save(classifiedAd);
                    break;
                case Contracts.ClassifiedAds.V1.UpdateText cmd:
                    classifiedAd = await _store.Load<ClassifiedAd>(cmd.Id.ToString());
                    if (classifiedAd == null) throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");
                    classifiedAd.UpdateText(ClassifiedAdText.FromString(cmd.Text));
                    await _store.Save(classifiedAd); 
                    break;
                case Contracts.ClassifiedAds.V1.UpdatePrice cmd:
                    classifiedAd = await _store.Load<ClassifiedAd>(cmd.Id.ToString());
                    if (classifiedAd == null) throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");
                    classifiedAd.UpdatePrice(Price.FromDecimal(cmd.Price, cmd.Currency, _currencyLookup));
                    await _store.Save(classifiedAd);
                    break;
                case Contracts.ClassifiedAds.V1.RequestToPublish cmd:
                    classifiedAd = await _store.Load<ClassifiedAd>(cmd.Id.ToString());
                    if (classifiedAd == null) throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");
                    classifiedAd.RequestToPublish();
                    await _store.Save(classifiedAd);
                    break;
                default:
                    throw new InvalidOperationException($"Command type {command.GetType().FullName} is unknown");
            }

        }
    }
}
