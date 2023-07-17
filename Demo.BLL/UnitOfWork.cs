using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL
{
    public class UnitOfWork : IUniteOfWork,IDisposable
    {
        private readonly MVCDbContext _dbContext;

        public IEmployeeRepositry EmployeeRepositry { get ; set ; }
        public IDepartmentRepositry DepartmentRepositry { get ; set ; }
        public UnitOfWork(MVCDbContext dbContext)
        {
            EmployeeRepositry = new EmployeeRepositries (dbContext);
            DepartmentRepositry = new DepartmentRepositry (dbContext);
            _dbContext = dbContext;
        }

        public async Task <int> Complete()
            => await _dbContext.SaveChangesAsync();

        public void Dispose()
            => _dbContext.Dispose();
    }
}
