using LawyerOffice.Models.DAL;
using LawyerOffice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawyerOffice.Controllers
{
    public class HomeController : Controller
    {


        private LawyerContext _db = new LawyerContext();

        private ClientService _service = new ClientService();

        #region index

        public ActionResult Index()
        {

            if (Session["empName"]!=null)
            {

                string name = Session["empName"].ToString();

                var empid = _db.Employee.Where(x => x.employeeName == name).FirstOrDefault();

                if (empid == null)
                {
                    return RedirectToAction("HavntAccess", "Employees");

                }
                else
                {


                    var s = _db.HowCanAcess.Where(ss => ss.employeeID == empid.id).FirstOrDefault();


                    if (s != null)
                    {
                        ViewBag.Cases = s.Cases;
                        ViewBag.Client = s.Clients;
                        ViewBag.agend = s.Agenda;
                        ViewBag.users = s.Employee;
                        ViewBag.Setting = s.Setting;
                        ViewBag.statics = s.Staticis;
                        ViewBag.library = s.library;



                        return View(s);
                    }
                }
                return RedirectToAction("Login", "Employees");

            }
            return RedirectToAction("Login", "Employees");


        }
        #endregion

        #region about


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        #endregion

        #region contact


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion

    }
}