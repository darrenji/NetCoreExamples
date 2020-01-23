﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.ClassifiedAd
{
    public static class QueryModels
    {
        public class GetPublishedClassifiedAds
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetOwnersClassifiedAd
        {
            public Guid OwnerId { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetPublicClassifiedAd
        {
            public Guid ClassifiedAdId { get; set; }
        }
    }
}
