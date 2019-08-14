using LawyerOffice.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawyerOffice.Controllers
{
    public class AgendaController : Controller
    {
        private LawyerContext _db = new LawyerContext();

        public ActionResult index()
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();
                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();

                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();

                if (_allowAll.Agenda==true)
                {
                    return View();

                }
            }
            return RedirectToAction("HavntAccess", "Employees");


        }


        // GET: Agenda
        public ActionResult getdata()
        {

            

                string name = Session["empName"].ToString();
                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _cases = _db.Cases.Where(dd => dd.employeeName == emp.id.ToString());

            var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();
            if (_allowAll.AccessAll)
            {
                return Json(_db.elgalasat.AsEnumerable().Select(e => new
                {
                    id = e.id,
                    title = "رقم القضيه" + e.NumberOfCase + "  " + "  المطلوب " + e.orders,
                    start = e.dateOfGalsa.AddDays(1).ToString("MM/dd/yyyy"),
                }), JsonRequestBehavior.AllowGet);
            }
            return Json(_db.elgalasat.Where(d => d.EmpName == emp.id.ToString()).AsEnumerable().Select(e => new
                {
                    id = e.id,
                    title = "رقم القضيه" + e.NumberOfCase +"  "+ " المطلوب " + e.orders,
                    start = e.dateOfGalsa.AddDays(1).ToString("MM/dd/yyyy"),
                }), JsonRequestBehavior.AllowGet);
           
        }




    }
}