using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Setup;
using Setup.Models;
using Setup.Repository.AuthRepo;

namespace Setup.Controllers
{
    public class UsersController(IUserRepository userRepository, IMapper mapper) : Controller
    {

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await userRepository.GetAllAsync();
            var userVMs = mapper.Map<List<UserViewModel>>(users);
            return View(userVMs);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var viewModel = mapper.Map<UserViewModel>(user);
            return View(viewModel);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,MobileNumber,Password,RoleId")] User userData)
        {
            var user = mapper.Map<User>(userData);
            await userRepository.AddAsync(user);
            return RedirectToAction("Index");
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            var viewModel = mapper.Map<UserViewModel>(user);
            return View(viewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,MobileNumber,Password,RoleId")] User userData)
        {
            if (id != userData.Id)
            {
                return BadRequest();
            }
            var user = mapper.Map<User>(userData);
            user.Id = id;
            await userRepository.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await userRepository.DeleteAsync(id);
            return RedirectToAction("Index");

        }
    }
}
