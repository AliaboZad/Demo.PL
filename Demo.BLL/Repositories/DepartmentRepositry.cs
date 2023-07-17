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
    public class DepartmentRepositry : GenericRepositry<Department> , IDepartmentRepositry
    {
        #region Befor Generic
        ////انا هعمل اوبجكت من ال دي بي كونتكست
        ////انا بعمل دا عشان افتح كونكشن مع الداتا باز
        //private readonly MVCDbContext _dbContext;
        //public DepartmentRepositry(MVCDbContext dbContext) // Ask for object from Dbcontext
        //{
        //    //هنا انا هعرف الاوبجكت الي انا عملته 
        //    //dbContext /*new MVCDbContext();*/
        //    _dbContext = dbContext; 
        //}
        //public int Add(Department department)
        //{
        //    _dbContext.Departments.Add(department);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _dbContext.Departments.Remove(department);
        //    return _dbContext.SaveChanges(); 
        //} 

        //public Department Get(int id)
        //    => _dbContext.Departments.Find(id); 

        //public IEnumerable<Department> GetAll()
        //    => _dbContext.Departments.ToList();

        //public int Update(Department department)
        //{
        //    _dbContext.Departments.Update(department);
        //    return _dbContext.SaveChanges();
        //} 
        #endregion

        
        /// <summary>
        /// هعمل دا لما اكون مش هكتب فنكشن تاني هنا 
        /// </summary>  
        
        public DepartmentRepositry(MVCDbContext dbContext) : base (dbContext)
        {

        }

        public IEnumerable<Department> GetDepartmentByName(string name)
            => _dbContext.Departments.Where(D => D.Name.ToLower().Contains(name.ToLower()));
    }
}
