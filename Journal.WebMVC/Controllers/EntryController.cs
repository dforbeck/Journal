using Journal.Models;
using Journal.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Journal.WebMVC.Controllers
{
    [Authorize]
    public class EntryController : Controller
    {
        // GET: Entry
        //displays all the notes for the current user
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EntryService(userId);
            var model = service.GetEntries();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        //makes sure valid, grabs current id, calls on createEntry and returns back to index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntryCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EntryService(userId);

            service.CreateEntry(model);

            return RedirectToAction("Index");
           
        }

    }
}