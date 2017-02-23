using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using CodeFirstDataAccess;
using CodeFirstModel;
using CodeFirstWebApp.Models;


namespace CodeFirstWebApp.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            try
            {
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BookEntityContext>());
                return View();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult GetBookDetails()
        {
            var model = new List<BookModel>();
            try
            {
                using (var context = new BookEntityContext())
                {
                    var value = context.Books.ToList();
                    foreach (var book in value)
                    {
                        var bookModel = new BookModel();
                        bookModel.BookName = book.BookName;
                        bookModel.Author = book.Author;
                        bookModel.Edition = book.Edition;
                        bookModel.Publishing = book.Publishing;

                        model.Add(bookModel);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return PartialView("_BookDetailView", model);
        }

        public ActionResult InsertBookDetail()
        {
            try
            {
                for (int counter = 0; counter < 5; counter++)
                {
                    var book = new Book()
                    {
                        BookName = "Book " + counter,
                        Author = "Author " + counter,
                        Edition = "Edition " + counter,
                        Publishing = "Publishing " + counter
                    };
                    using (var context = new BookEntityContext())
                    {
                        context.Books.Add(book);
                        context.SaveChanges();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return Json(true);
        }
    }
}