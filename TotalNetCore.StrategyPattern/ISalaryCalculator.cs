using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.StrategyPattern
{
    public interface ISalaryCalculator
    {
        double CalculateTotalSalary(IEnumerable<DeveloperReport> reports);
    }
}
