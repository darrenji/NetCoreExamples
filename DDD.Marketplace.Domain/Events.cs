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
            public ClassifiedAddId Id { get; set; }
            public UserId OwnerId { get; set; }
        }

        public class ClassifiedAdTitleChanged
        {
            public ClassifiedAddId Id { get; set; }
            public ClassifiedAdTitle Title { get; set; }
        }

        public class ClassifiedAdTextUpdated
        {
            public ClassifiedAddId Id { get; set; }
            public ClassifiedAdText AdText { get; set; }
        }

        public class ClassifiedAdPriceUpdated
        {
            public ClassifiedAddId Id { get; set; }
            public decimal Price { get; set; }
            public string CurrenccyCode { get; set; }
        }

        public class ClassifiedAdSentForReview
        {
            public ClassifiedAddId Id { get; set; }
        }
    }
}
