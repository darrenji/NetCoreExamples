using DDD.Marketplace.Adapter;
using DDD.Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Api
{
    public class ClassifiedAdsApplicationService : IApplicationService
    {
        private readonly IClassifiedAdRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrencyLookup _currencyLookup;

        public ClassifiedAdsApplicationService(IClassifiedAdRepository repository, IUnitOfWork unitOfWork, ICurrencyLookup currencyLookup)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _currencyLookup = currencyLookup;
        }
        public async Task Handle(object command)
        {
            switch(command)
            {
                case Contracts.ClassifiedAds.V1.Create cmd:
                    await HandleCreate(cmd);
                    break;
                case Contracts.ClassifiedAds.V1.SetTitle cmd:
                    await HandleUpdate(cmd.Id, c => c.SetTitle(ClassifiedAdTitle.FromString(cmd.Title)));
                    break;
                case Contracts.ClassifiedAds.V1.UpdateText cmd:
                    await HandleUpdate(cmd.Id, c => c.UpdateText(ClassifiedAdText.FromString(cmd.Text)));
                    break;
                case Contracts.ClassifiedAds.V1.UpdatePrice cmd:
                    await HandleUpdate(cmd.Id, c => c.UpdatePrice(Price.FromDecimal(cmd.Price, cmd.Currency, _currencyLookup)));
                    break;
                case Contracts.ClassifiedAds.V1.RequestToPublish cmd:
                    await HandleUpdate(cmd.Id, c => c.RequestToPublish());
                    break;
            }
        }

        private async Task HandleCreate(Contracts.ClassifiedAds.V1.Create cmd)
        {
            if (await _repository.Exists(cmd.Id.ToString()))
                throw new InvalidOperationException($"Entity with id {cmd.Id} already existes");

            var classifiedAd = new ClassifiedAd(new ClassifiedAddId(cmd.Id), new UserId(cmd.OwnerId));
            await _repository.Add(classifiedAd);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(Guid classifiedAdId, Action<ClassifiedAd> operation)
        {
            var classifiedAdd = await _repository.Load(classifiedAdId.ToString());
            if (classifiedAdd == null)
                throw new InvalidOperationException($"Entity with id {classifiedAdId} cannot be found");

            operation(classifiedAdd);

            await _unitOfWork.Commit();
        }
    }
}
