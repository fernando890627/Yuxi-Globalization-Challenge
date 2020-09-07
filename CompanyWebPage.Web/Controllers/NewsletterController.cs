using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWebPage.Web.Controllers
{
    public class NewsletterController : Controller
    {
        public IActionResult Subscribe()
        {
            return View(new ViewModel.NewsleterSubscribeViewModel());
        }

        [HttpPost]
        public IActionResult Subscribe(ViewModel.NewsleterSubscribeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // save data (todo: in the future)

                // redirect to /Home/Index
                return RedirectToAction(nameof(HomeController.Index), "Home");                
            }
            else
                return View(model);
        }
    }
}