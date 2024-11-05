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

        //RETURNS USER ACTIVITES IF THERE IS ANY
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

        //RETURN ALL USER ACTIVITIES IN A LIST
        public async Task<IActionResult> Index() {

            return View(await _context.UserActivities.ToListAsync());
        }
    }
}
