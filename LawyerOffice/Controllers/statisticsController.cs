using CrystalDecisions.CrystalReports.Engine;
using LawyerOffice.Models;
using LawyerOffice.Models.DAL;
using Microsoft.Reporting.WebForms;
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
    public class statisticsController : Controller
    {
        private LawyerContext _db = new LawyerContext();

        #region index --secure

        // GET: statistics
        public ActionResult Index()
        {
            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();
                if (_allowAll.Staticis == true)
                {
                    return View();
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        #endregion

        #region employeeAcconts --secure

        public ActionResult employeeAcconts()
        {
            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();
                if (_allowAll.Staticis == true)
                {


                    var model = new SalaryWithEmpAndMonth();
                    model.officeX = _db.Offices.ToList();
                    model.jobx = _db.Jobs.ToList();
                    model.EmpX = _db.Employee.ToList();


                    return View(model);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        #endregion


        #region salaries --secure

        public ActionResult Salary(int id, Logs _log)
        {
            ViewBag.empIdForPDF = id;
            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();
                if (_allowAll.Staticis == true)
                {
                    ViewBag.Month = new SelectList(_db.Month.ToList(), "id", "months");
                    Session["id"] = id;
                    var empID = _db.Employee.Where(s => s.id == id).FirstOrDefault();
                    // var vv = _db.Salaries.Where(s => s.Employee == empID.id.ToString()).ToList();

                    var model = new SalaryWithEmpAndMonth();
                    model.EmpX = _db.Employee.ToList();
                    model.MonthsX = _db.Month.ToList();
                    model.salaryX = _db.Salaries.Where(s => s.Employee == empID.id.ToString()).ToList();


                    _log.username = Session["empName"].ToString();
                    _log.date = DateTime.Now;
                    string ip = Request.UserHostAddress;

                    _log.ipaddress = ip;
                    _db.Logs.Add(_log);
                    _db.SaveChanges();
                    return View(model);
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        #endregion

        #region payMoney for employee --secure

        [HttpPost]
        public ActionResult PayMoney(Salaries salary, int _salary, string months)
        {
            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();
                if (_allowAll.Staticis == true)
                {

                    salary.Employee = Session["id"].ToString();
                    salary.Date = DateTime.Now;
                    salary.month = months;
                    salary.empSupplyer = Session["empName"].ToString();
                    salary.salary = _salary;
                    _db.Salaries.Add(salary);
                    _db.SaveChanges();
                    return RedirectToAction("Salary/" + Session["id"]);
                }
                //   return RedirectToAction("HavntAccess", "Employees");

            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        #endregion

        #region EditPayMoney --secure

        public ActionResult EditPayMoney(int? id)
        {
            if (Session["empName"] != null)
            {
                ViewBag.Month = new SelectList(_db.Month.ToList(), "id", "months");

                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Salaries _salary = _db.Salaries.Find(id);
                string empid = ss.id.ToString();


                if (_salary == null)
                {
                    return HttpNotFound();
                }
                else if (_allowAll.Staticis == true)
                {

                    return View(_salary);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        // POST: statics/EditPayMoney/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPayMoney(int id, Salaries _salary, string months)
        {
            ViewBag.Month = new SelectList(_db.Month.ToList(), "id", "months");


            if (ModelState.IsValid)
            {
                _salary.Employee = Session["id"].ToString();

                _salary.empSupplyer = Session["empName"].ToString();

                _db.Entry(_salary).State = EntityState.Modified;
                _salary.Date = DateTime.Now;
                _salary.month = months;
                _db.SaveChanges();
                return RedirectToAction("Salary/" + Session["id"]);


            }
            return View(_salary);
        }

        #endregion

        #region TotalPayment --secure

        public ActionResult TotalPayments(string months)
        {
            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();
                if (_allowAll.Staticis == true)
                {
                    ViewBag.Month = new SelectList(_db.Month.ToList(), "id", "months");


                    var model = new AllPayments();
                    model.caseX = _db.Cases.ToList();

                    //  var cases = _db.Cases.ToList();
                    if (model.caseX != null)
                    {
                        int sumcases = model.caseX.Sum(s => s.money);
                        ViewBag.sumofcases = sumcases;
                    }
                    else
                    {
                        ViewBag.sumofcases = 0;

                    }



                    model.EmpX = _db.Employee.ToList();
                    if (model.EmpX != null)
                    {
                        int sumEmp = _db.Employee.Sum(s => s.Salary);
                        ViewBag.SumofEmp = sumEmp;

                    }
                    else
                    {
                        ViewBag.SumofEmp = 0;
                    }

                    model.feesX = _db.feeses.ToList();

                    if (model.feesX != null)
                    {
                        int sumFess = model.feesX.Sum(s => s.recived); ;
                        ViewBag.sumoffees = sumFess;
                    }
                    else
                    {
                        ViewBag.sumoffees = 0;
                    }


                    model.salaryX = _db.Salaries.ToList();
                    if (model.salaryX != null)
                    {
                        int sumSalary = model.salaryX.Sum(s => s.salary);
                        ViewBag.SumofSalary = sumSalary;

                    }
                    else
                    {
                        ViewBag.SumofSalary = 0;
                    }

                    ViewBag.subtractCase = ViewBag.sumofcases - ViewBag.sumoffees;
                    ViewBag.subtractSalary = ViewBag.SumofEmp - ViewBag.SumofSalary;
                    return View();
                }
            }
            return RedirectToAction("HavntAccess", "Employees");


        }
        #endregion

        #region search by month --secure

        public ActionResult SumMoneyByMonth(string months)
        {
            if (Session["empName"] != null)
            {
                if (months == null)
                {
                    HttpNotFound();
                }


                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();
                if (_allowAll.Staticis == true)
                {
                    ViewBag.Month = new SelectList(_db.Month.ToList(), "id", "months");

                    var model = new AllPayments();
                    model.caseX = _db.Cases.Where(s => s.dateofElkaeed.Month.ToString() == months).ToList();
                    var EmpCase = _db.Cases.Where(s => s.dateofElkaeed.Month.ToString() == months).FirstOrDefault();
                    var Emptsalary = _db.Salaries.Where(s => s.month == months).FirstOrDefault();

                    if (EmpCase == null && Emptsalary == null)
                    {
                        ViewBag.sumofcases = 0;


                        model.EmpX = _db.Employee.ToList();
                        ViewBag.SumofEmp = _db.Employee.Sum(s => s.Salary);

                        model.feesX = _db.feeses.Where(s => s.DateofRevice.Month.ToString() == months).ToList();
                        ViewBag.sumoffees = 0;

                        model.salaryX = _db.Salaries.Where(s => s.month == months).ToList();
                        ViewBag.SumofSalary = 0;

                        ViewBag.subtractCase = ViewBag.sumofcases - ViewBag.sumoffees;
                        ViewBag.subtractSalary = ViewBag.SumofEmp - ViewBag.SumofSalary;
                        ViewBag.month = months;
                        return View("SumMoneyByMonth");

                    }
                    else if (EmpCase == null)
                    {
                        ViewBag.sumofcases = 0;


                        model.EmpX = _db.Employee.ToList();
                        ViewBag.SumofEmp = _db.Employee.Sum(s => s.Salary);

                        model.feesX = _db.feeses.Where(s => s.DateofRevice.Month.ToString() == months).ToList();
                        ViewBag.sumoffees = 0;

                        model.salaryX = _db.Salaries.Where(s => s.month == months).ToList();
                        ViewBag.SumofSalary = _db.Salaries.Where(s => s.month == months).Sum(s => s.salary);

                        ViewBag.subtractCase = ViewBag.sumofcases - ViewBag.sumoffees;
                        ViewBag.subtractSalary = ViewBag.SumofEmp - ViewBag.SumofSalary;
                        ViewBag.month = months;
                        return View("SumMoneyByMonth");

                    }
                    else if (Emptsalary == null)
                    {
                        ViewBag.sumofcases = _db.Cases.Where(s => s.dateofElkaeed.Month.ToString() == months).Sum(s => s.money);

                        model.EmpX = _db.Employee.ToList();
                        ViewBag.SumofEmp = _db.Employee.Sum(s => s.Salary);

                        model.feesX = _db.feeses.Where(s => s.DateofRevice.Month.ToString() == months).ToList();
                        ViewBag.sumoffees = model.feesX.Where(s => s.DateofRevice.Month.ToString() == months).Sum(s => s.recived);

                        model.salaryX = _db.Salaries.Where(s => s.month == months).ToList();
                        ViewBag.SumofSalary = 0;

                        ViewBag.subtractCase = ViewBag.sumofcases - ViewBag.sumoffees;
                        ViewBag.subtractSalary = ViewBag.SumofEmp - ViewBag.SumofSalary;
                        ViewBag.month = months;
                        return View("SumMoneyByMonth");
                    }
                    ViewBag.sumofcases = _db.Cases.Where(s => s.dateofElkaeed.Month.ToString() == months).Sum(s => s.money);

                    model.EmpX = _db.Employee.ToList();
                    ViewBag.SumofEmp = _db.Employee.Sum(s => s.Salary);

                    model.feesX = _db.feeses.Where(s => s.DateofRevice.Month.ToString() == months).ToList();
                    ViewBag.sumoffees = _db.feeses.Where(s => s.DateofRevice.Month.ToString() == months).Sum(s => s.recived);

                    model.salaryX = _db.Salaries.Where(s => s.month == months).ToList();
                    ViewBag.SumofSalary = _db.Salaries.Where(s => s.month == months).Sum(s => s.salary);

                    ViewBag.subtractCase = ViewBag.sumofcases - ViewBag.sumoffees;
                    ViewBag.subtractSalary = ViewBag.SumofEmp - ViewBag.SumofSalary;
                    ViewBag.month = months;
                    return View("SumMoneyByMonth");
                }
            }
            return RedirectToAction("HavntAccess", "Employees");


        }
        #endregion



        #region print employee salary report --secure

        public ActionResult PrintReport(int id)
        {
            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp2 = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp2.id).FirstOrDefault();
                if (_allowAll.Staticis == true)
                {

                    ReportDocument rd = new ReportDocument();
                    var infoes = _db.Info.FirstOrDefault();

                    rd.Load(Path.Combine(Server.MapPath("~/Report/CrystalReport1.rpt")));
                    var emp = _db.Employee.Where(f => f.id == id).FirstOrDefault();
                    rd.SetDataSource(_db.Salaries.Where(f => f.Employee == id.ToString()).Select(p => new
                    {
                        id = p.id,
                        Employee = _db.Employee.Where(f => f.id.ToString() == p.Employee).Select(f => f.employeeName).FirstOrDefault(),
                        empSupplyer = p.empSupplyer,
                        salary = p.salary,
                        month = _db.Month.Where(f => f.id.ToString() == p.month).Select(f => f.months).FirstOrDefault(),
                        OfficeName = _db.Offices.Where(f => f.Id.ToString() == emp.OfficeName).Select(f => f.OfficeName).FirstOrDefault(),
                        Date = p.Date,
                        jobs = _db.Jobs.Where(f => f.id.ToString() == emp.jobs).Select(f => f.jobs),

                        //info data
                        img = infoes.Img.Replace(@"~\", ""),
                        Expr3 = infoes.description,
                        Number = infoes.Number,
                        PlaceName = infoes.PlaceName,
                    }).ToList());


                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "aaplication/pdf", "حسابات الموظفين.pdf");
                }

            }
            return RedirectToAction("HavntAccess", "Employees");
        }
        #endregion

        #region totalCasedetaols and printing report --secure


        public ActionResult totalCasedetaols()
        {

            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();
                if (_allowAll.Staticis == true)
                {
                    var model = new CasesAndFeeses();
                    model.CasesX = _db.Cases.ToList();
                    model.FeesesX = _db.feeses.ToList();
                    return View(model);

                }
                return RedirectToAction("HavntAccess", "Employees");

            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        public ActionResult PrintCasesDetails()
        {

            ReportDocument rd = new ReportDocument();
            var infoes = _db.Info.FirstOrDefault();

            rd.Load(Path.Combine(Server.MapPath("~/Report/PrintCasesDetails.rpt")));

            rd.SetDataSource(_db.feeses.Select(p => new
            {
                NumberOfCase = _db.Cases.Where(f => f.id == p.NumberOfCase).Select(f => f.numberOfCase).FirstOrDefault(),
                NumberOfCase1 = _db.Cases.Where(f => f.id == p.NumberOfCase).Select(f => f.numberOfCase).FirstOrDefault(),
                recived = p.recived,
                employeeName = p.EmployeeName,
                DateofRevice = p.DateofRevice,
                notes = p.notes,

                dateofElkaeed = _db.Cases.Where(f => f.id == p.NumberOfCase).Select(f => f.dateofElkaeed).FirstOrDefault(),
                money = _db.Cases.Where(f => f.id == p.NumberOfCase).Select(f => f.money).FirstOrDefault(),

                //info data
                img = infoes.Img.Replace(@"~\", ""),
                description = infoes.description,
                Number = infoes.Number,
                PlaceName = infoes.PlaceName,

            }).ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "aaplication/pdf", "تفاصيل حسابات القضايا.pdf");



        }

        #endregion

        #region totalEmployeedetaols and print salary report -- secure


        public ActionResult totalEmployeedetaols()
        {


            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();
                if (_allowAll.Staticis == true)
                {
                    var model = new SalaryWithEmpAndMonth();

                    model.salaryX = _db.Salaries.ToList();
                    model.MonthsX = _db.Month.ToList();
                    model.EmpX = _db.Employee.ToList();

                    return View(model);
                }
                return RedirectToAction("HavntAccess", "Employees");

            }
            return RedirectToAction("HavntAccess", "Employees");


        }
        public ActionResult PrintSalaryDetails()
        {
            ReportDocument rd = new ReportDocument();
            var infoes = _db.Info.FirstOrDefault();

            rd.Load(Path.Combine(Server.MapPath("~/Report/PrintSalaryDetails.rpt")));
            rd.SetDataSource(_db.Salaries.Select(p => new
            {
                ID = p.id,
                Employee = _db.Employee.Where(f => f.id.ToString() == p.Employee).Select(f => f.employeeName).FirstOrDefault(),
                empSupplyer = p.empSupplyer,
                salary = p.salary,
                month = _db.Month.Where(f => f.id.ToString() == p.month).Select(f => f.months).FirstOrDefault(),
                Date = p.Date,

                //info data
                img = infoes.Img.Replace(@"~\", ""),
                description = infoes.description,
                Number = infoes.Number,
                PlaceName = infoes.PlaceName,


            }).ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "aaplication/pdf", "تفاصيل حسابات الموظفين.pdf");

        }


        #endregion


        #region expenses --secure


        public ActionResult Expenses()
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Staticis == true)
                {
                    ViewBag.cat = new SelectList(_db.Expensestypes.ToList(), "id", "catogery");
                    ViewBag.office = new SelectList(_db.Offices.ToList(), "id", "OfficeName");


                    var model = new ExpensesAndTypes();
                    model.officeX = _db.Offices.ToList();
                    model.expenTypeX = _db.Expensestypes.ToList();
                    model.expenX = _db.Expenses.ToList();

                    //    var expensesVal= _db.Expenses.Sum(f => f.values);

                    //  ViewBag.SumofExpenses = expensesVal;
                    return View(model);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        [HttpPost]
        public ActionResult AddExpenses(string catogery, string OfficeName, int values, string Comment, ExpensesModel mod)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();

                ViewBag.cat = new SelectList(_db.Expensestypes.ToList(), "id", "catogery");
                ViewBag.office = new SelectList(_db.Offices.ToList(), "id", "OfficeName");


                if (_allowAll.Staticis == true)
                {
                    mod.Expensestype = catogery;
                    mod.office = OfficeName;
                    mod.values = values;
                    mod.Comment = Comment;
                    mod.date = DateTime.Now;
                    _db.Expenses.Add(mod);

                    _db.SaveChanges();
                    var model = new ExpensesAndTypes();
                    model.officeX = _db.Offices.ToList();
                    model.expenTypeX = _db.Expensestypes.ToList();
                    model.expenX = _db.Expenses.ToList();

                    return View("Expenses", model);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        public ActionResult EditExpenses(int? id)
        {
            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();
                ViewBag.cat = new SelectList(_db.Expensestypes.ToList(), "id", "catogery");
                ViewBag.office = new SelectList(_db.Offices.ToList(), "id", "OfficeName");

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ExpensesModel _expen = _db.Expenses.Find(id);


                if (_expen == null)
                {
                    return HttpNotFound();
                }
                else if (_allowAll.Staticis == true)
                {

                    return View(_expen);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditExpenses(int id, ExpensesModel _expen, string OfficeName, string catogery)
        {
            if (Session["empName"] != null)
            {
                ViewBag.cat = new SelectList(_db.Expensestypes.ToList(), "id", "catogery");
                ViewBag.office = new SelectList(_db.Offices.ToList(), "id", "OfficeName");

                if (ModelState.IsValid)
                {
                    _expen.date = DateTime.Now;
                    _expen.office = OfficeName;
                    _expen.Expensestype = catogery;
                    _db.Entry(_expen).State = EntityState.Modified;
                    _db.SaveChanges();

                    TempData["EpenEdit"] = "تم التعديل بنجاح";

                    var model = new ExpensesAndTypes();
                    model.officeX = _db.Offices.ToList();
                    model.expenTypeX = _db.Expensestypes.ToList();
                    model.expenX = _db.Expenses.ToList();

                    return View("Expenses", model);
                }
                return View(_expen);
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        #endregion

        #region PrintExpenses --secure


        public ActionResult PrintExpenses()
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var emp2 = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp2.id).FirstOrDefault();

                if (_allowAll.Staticis == true)
                {

                    ReportDocument rd = new ReportDocument();

                    rd.Load(Path.Combine(Server.MapPath("~/Report/expensesReport.rpt")));
                    var infoes = _db.Info.FirstOrDefault();



                    rd.SetDataSource(_db.Expenses.Select(p => new
                    {
                        id = p.id,
                        Expensestype = _db.Expensestypes.Where(f => f.id.ToString() == p.Expensestype).Select(f => f.catogery).FirstOrDefault(),
                        office = _db.Offices.Where(f => f.Id.ToString() == p.office).Select(f => f.OfficeName).FirstOrDefault(),
                        date = p.date,
                        values = p.values,
                        Comment = p.Comment,


                        //info data
                        img = infoes.Img.Replace(@"~\", ""),
                        description = infoes.description,
                        Number = infoes.Number,
                        PlaceName = infoes.PlaceName,


                    }).ToList());

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "aaplication/pdf", "سجل المصروفات.pdf");
                }

            }
            return RedirectToAction("HavntAccess", "Employees");
        }

        #endregion



    }
}