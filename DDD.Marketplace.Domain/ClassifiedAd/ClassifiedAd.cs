using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class ClassifiedAd  : AggregateRoot<ClassifiedAddId>
    {
        public Guid ClassifiedAdId { get; private set; }

        protected ClassifiedAd() { }
        private string _databaseId { get; set; }
        public UserId OwnerId { get; private set; }
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public Price Price { get; private set; }
        public ClassifiedAdState State { get; private set; }
        public UserId ApprovedBy { get; private set; }
        public List<Picture> Pictures { get; private set; }

        public ClassifiedAd(ClassifiedAddId id, UserId ownerId)
        {
            Pictures = new List<Picture>();
            Apply(new Events.ClassifiedAdCreated { Id=id, OwnerId=ownerId});
        }

        public void SetTitle(ClassifiedAdTitle title) => Apply(new Events.ClassifiedAdTitleChanged { Id=Id, Title=title});

        public void UpdateText(ClassifiedAdText text) => Apply(new Events.ClassifiedAdTextUpdated { Id=Id, AdText=text});

        public void UpdatePrice(Price price) =>
            Apply(new Events.ClassifiedAdPriceUpdated
            {
                Id = Id,
                Price = price.Amount,
                CurrenccyCode = price.Currency.CurrencyCode
            });

        public void AddPicture(Uri pictureUri, PictureSize size)
        {
            Apply(new Events.PictureAddedToAClassifiedAd { 
                PictureId = new Guid(),
                ClassifiedAdId=Id,
                Url = pictureUri.ToString(),
                Height=size.Height,
                Width=size.Width,
                Order = NewPictureOrder()
            });

            int NewPictureOrder() => Pictures.Any() ? Pictures.Max(T => T.Order) + 1 : 0;

        }

        public void ResizePicture(PictureId pictureId, PictureSize newSize)
        {
            var picture = FindPicture(pictureId);
            if (picture == null)
                throw new InvalidOperationException("Cannot resize a picture that I dont't have");
            picture.Resize(newSize);
        }

        public void RequestToPublish() => Apply(new Events.ClassifiedAdSentForReview { Id=Id});

        public void Publish(UserId userId) => Apply(new Events.ClassifiedAdPublished { Id=Id, ApprovedBy =userId});

        protected override void EnsureValiedState()
        {
            var valid = Id != null && OwnerId != null;

            switch(State)
            {
                case ClassifiedAdState.PendingReview:
                    valid = valid && Title != null && Text != null && Price?.Amount > 0 && FirstPicture.HasCorrectSize();
                    break;
                case ClassifiedAdState.Active:
                    valid = Title != null && Text != null && Price?.Amount > 0 && FirstPicture.HasCorrectSize() && ApprovedBy != null;
                    break;
                default:
                    valid = true;
                    break;
            }

            if (!valid)
                throw new InvalidEntityStateException(this, $"Post-checks faild in state {State}");
        }

        protected override void When(object @event)
        {
            Picture picture;

            switch(@event)
            {
                case Events.ClassifiedAdCreated e:
                    Id = new ClassifiedAddId(e.Id);
                    OwnerId = new UserId(e.OwnerId);
                    State = ClassifiedAdState.Inactive;
                    break;
                case Events.ClassifiedAdTitleChanged e:
                    Title = new ClassifiedAdTitle(e.Title);
                    break;
                case Events.ClassifiedAdTextUpdated e:
                    Text = new ClassifiedAdText(e.AdText);
                    break;
                case Events.ClassifiedAdPriceUpdated e:
                    Price = new Price(e.Price, e.CurrenccyCode);
                    break;
                case Events.ClassifiedAdSentForReview _:
                    State = ClassifiedAdState.PendingReview;
                    break;
                case Events.PictureAddedToAClassifiedAd e:
                    picture = new Picture(Apply);
                    ApplyToEntity(picture, e);
                    break;
                    break;
                case Events.ClassifiedAdPictureResized e:
                    break;
            }
        }

        private Picture FindPicture(PictureId id)
        {
            return Pictures.FirstOrDefault(t => t.Id == id);
        }

        private Picture FirstPicture => Pictures.OrderBy(t => t.Order).FirstOrDefault();

        
    }



    public enum ClassifiedAdState
    {
        PendingReview,
        Active,
        Inactive,
        MarkedAsSold
    }
}
