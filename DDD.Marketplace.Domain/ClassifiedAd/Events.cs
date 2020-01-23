using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public static class Events
    {
        //在事件的类中不用value objects
        //领域事件会在系统之间或不同的系统之间传输，可以看作是系统发布的合同
        //如果用Event Sourcing,事件还会被持久化
        public class ClassifiedAdCreated
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
        }

        public class ClassifiedAdTitleChanged
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }

        public class ClassifiedAdTextUpdated
        {
            public Guid Id { get; set; }
            public string AdText { get; set; }
        }

        public class ClassifiedAdPriceUpdated
        {
            public Guid Id { get; set; }
            public decimal Price { get; set; }
            public string CurrenccyCode { get; set; }
        }

        public class ClassifiedAdSentForReview
        {
            public Guid Id { get; set; }
        }

        public class PictureAddedToAClassifiedAd
        {
            public Guid ClassifiedAdId { get; set; }
            public Guid PictureId { get; set; }
            public string Url { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public int Order { get; set; }
        }

        public class ClassifiedAdPictureResized
        {
            public Guid ClassifiedAdId { get; set; }
            public Guid PictureId { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
        }

        public class ClassifiedAdPublished
        {
            public Guid Id { get; set; }
            public Guid ApprovedBy { get; set; }
        }
    }
}
