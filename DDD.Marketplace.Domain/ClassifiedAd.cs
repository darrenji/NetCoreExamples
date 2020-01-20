using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class ClassifiedAd : Entity
    {
        
        //虽然是类，而实际是看作值类型来看待了 
        //可以在Value Types中的类型做各种约束
        public ClassifiedAddId Id { get;  }
        public UserId OwnerId { get; }
        public ClassifiedAdState State { get; private set; }
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public Price Price { get; private set; }
        public UserId ApprovedBy { get; private set; }

        public ClassifiedAd(ClassifiedAddId id, UserId ownerId)
        {
            Id = id;
            OwnerId = ownerId;
            State = ClassifiedAdState.Inactive;
            EnsureValidState();

            Raise(new Events.ClassifiedAdCreated { Id=id, OwnerId=ownerId});
        }

        public void SetTitle(ClassifiedAdTitle title)
        {
            Title = title;
            EnsureValidState();

            Raise(new Events.ClassifiedAdTitleChanged { Id=Id, Title=title});
        }

        public void UpdateText(ClassifiedAdText text)
        {
            Text = text;
            EnsureValidState();

            Raise(new Events.ClassifiedAdTextUpdated {Id=Id,AdText=text }); 
        }

        public void UpdatePrice(Price price)
        {
            Price = price;
            EnsureValidState();

            Raise(new Events.ClassifiedAdPriceUpdated { Id = Id, Price = price.Amount, CurrenccyCode = Price.Currency.CurrencyCode });
        }

        public void RequestToPublish()
        {
            State = ClassifiedAdState.PendingReview;
            EnsureValidState();

            Raise(new Events.ClassifiedAdSentForReview { Id=Id});
        }

        protected  void EnsureValidState()
        {
            bool valid;
            switch(State)
            {
                case ClassifiedAdState.PendingReview:
                    valid = Id != null 
                        && OwnerId != null 
                        && Title != null 
                        && Text != null 
                        && Price?.Amount > 0;
                    break;
                case ClassifiedAdState.Active:
                    valid = Id != null
                        && OwnerId != null 
                        && Title != null 
                        && Text != null 
                        && Price?.Amount > 0 
                        && ApprovedBy != null;
                    break;
                default:
                    valid = true;
                    break;
            }

            if(!valid)
            {
                throw new InvalidEntityStateException(this, $"Post-checks failed in State {State}");
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
