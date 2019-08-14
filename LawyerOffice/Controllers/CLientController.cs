using LawyerOffice.Models;
using LawyerOffice.Models.DAL;
using LawyerOffice.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LawyerOffice.Controllers
{
    public class CLientController : Controller
    {



        private LawyerContext _db = new LawyerContext();

        private ClientService _service = new ClientService();



        #region index

        // GET: CLient
        public ActionResult Index()

        {

            if (Session["empName"] != null)
            {


                string name = Session["empName"].ToString();



                var _empID = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();

                var ss = _db.Cases.Where(d => d.employeeName == _empID.id.ToString()).FirstOrDefault();

                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == _empID.id).FirstOrDefault();
                ViewBag.accessAll = _allowAll.AccessAll;
                var model = new ClientWithCases();
                model.empX = _db.Employee.ToList();

                if (_allowAll.AccessAll == true)
                {
                    model.ClientXs = _db.Client.ToList();
                    return View(model);

                }

                else if (_allowAll.Clients == true)
                {

                    model.ClientXs = _db.Client.Where(p => p.employeeName == _empID.id).ToList();
                    return View(model);
                }

            }
            return RedirectToAction("HavntAccess", "Employees");




        }

        #endregion

        #region Create -- secure

        [HttpGet]

        public ActionResult Create()

        {

            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                string empid = ss.id.ToString();
                HowCanAcess _hca = _db.HowCanAcess.Where(s => s.employeeID.ToString() == empid).FirstOrDefault();

                var LawyerJobID = _db.Jobs.Where(p => p.jobs == "محامى" || p.jobs == "محامي").Select(f => f.id).FirstOrDefault();
                //  ViewBag.Employee = new SelectList(_db.Employee.Where(s => s.jobs.ToString() == "1"), "id", "employeeName");
                ViewBag.Employee = new SelectList(_db.Employee.Where(s => s.jobs.ToString() == LawyerJobID.ToString()), "id", "employeeName");

                if (_hca.AccessAll == true)
                {
                    return View();
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        [HttpPost]

        public ActionResult Create(Client client)

        {

            var LawyerJobID = _db.Jobs.Where(p => p.jobs == "محامى" || p.jobs == "محامي").Select(f => f.id).FirstOrDefault();
            //  ViewBag.Employee = new SelectList(_db.Employee.Where(s => s.jobs.ToString() == "1"), "id", "employeeName");
            ViewBag.Employee = new SelectList(_db.Employee.Where(s => s.jobs.ToString() == LawyerJobID.ToString()), "id", "employeeName");

            _service.NewClient(client);

            TempData["ClientAdded"] = "تم اضافه العميل ";
            return RedirectToAction("Index");
        }

        #endregion

        #region Search


        public ViewResult Search(string SeacrhText)

        {

            var dd = _db.Client.Where(s => s.Name.StartsWith(SeacrhText));

            return View("search", dd);

        }

        #endregion

        #region  Delete --secure

        [HttpGet]
        public ActionResult Delete(int? id)

        {

            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();

                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Client client = _db.Client.Find(id);
                int empid = ss.id;

                if (client == null)
                {
                    return HttpNotFound();
                }
                else if (client.employeeName == empid || _allowAll.AccessAll == true)
                {
                    return View(client);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = _db.Client.Find(id);
            _db.Client.Remove(client);
            _db.SaveChanges();
            TempData["ClientDelete"] = "تم الحذف بنجاح  ";

            return RedirectToAction("Index");
        }

        #endregion


        #region Edit --secure
        // GET: test/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();

                ViewBag.Employee = new SelectList(_db.Employee.Where(s => s.jobs.ToString() == "1"), "id", "employeeName");

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Client client = _db.Client.Find(id);
                int empid = ss.id;

                if (client == null)
                {
                    return HttpNotFound();
                }
                else if (client.employeeName == empid || _allowAll.AccessAll == true)
                {
                    return View(client);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");


        }

        // POST: test/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client client)
        {
            ViewBag.Employee = new SelectList(_db.Employee.Where(s => s.jobs.ToString() == "1"), "id", "employeeName");

            if (ModelState.IsValid)
            {
                _db.Entry(client).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["ClientEdit"] = "تم التعديل بنجاح  ";

                return RedirectToAction("Index");
            }
            return View(client);
        }


        #endregion

        #region Detalis --secure

        // GET: test/Details/5
        public ActionResult Details(int? id, Courts _court)
        {

            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();
                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();

                int empid = ss.id;

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Client client = _db.Client.Find(id);

                var model = new ClientWithCases();
                var cases = _db.Cases.Where(s => s.id == id).ToList();
                //var cse = cases.FirstOrDefault();
                model.CaseX = _db.Cases.Where(s => s.clientID == id).ToList();

                var _client = _db.Client.Where(s => s.ID == id);
                model.ClientX = _client.FirstOrDefault();

                model.CourtX = _db.Court.ToList();


                // model.ClientX = client.;

                if (client == null)
                {
                    return HttpNotFound();
                }
                else if (client.employeeName == empid || _allowAll.AccessAll == true)
                {
                    return View(model);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");
        }
        #endregion

        #region NewCase -- secure


        [HttpGet]

        public ActionResult NewCase(int? id)

        {


            if (Session["empName"] != null)
            {


                // check user have access
                string name = Session["empName"].ToString();
                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();

                int empid = ss.id;

                ViewBag.Courts = new SelectList(_db.Court.ToList(), "id", "CourtName");
                ViewBag.CourtType = new SelectList(_db.CourtType.ToList(), "id", "TypeName");

                var LawyerJobID = _db.Jobs.Where(p => p.jobs == "محامى" || p.jobs == "محامي").Select(f => f.id).FirstOrDefault();
                //  ViewBag.Employee = new SelectList(_db.Employee.Where(s => s.jobs.ToString() == "1"), "id", "employeeName");
                ViewBag.Employee = new SelectList(_db.Employee.Where(s => s.jobs.ToString() == LawyerJobID.ToString()), "id", "employeeName");

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Client client = _db.Client.Find(id);
                if (client == null)
                {
                    return HttpNotFound();
                }
                else if (client.employeeName == empid || _allowAll.AccessAll == true)
                {
                    return View();
                }



            }
            return RedirectToAction("HavntAccess", "Employees");


        }

        [HttpPost]

        public ActionResult NewCase(Cases _newcase, int? id)

        {
            _newcase.clientID = _db.Client.First(s => s.ID == id).ID;

            _newcase.dateofElkaeed = DateTime.Now;
            _db.Cases.Add(_newcase);

            //_newcase.DateOfRecive = DateTime.Now;


            _db.SaveChanges();

            // create new folder

            var path = _db.Info.Select(f => f.PhyscialPath).FirstOrDefault();
            var folder = Path.Combine(path, _newcase.numberOfCase.ToString());

            Directory.CreateDirectory(folder);

            TempData["CaseAdded"] = "تم اضافه القضيه بنجاح";

            return RedirectToAction("Index");
        }



        public JsonResult checkCaseID(int numberOfCase)
        {

            System.Threading.Thread.Sleep(200);
            var searchData = _db.Cases.Where(p => p.numberOfCase == numberOfCase).FirstOrDefault();
            if (searchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        #endregion


    }
}

