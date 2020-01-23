using DDD.Marketplace.Adapter;
using DDD.Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DDD.Marketplace.ClassifiedAd.Commands;

namespace DDD.Marketplace.ClassifiedAd
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
                case V1.Create cmd:
                    await HandleCreate(cmd);
                    break;
                case V1.SetTitle cmd:
                    await HandleUpdate(cmd.Id, c => c.SetTitle(ClassifiedAdTitle.FromString(cmd.Title)));
                    break;
                case V1.UpdateText cmd:
                    await HandleUpdate(cmd.Id, c => c.UpdateText(ClassifiedAdText.FromString(cmd.Text)));
                    break;
                case V1.UpdatePrice cmd:
                    await HandleUpdate(cmd.Id, c=> c.UpdatePrice(Price.FromDecimal(cmd.Price, cmd.Currency,_currencyLookup)));
                    break;
                case V1.RequestToPublish cmd:
                    await HandleUpdate(cmd.Id, c => c.RequestToPublish());
                    break;
                case V1.Publish cmd:
                    await HandleUpdate(cmd.Id, c => c.Publish(new UserId(cmd.ApprovedBy)));
                    break;
                default:
                    return Task.CompletedTask;
                    break;
            }
        }

        private async Task HandleCreate(V1.Create cmd)
        {
            if(await _repository.Exists(cmd.Id.ToString()))
            {
                throw new InvalidOperationException($"Entity with id {cmd.Id} already exists");
            }

            var classifiedAd = new Domain.ClassifiedAd(new ClassifiedAddId(cmd.Id), new UserId(cmd.OwnerId));

            await _repository.Add(classifiedAd);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(Guid id, Action<Domain.ClassifiedAd> operation)
        {
            var classifiedAd = await _repository.Load(id.ToString());
            if (classifiedAd == null)
                throw new InvalidOperationException($"Entity with id{id} cannot be found");
            operation(classifiedAd);
            await _unitOfWork.Commit();
        }
    }
}
