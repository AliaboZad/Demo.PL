using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepositries : GenericRepositry<Employee>, IEmployeeRepositry
    {
        /// <summary>
        /// اعمل دا لما يكون عندي فنكنشن تانيه هكتبها
        /// </summary>
        //public readonly MVCDbContext _dbContext;
        public EmployeeRepositries(MVCDbContext dbContext) :base (dbContext)
        {
            //_dbContext = dbContext;
        }

        public IEnumerable<Employee> GetEmpByName(string name)
            => _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));

        public IQueryable<Employee> GetEmpolyeeByAddress(string address)
        {
            _dbContext.Employees.Where(E => E.Address == address).ToList();
            return _dbContext.Employees;
        }


    }
}
