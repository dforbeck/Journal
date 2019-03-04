using Journal.Models;
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
        public ActionResult Index()
        {
            var model = new EntryListItem[0];
            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }


    }
}