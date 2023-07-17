﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUniteOfWork
    {
        public IEmployeeRepositry EmployeeRepositry { get; set; }
        public IDepartmentRepositry DepartmentRepositry { get; set; }

        Task< int> Complete();

    }
}
