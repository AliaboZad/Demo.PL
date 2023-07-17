using Demo.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Demo.PL.ViewsModels
{
    public class DepartmentViewmodel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        //Navigation Property (Many)
        public ICollection<Employee> Employees { get; set; }
    }
}
