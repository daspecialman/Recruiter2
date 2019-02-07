using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recruiter.Context;

namespace Recruiter.Controllers
{
	
    public class DocumentController : Controller
    {
        // GET: Document
		[HttpGet]
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Upload(HttpPostedFileBase file)
		{
			var model = Server.MapPath("~/App_Data/UploadedFiles/") + file.FileName;
			TempData["type"] = file.ContentType;
			if (file.ContentLength > 0)
			{
				RecruiterContext db = new RecruiterContext();
				file.SaveAs(model);
				ViewBag.Msg = "Uploaded Successfully";
				return View("Index");
			}
			else
			{
				ViewBag.Msg = "Upload Failed";
			}
			return View("Index");
		}
	}
}