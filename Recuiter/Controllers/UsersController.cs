using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
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
    public class UsersController : Controller
    {
        private RecruiterContext db = new RecruiterContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.CreatedBy).Include(u => u.Department).Include(u => u.LastModifiedBy).Include(u => u.Roles).Where(u => u.IsDeleted == false);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var user = Membership.GetUser(User.Identity.Name) as CustomMembershipUser;

                var users = new User
                {
                    Username = userVM.Username,
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    Email = userVM.Email,
                    Password = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(userVM.Password))),
                    CreatedDate = DateTime.Now
                };
                
                if (user != null)
                {
                    users.CreatedById = user.UserId;
                }

                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", user.CreatedById);
            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", user.DepartmentId);
            //ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username", user.LastModifiedById);
            return View(userVM);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", user.CreatedById);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", user.DepartmentId);
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username", user.LastModifiedById);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedDate,LastModifiedDate,CreatedById,LastModifiedById,IsDeleted,Username,FirstName,LastName,Email,Password,IsActive,DepartmentId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", user.CreatedById);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", user.DepartmentId);
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username", user.LastModifiedById);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            //db.Users.Remove(user);
            user.IsDeleted = true;
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
