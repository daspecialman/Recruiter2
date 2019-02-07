using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Data.Models;
using Recruiter.Context;
using Recruiter.CustomAuthentication;
using Recruiter.ViewModels;


namespace Recruiter.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private RecruiterContext db = new RecruiterContext();

        // GET: Departments
        public ActionResult Index()
        {
            var departments = db.Departments.Include(d => d.CreatedBy).Include(d => d.HoD).Include(d => d.LastModifiedBy).Where(d => d.IsActive == false);
            return View(departments.ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username");
            ViewBag.HoDId = new SelectList(db.Users, "Id", "Username");
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentVM departmentVM)
        {
            if (ModelState.IsValid)
            {

                var user = Membership.GetUser(User.Identity.Name) as CustomMembershipUser;

                var department = new Department
                {
                    Name = departmentVM.Name,
                    CreatedDate = DateTime.Now,
                    HoDId = departmentVM.HoDId
                };

                if (user != null)
                {
                    department.CreatedById = user.UserId;
                }

                department.CreatedDate = DateTime.Now;

                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HoDId = new SelectList(db.Users, "Id", "Username", departmentVM.HoDId);
            return View(departmentVM);
        }
        

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", department.CreatedById);
            ViewBag.HoDId = new SelectList(db.Users, "Id", "Username", department.HoDId);
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username", department.LastModifiedById);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                var user = Membership.GetUser(User.Identity.Name) as CustomMembershipUser;

                if (user != null)
                {
                    department.LastModifiedById = user.UserId;
                }

                department.LastModifiedDate = DateTime.Now;

                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", department.CreatedById);
            ViewBag.HoDId = new SelectList(db.Users, "Id", "Username", department.HoDId);
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username", department.LastModifiedById);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            //db.Departments.Remove(department);
            department.IsActive = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
