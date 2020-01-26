using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.UnitTest.EmployeeApp.Contracts;
using TotalNetCore.UnitTest.EmployeeApp.Models;

namespace TotalNetCore.UnitTest.EmployeeApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll() => _context.Employees.ToList();

        public Employee GetEmployee(Guid id) => _context.Employees
            .SingleOrDefault(e => e.Id.Equals(id));

        public void CreateEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _context.Add(employee);
            _context.SaveChanges();
        }
    }
}
