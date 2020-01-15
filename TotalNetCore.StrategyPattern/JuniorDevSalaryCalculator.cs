using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TotalNetCore.StrategyPattern
{
    public class JuniorDevSalaryCalculator : ISalaryCalculator
    {
        public double CalculateTotalSalary(IEnumerable<DeveloperReport> reports)
        {
            return reports
            .Where(r => r.Level == DeveloperLevel.Junior)
            .Select(r => r.CalculateSalary())
            .Sum(); 
        }
    }
}
