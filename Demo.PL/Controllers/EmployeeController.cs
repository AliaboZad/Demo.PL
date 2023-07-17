using AutoMapper;
using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //public readonly IEmployeeRepositry _employeeRepositry;
        //public readonly IDepartmentRepositry _departmentRepositry;
        private readonly IMapper _mapper;
        private readonly IUniteOfWork _uniteOfWork;

        public EmployeeController(/*IEmployeeRepositry employeeRepositry , IDepartmentRepositry departmentRepositry*/  IUniteOfWork uniteOfWork,
            IMapper mapper)
        {
            _uniteOfWork = uniteOfWork;
            //_employeeRepositry = employeeRepositry;
            //_departmentRepositry = departmentRepositry;
            _mapper = mapper;
        }

        public async Task< IActionResult > Index(string SearchValue)
        {
            #region View Data & Bag
            //1. ViewDatat (Dictionary Object )
            //ViewData["Message"] = "Hi View Data";

            //2. ViewBage (Dynamic Property )
            //ViewBag.Message = "Hi View Bag"; 
            #endregion

            IEnumerable<Employee> employee;

            if (string.IsNullOrEmpty(SearchValue))
                 employee = await _uniteOfWork.EmployeeRepositry.GetAll();
            else
                 employee= _uniteOfWork.EmployeeRepositry.GetEmpByName(SearchValue);


            var mappedEmps = _mapper.Map<IEnumerable<EmployeeViewModel>>(employee);
            return View(mappedEmps);
        }  
        public IActionResult Create()
        {
            //ViewBag.Department = _departmentRepositry.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {

                employeeVM.ImageName = DecumentSetting.UploadFile(employeeVM.Image, "Images");

                var mappEmp = _mapper.Map<Employee>(employeeVM);
                 await _uniteOfWork.EmployeeRepositry.Add(mappEmp);

                var AddDepartmen = await _uniteOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public async Task< IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = await _uniteOfWork.EmployeeRepositry.Get(id.Value);
            

            if (employee is null)
                return NotFound();

            var mapEmp = _mapper.Map<EmployeeViewModel>(employee);

            return View(ViewName, mapEmp);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ///if (id is null)
            ///    return BadRequest();
            ///var department = _departmentRepositry.Get(id.Value);
            ///if (department is null)
            ///    return NotFound();



            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mapEmp = _mapper.Map<Employee>(employeeVM);
                    _uniteOfWork.EmployeeRepositry.Update(mapEmp);

                    int count = await _uniteOfWork.Complete();

                    if (count > 0)
                        DecumentSetting.DeletFile(mapEmp.ImageName, "Images");
                    
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var mapEmp= _mapper.Map< Employee>(employeeVM);
                _uniteOfWork.EmployeeRepositry.Delete(mapEmp);
                await _uniteOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }

        }
    }
}
