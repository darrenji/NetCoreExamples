using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    public class Address : ValueObject<Address>
    {
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Country;
            yield return ZipCode;
            yield return City;
            yield return Street;
        }

        public string Country { get; }
        public string ZipCode { get; }
        public string City { get; }
        public string Street { get; }

        public Address(string country, string zipCode, string city, string street)
        {
            if (string.IsNullOrEmpty(country))
                throw new ArgumentException("Country cannot be empty.");
            if (string.IsNullOrEmpty(zipCode))
                throw new ArgumentException("Zip code cannot be empty");
            if (string.IsNullOrEmpty(city))
                throw new ArgumentException("City cannot be empty");
            if (string.IsNullOrEmpty(street))
                throw new ArgumentException("Street cannot be empty");
            if (!new Regex("[0-9]{2}-[0-9]{3}").Match(zipCode).Success)
                throw new ArgumentException("Zip code must be NN-NNN format");

            Country = country;
            ZipCode = zipCode;
            City = city;
            Street = street;
        }

        //EF Core对object value type的要求
        protected Address()
        {

        }
    }
}
