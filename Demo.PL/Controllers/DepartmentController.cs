using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using AutoMapper;
using Demo.PL.ViewsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUniteOfWork _uniteOfWork;
        //private readonly IDepartmentRepositry _departmentRepositry;
        private readonly IMapper _mapper;

        public DepartmentController(IUniteOfWork uniteOfWork , IMapper mapper)
        {
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchByValue)
        {
            IEnumerable<Department> department;

            if (string.IsNullOrEmpty(SearchByValue))
                department = await _uniteOfWork.DepartmentRepositry.GetAll();
            else
                department = _uniteOfWork.DepartmentRepositry.GetDepartmentByName(SearchByValue);

            
            var mapDep = _mapper.Map<IEnumerable<DepartmentViewmodel>>(department);


            return View(mapDep);
        }


        //[HttpGet] : Default Value
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewmodel department)
        {
            if (ModelState.IsValid)
            {
                var mappDep = _mapper.Map<Department>(department);
                  await _uniteOfWork.DepartmentRepositry.Add(mappDep);

                var AddDepartmen = await _uniteOfWork.Complete();

                if (AddDepartmen > 0)
                    TempData["Message"] = "Department Is Created Successfuly";

                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task< IActionResult > Details(int? id,string ViewName = "Details" )
        {
            if (id is null)
                return BadRequest();
            var department = await _uniteOfWork.DepartmentRepositry.Get(id.Value);

            if (department is null)
                return NotFound();

            var mapDep = _mapper.Map<DepartmentViewmodel>(department);

            return View(ViewName,mapDep );
        }

        public async Task< IActionResult> Edit(int? id)
        {
            ///if (id is null)
            ///    return BadRequest();
            ///var department = _departmentRepositry.Get(id.Value);
            ///if (department is null)
            ///    return NotFound();
            ///return View(department);


            return await Details(id , "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewmodel department)
        {
            if (id != department.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mapDep = _mapper.Map<Department>(department);
                    _uniteOfWork.DepartmentRepositry.Update(mapDep);
                    await _uniteOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id , "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id ,DepartmentViewmodel department)
        {
            if (id != department.Id)
                return BadRequest();
            try
            {
                var mapDep = _mapper.Map<Department>(department);
                _uniteOfWork.DepartmentRepositry.Delete(mapDep);
                await _uniteOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty , ex.Message);
                return View(department);
            }
            
        }
    }
}
