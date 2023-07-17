using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewsModels
{
    public class EmployeeViewModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Max Length is 50 Char")]
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(5, ErrorMessage = "Minimum length is 5 Char ")]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Range(20, 50)]
        public int? Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address Must be Like 123-Street-City-Country")]
        public string Address { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HiringDate { get; set; }
        
        //[ForeignKey("Departments")]
        public int? DepartmentId { get; set; }

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }


        //Navigation Property (One)
        public Department Departments { get; set; }
    }
}
