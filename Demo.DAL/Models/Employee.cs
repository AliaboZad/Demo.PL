using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        [MinLength(5 )]
        public string Name { get; set; }
        [DataType(DataType.Currency)] 
        public decimal Salary { get; set; }
        
        public int? Age { get; set; }
        
        public string Address { get; set; }
        public bool IsActive { get; set; }
       
        public string EmailAddress { get; set; }
        
        public string PhoneNumber { get; set; }

        public DateTime HiringDate { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.Now;
        //[ForeignKey("Departments")]
        public int? DepartmentId { get; set; }

        public string ImageName { get; set; }


        //Navigation Property (One)
        public Department Departments { get; set; }

    }
}
