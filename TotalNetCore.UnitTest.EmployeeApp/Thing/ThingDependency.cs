using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.UnitTest.EmployeeApp.Thing
{
    public class ThingDependency : IThingDependency
    {
        public int Meaning => throw new NotImplementedException();

        public string JoinUpper(string a, string b)
        {
            throw new NotImplementedException();
        }
    }
}
