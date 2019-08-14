using LawyerOffice.Models;
using LawyerOffice.Models.DAL;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LawyerOffice.Controllers
{

    public class EmployeesController : Controller
    {
        private LawyerContext _db = new LawyerContext();


        #region index

        public ActionResult Index()
        {
            var model = new EmpOfficeJob();
            model.JobX = _db.Jobs.ToList();
            model.OfficeX = _db.Offices.ToList();
            model.hcaX = _db.HowCanAcess.ToList();

            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();

                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();



                if (_allowAll.AccessAll == true)
                {
                    model.Empx = _db.Employee.ToList();


                    return View("index", model);
                }
                else if (_allowAll.Employee == true)
                {
                    model.Empx = _db.Employee.ToList();


                    return View("index", model);
                }


            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        #endregion

        #region Login

        [HttpGet]
        public ActionResult Login()

        {

            return View();

        }

        [HttpPost]
        public ActionResult Login(Employees _emp, Logs _log)
        {

            if (_emp.employeeName == "uptop" && _emp.Paswword == "11")
            {
                Session["uptop"] = "true";

                Session["empName"] = "uptop";
                ViewBag.uptop = true;
                Session.Timeout = 1440;
                return RedirectToAction("uptop", "Mange");

            }
            else
            {
                var _user = _db.Employee.Where(s => s.employeeName == _emp.employeeName && s.Paswword == _emp.Paswword).FirstOrDefault();
                if (_user != null)
                {
                    string ip = Request.UserHostAddress;

                    Session.Add("empName", _user.employeeName);
                    _log.username = Session["empName"].ToString();
                    _log.date = DateTime.Now;

                    _log.ipaddress = ip;
                    _db.Logs.Add(_log);
                    _db.SaveChanges();
                    Session.Timeout = 1440;


                    return RedirectToAction("Index", "Home");
                }

            }
            return View();

        }
        #endregion

        #region Log off

        public ActionResult LogOff(Logs _log)
        {
            if (Session["empName"] != null)
            {


                _log.username = Session["empName"].ToString();
                _log.date = DateTime.Now;
                string ip = Request.UserHostAddress;

                _log.ipaddress = ip;
                _db.Logs.Add(_log);
                _db.SaveChanges();

                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");


        }
        #endregion

        #region new Employee -- secure
        public ActionResult newEmployee()

        {
            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                string empid = ss.id.ToString();
                HowCanAcess _hca = _db.HowCanAcess.Where(s => s.employeeID.ToString() == empid).FirstOrDefault();

                ViewBag.Offices = new SelectList(_db.Offices.ToList(), "id", "OfficeName");
                ViewBag.Jobs = new SelectList(_db.Jobs.ToList(), "id", "jobs");

                if (_hca.Employee == true)
                {
                    return View();
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        [HttpPost]

        public ActionResult newEmployee(Employees _newEmp, HowCanAcess hca, Logs _log)

        {

            _db.Employee.Add(_newEmp);
            _db.SaveChanges();

            // add check boxs
            hca.employeeID = _newEmp.id;
            _db.HowCanAcess.Add(hca);

            _db.SaveChanges();

            TempData["AddEmp"] = "تم اضافه الموظف بنجاح ";
            _log.username = Session["empName"].ToString();
            _log.date = DateTime.Now;
            string ip = Request.UserHostAddress;

            _log.ipaddress = ip;
            _db.Logs.Add(_log);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Edit  -- secure
        // GET: employee/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                string empid = ss.id.ToString();
                HowCanAcess _hca = _db.HowCanAcess.Where(s => s.employeeID.ToString() == empid).FirstOrDefault();

                ViewBag.Offices = new SelectList(_db.Offices.ToList(), "id", "OfficeName");
                ViewBag.Jobs = new SelectList(_db.Jobs.ToList(), "id", "jobs");

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employees _emp = _db.Employee.Find(id);
                // HowCanAcess _hca = _db.HowCanAcess.Where(s => s.employeeID == _emp.id).FirstOrDefault();

                //   var model = new hcaAndEmp();
                // model.EmpX = _emp;
                //model.hcaX = _hca;

                if (_emp == null)
                {
                    return HttpNotFound();
                }
                else if (_hca.Employee == true)
                {
                    return View(_emp);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        // POST: Case/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employees _emp, HowCanAcess hca)
        {

            if (Session["empName"] != null)
            {

                ViewBag.Offices = new SelectList(_db.Offices.ToList(), "id", "OfficeName");
                ViewBag.Jobs = new SelectList(_db.Jobs.ToList(), "id", "jobs");

                if (_emp != null)
                {
                    _db.Entry(_emp).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["EditEmp"] = "تم التعديل بنجاح";
                    return RedirectToAction("Index");
                }

                return View(_emp);
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        #endregion

        #region Privilage -- secures

        [HttpGet]
        public ActionResult Privilage(int? id)
        {
            if (Session["empName"] != null)
            {
                HowCanAcess _hca = _db.HowCanAcess.Where(s => s.ID == id).FirstOrDefault();


                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();

                Session["empID"] = _hca.employeeID;

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                string empid = ss.id.ToString();
                HowCanAcess _hca2 = _db.HowCanAcess.Where(s => s.employeeID.ToString() == empid).FirstOrDefault();


                if (_hca == null)
                {
                    return HttpNotFound();

                }
                else if (_hca2.Employee == true)
                {
                    return View(_hca);
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Privilage(HowCanAcess _hca, int id, Logs _log)
        {

            if (Session["empName"] != null)
            {
                string v = Session["empID"].ToString();

                _hca.employeeID = int.Parse(v);
                _db.Entry(_hca).State = EntityState.Modified;
                _db.SaveChanges();

                TempData["editePrrivelage"] = "تم تعديل الصلاحيات ل الموظف";
                _log.username = Session["empName"].ToString();
                _log.date = DateTime.Now;
                string ip = Request.UserHostAddress;

                _log.ipaddress = ip;
                _db.Logs.Add(_log);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("HavntAccess", "Employees");
        }

        #endregion 

        #region delete -- secure


        [HttpGet]
        public ActionResult Delete(int id)

        {
            if (Session["empName"] != null)
            {


                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                string empid = ss.id.ToString();
                HowCanAcess _hca = _db.HowCanAcess.Where(s => s.employeeID.ToString() == empid).FirstOrDefault();

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employees emp = _db.Employee.Find(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                else if (_hca.Employee == true)
                {

                    var model = new EmpOfficeJob();
                    model.EmpY = emp;
                    model.JobX = _db.Jobs.ToList();
                    model.OfficeX = _db.Offices.ToList();
                    return View(model);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees emp = _db.Employee.Find(id);

            HowCanAcess hca = _db.HowCanAcess.Where(d => d.employeeID == emp.id).FirstOrDefault();
            if (hca == null)
            {
                return RedirectToAction("HavntAccess", "Employees");

            }
            _db.HowCanAcess.Remove(hca);
            _db.Employee.Remove(emp);
            _db.SaveChanges();
            TempData["DeleteEmp"] = "تم مسح بيانات الموظف";
            return RedirectToAction("Index");
        }

        #endregion

        #region Havent access


        [HttpGet]
        public ActionResult HavntAccess()
        {

            return View();

        }
        #endregion

        #region profile -- secure

        [HttpGet]
        public ActionResult profile()
        {



            if (Session["empName"] != null)
            {



                string item = Session["empName"].ToString();

                var model = new EmpOfficeJob();
                model.Empx = _db.Employee.Where(s => s.employeeName == item);
                model.JobX = _db.Jobs.ToList();
                model.OfficeX = _db.Offices.ToList();
                model.EmpY = _db.Employee.Where(p => p.employeeName == item).FirstOrDefault();
                if (model == null)
                {
                    return View("HavntAccess");

                }
                return View(model);

            }
            return RedirectToAction("HavntAccess", "Employees");
        }

        #endregion


    }
}