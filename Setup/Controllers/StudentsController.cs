﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Setup;
using Setup.Models;
using Setup.Repository.StudentRepo;

namespace Setup.Controllers
{
    public class StudentsController(IStudentRepository studentRepository, IMapper mapper) : Controller
    {

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await studentRepository.GetAllAsync();
            var studentVMs = mapper.Map<List<StudentViewModel>>(students);
            return View(studentVMs);
        }

        // GET: Students/Details/5  
        public async Task<IActionResult> Details(int id)
        {
            var student = await studentRepository.GetByIdAsync(id);
            if (student == null) return NotFound();
            var viewModel = mapper.Map<StudentViewModel>(student);
            return View(viewModel);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,EnrollmentNo,Email,Course,AdmissionDate")] Student studentData)
        {
            var student = mapper.Map<Student>(studentData);
            await studentRepository.AddAsync(student);
            return RedirectToAction("Index");
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await studentRepository.GetByIdAsync(id);
            if (student == null) return NotFound();
            var viewModel = mapper.Map<StudentViewModel>(student);
            return View(viewModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EnrollmentNo,Email,Course,AdmissionDate")] Student viewModel)
        {
            if (id != viewModel.Id) return BadRequest();
            if (!ModelState.IsValid) return View(viewModel);

            var student = mapper.Map<Student>(viewModel);
            await studentRepository.UpdateAsync(student);
            return RedirectToAction("Index");
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await studentRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
