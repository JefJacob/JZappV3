using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JZappV3.Models;
using Microsoft.AspNet.Identity;

namespace JZappV3.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            if (TempData["ErrorMsg"] == null)
            {
                TempData["ErrorMsg"] = "";
            }
            ViewBag.ErrorMsg = TempData["ErrorMsg"].ToString();
            var job = db.Jobs.Where(j => (j.CommitedBy == null )).Distinct();
            return View(await job.ToListAsync());
        }
        //IndexUser
        public async Task<ActionResult> IndexUser(String username)
        {
            var job = db.Jobs.Where(j => (j.PostedBy  == username)).Distinct();
            return View(await job.ToListAsync());
        }
        public async Task<ActionResult> CommitedJobs(String username)
        {
            var job = db.Jobs.Where(j => (j.CommitedBy == username)).Distinct();
            return View(await job.ToListAsync());
        }
        // GET: Jobs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        public async Task<ActionResult> Commit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                if (job.PostedBy == User.Identity.GetUserName())
                {
                    TempData["ErrorMsg"] = " You cannot commit to your job";
                    return RedirectToAction("Index");
                }
                job.CommitedBy = User.Identity.GetUserName();
                db.Entry(job).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("CommitedJobs",new { username=User.Identity.GetUserName()});
            }
            return RedirectToAction("Index");
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "JobId,JobType,Description,StartDateTime,FinishDateTime")] Job job)
        {
            if (ModelState.IsValid)
            {
                job.PostedBy = User.Identity.GetUserName();
                db.Jobs.Add(job);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "JobId,JobType,Description,StartDateTime,FinishDateTime")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("IndexUser", new { username = User.Identity.GetUserName() });
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Job job = await db.Jobs.FindAsync(id);
            db.Jobs.Remove(job);
            await db.SaveChangesAsync();
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
