using DDD.Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DDD.Marketplace.Tests
{
    public class ClassifiedAd_Publish_Spec
    {
        private readonly ClassifiedAd _classifiedAd;

        public ClassifiedAd_Publish_Spec()
        {
            _classifiedAd = new ClassifiedAd(new ClassifiedAddId(Guid.NewGuid()), new UserId(Guid.NewGuid()));
        }

        [Fact]
        public void Can_Publish_a_valid_ad()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test ad"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(100.0m, "EUR",new FakeCurrencyLookup()));

            _classifiedAd.RequestToPublish();

            Assert.Equal(ClassifiedAdState.PendingReview, _classifiedAd.State);
        }

        [Fact]
        public void Cannot_publish_without_title()
        {
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("buy stuff"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(100m, "EUR", new FakeCurrencyLookup()));

            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_without_text()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("test"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(100m, "EUR", new FakeCurrencyLookup()));

            Assert.Throws<InvalidEntityStateException>(()=> _classifiedAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_without_price()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("title"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("buy"));

            Assert.Throws<InvalidEntityStateException>(()=> _classifiedAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_with_zero_price()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("title"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("text"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(0.0m, "EUR",new FakeCurrencyLookup()));

            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }
    }
}
