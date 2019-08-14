using LawyerOffice.Models;
using LawyerOffice.Models.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawyerOffice.Controllers
{
    public class LibraryController : Controller
    {

        private LawyerContext _db = new LawyerContext();

        // GET: Library
        public ActionResult Index()
        {

            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();

                if (_allowAll.library == true)
                {
                    return View();

                }
                return RedirectToAction("HavntAccess", "Employees");

            }
            return RedirectToAction("HavntAccess", "Employees");
        }
        public ActionResult ListofBooks()
        {


            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();

                if (_allowAll.library == true)
                {
                    return View(_db.Library.ToList());

                }
                return RedirectToAction("HavntAccess", "Employees");

            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        [HttpGet]
        public ActionResult AddBook()
        {


            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();

                if (_allowAll.library == true)
                {
                    return View();

                }
                return RedirectToAction("HavntAccess", "Employees");

            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        [HttpPost]
        public ActionResult AddBook(Library _lib)
        {
            if (Session["empName"] != null)
            {

                // create new folder

                var path = _db.Info.Select(f => f.PhyscialPath).FirstOrDefault();
                string foldername = "المكتبه";
                var folder = Path.Combine(path, foldername);
                Directory.CreateDirectory(folder);


                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();

                if (_allowAll.library == true)
                {


                    string filNameS = Path.GetFileNameWithoutExtension(_lib.BookFile.FileName);

                    string exten = Path.GetExtension(_lib.BookFile.FileName);
                    filNameS = filNameS + DateTime.Now.ToString("yymmssff") + exten;

                    string documentPath = _db.Info.Select(f => f.DocumentPath).FirstOrDefault();

                    string firstPath = documentPath + "\\" + foldername + "\\";
                    _lib.BookPath = firstPath + filNameS;


                    filNameS = Path.Combine(Server.MapPath(firstPath), filNameS);


                    _lib.BookFile.SaveAs(filNameS);
                    _lib.Date = DateTime.Now;

                    _lib.Publisher = Session["empName"].ToString();

                    _db.Library.Add(_lib);

                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }



        public ActionResult readBook(int id)
        {


            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();

                if (_allowAll.library == true)
                {
                    return View(_db.Library.Where(p => p.id == id).FirstOrDefault());

                }
                return RedirectToAction("HavntAccess", "Employees");

            }
            return RedirectToAction("HavntAccess", "Employees");


        }


    }
}