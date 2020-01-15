using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.FacadeBuilderPattern
{
    public class CarBuilderFacade
    {
        protected Car Car { get; set; }

        public CarBuilderFacade()
        {
            Car = new Car();
        }

        public Car Build() => Car;

        public CarInfoBuilder Info => new CarInfoBuilder(Car);
        public CarAddressBuilder Address => new CarAddressBuilder(Car);
    }
}
