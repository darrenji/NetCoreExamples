using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.SingletonPattern
{
    public interface ISingletonContainer
    {
        int GetPopulation(string name);
    }
}
