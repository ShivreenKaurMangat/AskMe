﻿using AskMe.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AskMe.Controllers
{
  [Authorize]
  public class CommentsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Comments
    public ActionResult Index(int answerId)
    {
      ViewBag.AnswerId = answerId;
      var comments = db.Comments.Include(c => c.Answer).Where(c => c.AnswerId == answerId);
      return View(comments.ToList());
    }

    // GET: Comments/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Comment comment = db.Comments.Find(id);
      if (comment == null)
      {
        return HttpNotFound();
      }
      return View(comment);
    }

    // GET: Comments/Create
    public ActionResult Create(int id)
    {
      var comment = new Comment()
      {
        AnswerId = id
      };
      ViewBag.AnswerId = new SelectList(db.Answers, "PostId", "PostId");
      return View(comment);
    }

    // POST: Comments/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,CommentText,AnswerId")] Comment comment)
    {
      if (ModelState.IsValid)
      {
        db.Comments.Add(comment);
        db.SaveChanges();
        return RedirectToAction("Index", new { answerId = comment.AnswerId });
      }

      ViewBag.AnswerId = new SelectList(db.Answers, "PostId", "PostId", comment.AnswerId);
      return View(comment);
    }

    // GET: Comments/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Comment comment = db.Comments.Find(id);
      if (comment == null)
      {
        return HttpNotFound();
      }
      ViewBag.AnswerId = new SelectList(db.Answers, "PostId", "PostId", comment.AnswerId);
      return View(comment);
    }

    // POST: Comments/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,CommentText,AnswerId")] Comment comment)
    {
      if (ModelState.IsValid)
      {
        db.Entry(comment).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index", new { answerId = comment.AnswerId });
      }
      ViewBag.AnswerId = new SelectList(db.Answers, "PostId", "PostId", comment.AnswerId);
      return View(comment);
    }

    // GET: Comments/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Comment comment = db.Comments.Find(id);
      if (comment == null)
      {
        return HttpNotFound();
      }
      return View(comment);
    }

    // POST: Comments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Comment comment = db.Comments.Find(id);
      db.Comments.Remove(comment);
      db.SaveChanges();
      return RedirectToAction("Index", new { answerId = comment.AnswerId });
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
