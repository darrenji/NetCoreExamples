using DDD.Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DDD.Marketplace.Tests
{
    public class MoneyTest
    {
        [Fact]
        public void Money_object_with_the_same_amount_should_be_equal()
        {
            //Arrange
            var firstAmount = new Money(10);
            var secondAmount = new Money(10);

            //Assert
            //在Money没有实现IEquatable<Money>接口之前，结果证明两者是不相等，这也很好理解，因为毕竟是引用类型
            //Money实现IEquatalble<Money>接口之后，两者相等，因为对如何相等做了严格的定义
            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Sum_of_money_give_full_amount()
        {
            var coin1 = new Money(1);
            var coin2 = new Money(2);
            var coin3 = new Money(2);

            var banknote = new Money(5);
            Assert.Equal(banknote, coin1+coin2+coin3);
        }
    }
}
