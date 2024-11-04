using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeShare.Data;
using RecipeShare.Models;

namespace RecipeShare.Controllers {
    public class UserActivitiesController : Controller {

        private readonly ApplicationDbContext _context;

        public UserActivitiesController(ApplicationDbContext context) {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Analytics() {

            var userActivities = await _context.UserActivities.ToListAsync();
            
            if (userActivities.Count != 0) {
             
                return View(userActivities);
            }
            else {
                
                return View();
            }
        }

        // GET: UserActivities
        public async Task<IActionResult> Index() {
            return View(await _context.UserActivities.ToListAsync());
        }

        // GET: UserActivities/Details/5
        public async Task<IActionResult> Details(int? id) {
        
            if (id == null) {
            
                return NotFound();
            }

            var userActivity = await _context.UserActivities.FirstOrDefaultAsync(m => m.Id == id);
            
            if (userActivity == null) {
            
                return NotFound();
            }

            return View(userActivity);
        }

        // GET: UserActivities/Create
        public IActionResult Create() {
            return View();
        }

        // POST: UserActivities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Url,Username,IpAddress")] UserActivity userActivity) {
            
            if (ModelState.IsValid) {
            
                _context.Add(userActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
         
            return View(userActivity);
        }

        // GET: UserActivities/Edit/5
        public async Task<IActionResult> Edit(int? id) {
        
            if (id == null) {
            
                return NotFound();
            }

            var userActivity = await _context.UserActivities.FindAsync(id);
            
            if (userActivity == null) {
            
                return NotFound();
            }
            
            return View(userActivity);
        }

        // POST: UserActivities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Url,Username,IpAddress")] UserActivity userActivity) {
            
            if (id != userActivity.Id) {
            
                return NotFound();
            }

            if (ModelState.IsValid) {
                
                try {
                    _context.Update(userActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                
                    if (!UserActivityExists(userActivity.Id)) {
                    
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userActivity);
        }

        // GET: UserActivities/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            
            if (id == null) {
            
                return NotFound();
            }

            var userActivity = await _context.UserActivities.FirstOrDefaultAsync(m => m.Id == id);
            
            if (userActivity == null) {
            
                return NotFound();
            }

            return View(userActivity);
        }

        // POST: UserActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
        
            var userActivity = await _context.UserActivities.FindAsync(id);
            
            if (userActivity != null) {
            
                _context.UserActivities.Remove(userActivity);
            }

            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool UserActivityExists(int id) {
            return _context.UserActivities.Any(e => e.Id == id);
        }
    }
}
