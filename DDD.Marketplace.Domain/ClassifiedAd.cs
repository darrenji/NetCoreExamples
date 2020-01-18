using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class ClassifiedAd
    {

        public ClassifiedAd(ClassifiedAddId id,UserId ownerId)
        {
            Id = id;
            _ownerId = ownerId;
        }

        public ClassifiedAddId Id { get; }

        //私有字段看作是领域的状态
        //领域的状态不希望通过属性的设置来改变，而是希望通过某个event来改变

        private UserId _ownerId;
        private string _title;
        private string _text;
        private decimal _price;

        public void SetTitle(string title) => _title = title;
        public void UpdateTextw(string text) => _text = text;
        public void UpdatePrice(decimal price) => _price = price;
    }
}
