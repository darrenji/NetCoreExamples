using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class Money : Value<Money>
    {
        public decimal Amount { get; }
        public string CurrencyCode { get; set; }
        private const string DefaultCurrency = "EUR";

        
        //只有子类可以调用，外界不能调用
        protected Money(decimal amount, string currencyCode="EUR")
        {
            if(decimal.Round(amount,2)!=amount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot have more than two decimals");
            }
            Amount = amount;
            CurrencyCode = currencyCode;
        }

        //封闭原则下，对于现在是值类型的Money不允许提供对外公开的方法来改变Money的字段
        //如果要更改，改变的是一个新实例
        public Money Add(Money summand)
        {

            if(CurrencyCode!=summand.CurrencyCode)//保证了是两种相同的货币在计算
            {
                throw new CurrencyMismatchException("Cannot sum amounts with different currencies");
            }
            return new Money(Amount + summand.Amount);
        }

        public Money Subtract(Money subtrahend)
        {
            if(CurrencyCode!=subtrahend.CurrencyCode)//保证了是两种相同的货币在计算
            {
                throw new CurrencyMismatchException("Cannot sum amounts with different currencies");
            }
            return new Money(Amount - subtrahend.Amount);
        }
            

        public static Money operator +(Money summand1, Money summand2) =>
            summand1.Add(summand2);

        public static Money operator -(Money minuend, Money sutrahend) =>
            minuend.Subtract(sutrahend);

        public static Money FromDecimal(decimal amount, string currency = DefaultCurrency) =>
            new Money(amount, currency);

        public static Money FromString(string amount, string currency=DefaultCurrency) =>
            new Money(decimal.Parse(amount),currency);

    }
}
