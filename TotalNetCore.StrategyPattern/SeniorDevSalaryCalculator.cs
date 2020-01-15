using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TotalNetCore.StrategyPattern
{
    public class SeniorDevSalaryCalculator : ISalaryCalculator
    {
        public double CalculateTotalSalary(IEnumerable<DeveloperReport> reports)
        {
            return reports
            .Where(r => r.Level == DeveloperLevel.Senior)
            .Select(r => r.CalculateSalary() * 1.2)
            .Sum();
        }
    }
}
