using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieMvcProject.Models;
using PagedList;

namespace MovieMvcProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private MoviesDb db = new MoviesDb();

        [AllowAnonymous]
        public ActionResult Index(int genreid)
        {
            List<Movie> movies = db.Movies1.Where(x => x.GenreId == genreid).ToList();
            return View(movies);
        }
        [AllowAnonymous]
        public ActionResult AllMovies(int? page)
        {
           // List<Movie> movies = db.Movies1.ToList().ToPagedList(page?? 1,5);
            return View(db.Movies1.ToList().ToPagedList(page ?? 1, 10));
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult AllMovies()
        {
            List<Movie>movies = db.Movies1.ToList();
            return View(movies);
        }
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies1.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        
        public ActionResult Create()
        {
            Movie movie = new Movie();
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");
            return View(movie);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieId,Name,DirectorName,Description,Year,GenreId")] Movie movie, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                          + file.FileName);
                    movie.ImagePath = file.FileName;
                }
                db.Movies1.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", movie.GenreId);
            return View(movie);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies1.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", movie.GenreId);
            return View(movie);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Name,DirectorName,Description,Year,GenreId")] Movie movie, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                          + file.FileName);
                    movie.ImagePath = file.FileName;
                }
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllMovies");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", movie.GenreId);
            return View(movie);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies1.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies1.Find(id);
            db.Movies1.Remove(movie);
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
