using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.UnitTest.EmployeeApp.Thing
{
    public interface IThingDependency
    {
        string JoinUpper(string a, string b);
        int Meaning { get; }
    }
}
