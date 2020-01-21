using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class ClassifiedAd : Entity
    {

        public ClassifiedAddId Id { get; private set; }
        public UserId OwnerId { get; private set; }
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public Price Price { get; private set; }
        public ClassifiedAdState State { get; private set; }
        public UserId ApprovedBy { get; private set; }
        private readonly ICurrencyLookup _currencyLookup;

        public ClassifiedAd(ClassifiedAddId id, UserId ownerId, ICurrencyLookup currencyLookup)
        {
            _currencyLookup = currencyLookup;
            Apply(new Events.ClassifiedAdCreated { Id = id, OwnerId = ownerId });
        }

        public ClassifiedAd(ClassifiedAddId id, UserId ownerId)

        {
            Apply(new Events.ClassifiedAdCreated { Id=id, OwnerId = ownerId});
        }
        public void SetTitle(ClassifiedAdTitle title)
        {
            Apply(new Events.ClassifiedAdTitleChanged { Id = Id, Title = title });
        }

        public void UpdateText(ClassifiedAdText text)
        {
            Apply(new Events.ClassifiedAdTextUpdated { Id=Id, AdText=text});
        }

        public void UpdatePrice(Price price)
        {
            Apply(new Events.ClassifiedAdPriceUpdated { Id=Id, Price=price.Amount, CurrenccyCode=price.Currency.CurrencyCode});
        }

        public void RequestToPublish()
        {
            Apply(new Events.ClassifiedAdSentForReview { Id=Id});
        }

        //所有状态的改变交给了领域事件
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.ClassifiedAdCreated e:
                    Id = new ClassifiedAddId(e.Id);
                    OwnerId = e.OwnerId;
                    State = ClassifiedAdState.Inactive;
                    break;
                case Events.ClassifiedAdTitleChanged e:
                    Title = e.Title;
                    break;
                case Events.ClassifiedAdTextUpdated e:
                    Text = e.AdText;
                    break;
                case Events.ClassifiedAdPriceUpdated e:
                    Price = new Price(e.Price, e.CurrenccyCode, _currencyLookup);
                    break;
                case Events.ClassifiedAdSentForReview e:
                    State = ClassifiedAdState.PendingReview;
                    break;

            }
        }

        protected override void EnsureValidState()
        {
            var valid = Id != null
                && OwnerId != null;

            switch(State)
            {
                case ClassifiedAdState.PendingReview:
                    valid = valid && Title != null && Text != null && Price?.Amount > 0;
                    break;
                case ClassifiedAdState.Active:
                    valid = valid && Title != null && Text != null && Price?.Amount > 0 && ApprovedBy != null;
                    break;
                default:
                    valid = true;
                    break;
            }

            if(!valid)
            {
                throw new InvalidEntityStateException(this, $"Post-checks failed in state {State}");
            }
                
        }

    }

    //现在领域中有了事件，并且领域提供了方法可以获取到所有的事件。
    //调用的时候一般会通过EventBus把事件发布出去
    //var entity = await _repository.Load<ClassifiedSAd>(command.Id);
    //entity.RequestToPublish();
    //await _repository.Save(entity);
    //foreach(var @event in entity.GetChanges()){
    //await _bus.Publish(@event);
    //其它的组件如果订阅了事件就会引发动作，这就是reactive behavior

    public enum ClassifiedAdState
    {
        PendingReview,
        Active,
        Inactive,
        MarkedAsSold
    }
}
