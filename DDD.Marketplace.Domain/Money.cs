using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain
{
    public class Money : Value<Money>
    {
        public decimal Amount { get; }
        public CurrencyDetails Currency { get; }
        private const string DefaultCurrency = "EUR";

        
        //只有子类可以调用，外界不能调用
        //这里，Money这个领域依赖了ICurrencyLookup接口，保持领域对外界没有依赖
        protected Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
        {
            if(string.IsNullOrEmpty(currencyCode))
            {
                throw new ArgumentNullException(nameof(currencyCode),"Currency code must be specified");
            }

            var currency = currencyLookup.FindCurrency(currencyCode);
            if(!currency.InUse)
            {
                throw new ArgumentException($"Currency {currencyCode} is not valid");
            }

            if(decimal.Round(amount,currency.DecimalPlaces)!=amount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), $"Amount in {currencyCode} cannot have more than {currency.DecimalPlaces} decimals");
            }
            Amount = amount;
            Currency = currency;
        }

        protected Money(decimal amount, CurrencyDetails currency)
        {
            Amount = amount;
            Currency = currency;
        }

        //私有构造函数只供给内部方法Add和Subtract用

        //封闭原则下，对于现在是值类型的Money不允许提供对外公开的方法来改变Money的字段
        //如果要更改，改变的是一个新实例
        public Money Add(Money summand)
        {

            if(Currency!=summand.Currency)
            {
                throw new CurrencyMismatchException("Cannot sum amounts with different currencies");
            }
            return new Money(Amount + summand.Amount, Currency); ;
        }

        public Money Subtract(Money subtrahend)
        {
            if(Currency!=subtrahend.Currency)
            {
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
            }
            return new Money(Amount - subtrahend.Amount, Currency);
        }
            

        public static Money operator +(Money summand1, Money summand2) =>
            summand1.Add(summand2);

        public static Money operator -(Money minuend, Money sutrahend) =>
            minuend.Subtract(sutrahend);

        public static Money FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) =>
            new Money(amount, currency, currencyLookup);

        public static Money FromString(string amount, string currency, ICurrencyLookup currencyLookup) =>
            new Money(decimal.Parse(amount),currency,currencyLookup);

        public override string ToString()
        {
            return $"{Currency.CurrencyCode} {Amount}";
        }

    }
}
