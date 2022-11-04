using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using supra.Context;
using supra.Models;

namespace supra.Models.Areas.Supervisor.Controllers
{
    [Area("Supervisor")]
    [Authorize(Roles = "Admin")]
    public class SupervisorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}