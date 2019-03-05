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

            var service = CreateEntryService();

            if (service.CreateEntry(model))
            {
                TempData["SaveResult"] = "Your entry was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Entry could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateEntryService();
            var model = svc.GetEntryById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateEntryService();
            var detail = service.GetEntryById(id);
            var model = new EntryEdit
            {
                EntryId = detail.EntryId,
                Title = detail.Title,
                Content = detail.Content
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (int id, EntryEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.EntryId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateEntryService();

            if (service.UpdateEntry(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateEntryService();
            var model = svc.GetEntryById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateEntryService();
            service.DeleteEntry(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        public EntryService CreateEntryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EntryService(userId);
            return service;
        }
    }
}