using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.UnitTest.EmployeeApp.Models;

namespace TotalNetCore.UnitTest.EmployeeApp.Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetEmployee(Guid id);
        void CreateEmployee(Employee employee);
    }
}
