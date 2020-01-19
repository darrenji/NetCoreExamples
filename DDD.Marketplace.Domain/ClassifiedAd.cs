using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class ClassifiedAd 
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
        }

        public void SetTitle(ClassifiedAdTitle title)
        {
            Title = title;
            EnsureValidState();
        }

        public void UpdateText(ClassifiedAdText text)
        {
            Text = text;
            EnsureValidState();
        }

        public void UpdatePrice(Price price)
        {
            Price = price;
            EnsureValidState();
        }

        public void RequestToPublish()
        {
            State = ClassifiedAdState.PendingReview;
            EnsureValidState();
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

    public enum ClassifiedAdState
    {
        PendingReview,
        Active,
        Inactive,
        MarkedAsSold
    }
}
