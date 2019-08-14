using CrystalDecisions.CrystalReports.Engine;
using LawyerOffice.Models;
using LawyerOffice.Models.DAL;
using LawyerOffice.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LawyerOffice.Controllers
{
    public class CasesController : Controller
    {

        private LawyerContext _db = new LawyerContext();

        readonly private ClientService _service = new ClientService();

        #region index

        // GET: Cases
        public ActionResult Index()
        {
            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();

                var model = new CasesCourtsEmployees();
                model.courtsX = _db.Court.ToList();
                model.clientX = _db.Client.ToList();
                model.EmpX = _db.Employee.Where(id => id.jobs == "1");
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();

                if (_allowAll.AccessAll == true)
                {
                    model.casesX = _db.Cases.ToList();
                    return View(model);

                }
                else if (_allowAll.Cases == true)
                {

                    model.casesX = _db.Cases.Where(p => p.employeeName == ss.id.ToString()).ToList();
                    return View(model);
                }
            }

            return RedirectToAction("HavntAccess", "Employees");
        }
        #endregion


        #region NewGalsa --secure


        [HttpGet]

        public ActionResult NewGalsa(int? id)

        {
            if (Session["empName"] != null)
            {
                ViewBag.Courts = new SelectList(_db.Court.ToList(), "id", "CourtName");
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cases cases = _db.Cases.Find(id);
                string empid = ss.id.ToString();

                if (cases == null)
                {
                    return HttpNotFound();
                }
                else if (cases.employeeName == empid || _allowAll.AccessAll == true)
                {

                    return View();
                }
            }

            return RedirectToAction("HavntAccess", "Employees");


        }

        [HttpPost]

        public ActionResult NewGalsa(elgalasat _elgalsa, int? id, DateTime dateOfGalsa)
        {
            ViewBag.Courts = new SelectList(_db.Court.ToList(), "id", "CourtName");
            var CaseID = _db.Cases.Find(id);
            _elgalsa.NumberOfCase = CaseID.numberOfCase;
            _elgalsa.EmpName = CaseID.employeeName;
            _elgalsa.dateOfGalsa = dateOfGalsa;
            _db.elgalasat.Add(_elgalsa);
            _db.SaveChanges();
            @TempData["Addgalsa"] = "تم اضافه الجلسه فى الاجنده بنجاح";
            return RedirectToAction("index");
        }




        // GET: Case/EditGalsa/5
        public ActionResult EditGalsa(int? id)
        {
            ViewBag.Courts = new SelectList(_db.Court.ToList(), "id", "CourtName");

            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                elgalasat galsa = _db.elgalasat.Find(id);
                int caseid = galsa.NumberOfCase;

                var _case = _db.Cases.Where(d => d.numberOfCase == caseid).FirstOrDefault();
                string empid = ss.id.ToString();
                string cc = _case.employeeName;


                if (galsa == null)
                {
                    return HttpNotFound();
                }
                else if (_case.employeeName == empid || _allowAll.AccessAll == true)
                {

                    return View(galsa);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        // POST: Case/EditGalsa/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGalsa(int id, elgalasat _galasa, DateTime dateOfGalsa)
        {
            ViewBag.Courts = new SelectList(_db.Court.ToList(), "id", "CourtName");


            if (ModelState.IsValid)
            {
                _galasa.dateOfGalsa = dateOfGalsa;
                _db.Entry(_galasa).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_galasa);
        }



        [HttpGet]
        public ActionResult DeleteGalsa(int? id)

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
                elgalasat galsa = _db.elgalasat.Find(id);
                int caseid = galsa.NumberOfCase;

                var _case = _db.Cases.Where(d => d.numberOfCase == caseid).FirstOrDefault();
                string empid = ss.id.ToString();
                string cc = _case.employeeName;

                if (_case == null)
                {
                    return HttpNotFound();
                }
                else if (_case.employeeName == empid || _allowAll.AccessAll == true)
                {
                    return View(galsa);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpPost, ActionName("DeleteGalsa")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            elgalasat galsa = _db.elgalasat.Find(id);
            _db.elgalasat.Remove(galsa);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        #endregion

        #region Edit --secure
        // GET: Case/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["empName"] != null)
            {
                ViewBag.Courts = new SelectList(_db.Court.ToList(), "id", "CourtName");
                ViewBag.CourtType = new SelectList(_db.CourtType.ToList(), "id", "TypeName");

                ViewBag.Employee = new SelectList(_db.Employee.Where(s => s.jobs.ToString() == "1"), "id", "employeeName");

                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cases _case = _db.Cases.Find(id);
                string empid = ss.id.ToString();


                if (_case == null)
                {
                    return HttpNotFound();
                }
                else if (_case.employeeName == empid || _allowAll.AccessAll == true)
                {

                    return View(_case);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        // POST: Case/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cases _cases)
        {
            if (Session["empName"] != null)
            {
                ViewBag.Courts = new SelectList(_db.Court.ToList(), "id", "CourtName");
                ViewBag.CourtType = new SelectList(_db.CourtType.ToList(), "id", "TypeName");

                ViewBag.Employee = new SelectList(_db.Employee.Where(s => s.jobs.ToString() == "1"), "id", "employeeName");

                if (ModelState.IsValid)
                {
                    _cases.dateofElkaeed = DateTime.Now;
                    _db.Entry(_cases).State = EntityState.Modified;
                    _db.SaveChanges();

                    TempData["CaseEdit"] = "تم التعديل بنجاح";
                    return RedirectToAction("Index");
                }
                return View(_cases);
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        #endregion

        #region Delete --secure


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
                Cases _cases = _db.Cases.Find(id);
                string empid = ss.id.ToString();

                if (_cases == null)
                {
                    return HttpNotFound();
                }
                else if (_cases.employeeName == empid || _allowAll.AccessAll == true)
                {
                    return View(_cases);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGalsa(int id)
        {
            Cases _cases = _db.Cases.Find(id);

            var elgalsa = _db.elgalasat.Where(cv => cv.NumberOfCase == _cases.numberOfCase);
            //  var fff = _db.feeses.Where(g=>g.NumberOfCase==_cases.clientID)
            var fessX = _db.feeses.Where(v => v.NumberOfCase == _cases.id);

            if (elgalsa != null)
            {
                foreach (var item in elgalsa)
                {
                    _db.elgalasat.Remove(item);
                }
            }
            if (fessX != null)
            {
                foreach (var item in fessX)
                {
                    _db.feeses.Remove(item);
                }
            }
            _db.Cases.Remove(_cases);
            _db.SaveChanges();
            TempData["CaseDelete"] = "تم الحذف بنجاح";
            return RedirectToAction("Index");
        }
        #endregion

        #region Detalis --secure
        // GET: Case/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();
                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();

                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();


                var model = new CasesAndGalasat();

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cases _case = _db.Cases.Find(id);

                if (_case == null)
                {
                    return HttpNotFound();
                }


                model.CasesX = _db.Cases.Where(s => s.id == id).ToList();
                var CaseID = _db.Cases.Find(id);
                model.galasatX = _db.elgalasat.Where(s => s.NumberOfCase == CaseID.numberOfCase).ToList();
                model.courtsX = _db.Court.ToList();
                string cc = Session["empName"].ToString();
                var empName = _db.Employee.Where(s => s.employeeName == cc).FirstOrDefault();
                if (CaseID.employeeName == empName.id.ToString() || _allowAll.AccessAll == true)
                {
                    return View(model);

                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        #endregion  ☻

        #region  attachments --secure


        public ActionResult ViewAttachment(int? id)

        {
            ViewBag.caseAttID = id;


            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();

                var cases = _db.Cases.Find(id);
                string empid = emp.id.ToString();


                if (cases.employeeName == empid || _allowAll.AccessAll == true)
                {

                    var model = new attachmentAndTypes();
                    model.attTypeX = _db.attachmentType.ToList();
                    model.attX = _db.attachements.Where(d => d.CaseID == id);
                    // ViewBag.CaseID = 6;
                    return View(model);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        [HttpGet]
        public ActionResult UploadAttachment(int? id)

        {

            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();

                var cases = _db.Cases.Where(i => i.id == id).FirstOrDefault();

                string empid = emp.id.ToString();
                if (cases.employeeName == empid || _allowAll.AccessAll == true)
                {

                    ViewBag.AttType = new SelectList(_db.attachmentType.ToList(), "Id", "AttType");

                    return View();
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        [HttpPost]
        public ActionResult UploadAttachment(attachements att, int AttType, int id)

        {
            string name = Session["empName"].ToString();

            var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
            var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();

            if (Session["empName"] != null)
            {
                var cases = _db.Cases.Find(id);
                string empid = emp.id.ToString();
                if (cases.employeeName == empid || _allowAll.AccessAll == true)
                {
                    ViewBag.AttType = new SelectList(_db.attachmentType.ToList(), "Id", "AttType");

                    string filName = Path.GetFileNameWithoutExtension(att.imageFile.FileName);
                    string exten = Path.GetExtension(att.imageFile.FileName);
                    filName = filName + DateTime.Now.ToString("yymmssff") + exten;

                    var caseid = _db.Cases.Where(d => d.id == id).Select(f => f.numberOfCase).FirstOrDefault();

                    string firstPath = _db.Info.Select(f => f.DocumentPath).FirstOrDefault();


                    string path = firstPath + "/" + caseid + "\\";

                    att.ImagePath = path + filName;

                    /// this for upload att in wwwroot image folder
                    filName = Path.Combine(Server.MapPath(path), filName);



                    att.imageFile.SaveAs(filName);

                    att.AttType = AttType;

                    att.CaseID = id;

                    _db.attachements.Add(att);
                    _db.SaveChanges();

                    return RedirectToAction("ViewAttachment/" + id);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");


        }



        #endregion



        #region Money --secure


        public ActionResult MoneyDetalis(int id)
        {
            ViewBag.id = id;

            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();

                var cases = _db.Cases.Find(id);
                string empid = emp.id.ToString();
                if (cases.employeeName == empid || _allowAll.AccessAll == true)
                {


                    var _fe = _db.feeses.Where(s => s.NumberOfCase == id).ToList();
                    if (_fe == null)
                    {
                        return RedirectToAction("HavntAccess", "Employees");

                    }
                    var _fees = (from u in _db.feeses
                                 where u.NumberOfCase == id
                                 select (int?)u.recived).Sum() ?? 0;


                    ViewBag.ReciverSum = _fees;
                    var _Case = _db.Cases.Where(d => d.id == id).FirstOrDefault();
                    if (_Case == null)
                    {
                        HttpNotFound();
                    }
                    ViewBag.Total = _Case.money;
                    ViewBag.remain = ViewBag.Total - ViewBag.ReciverSum;
                    // to pass this value to newmony action
                    TempData["remain"] = ViewBag.remain;

                    ViewBag.NumOfCase = _Case.numberOfCase;
                    return View(_db.feeses.Where(ss => ss.NumberOfCase == id));

                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        [HttpGet]
        public ActionResult NewMoney(int id)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();

                var cases = _db.Cases.Find(id);
                string empid = emp.id.ToString();
                if (cases.employeeName == empid || _allowAll.AccessAll == true)
                {
                    int remain = Convert.ToInt32(TempData["remain"]);
                    if (remain != 0)
                    {
                        return View();

                    }
                    return RedirectToAction("index", "home");

                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        [HttpPost]
        public ActionResult NewMoney(fees _fess, int? id, Logs _log)
        {


            if (Session["empName"] != null)
            {
                var _Case = _db.Cases.Where(s => s.id == id).FirstOrDefault();

                if (_Case == null)
                {
                    return RedirectToAction("HavntAccess", "Employees");

                }
                _fess.NumberOfCase = _Case.id;
                _fess.EmployeeName = Session["empName"].ToString();
                _fess.DateofRevice = DateTime.Now;
                _db.feeses.Add(_fess);

                _log.username = Session["empName"].ToString();
                _log.date = DateTime.Now;
                string ip = Request.UserHostAddress;

                _log.ipaddress = ip;
                _db.Logs.Add(_log);
                _db.SaveChanges();

                return RedirectToAction("MoneyDetalis/" + id);
            }
            return RedirectToAction("HavntAccess", "Employees");
        }



        public ActionResult printreceipt(int? id)
        {
            if (Session["empName"] != null)
            {

                string name = Session["empName"].ToString();

                var emp2 = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp2.id).FirstOrDefault();

                ReportDocument rd = new ReportDocument();
                var infoes = _db.Info.FirstOrDefault();

                rd.Load(Path.Combine(Server.MapPath("~/Report/test2.rpt")));
                var emp = _db.Employee.Where(f => f.id == id).FirstOrDefault();
                rd.SetDataSource(_db.feeses.Where(f => f.NumberOfCase == id).Select(p => new
                {
                    id = p.ID,
                    // Employee = _db.Employee.Where(f => f.id.ToString() == p.Employee).Select(f => f.employeeName).FirstOrDefault(),

                    NumberOfCase = p.NumberOfCase,

                    DateofRevice = p.DateofRevice,
                    clientID_ID = p.clientID,
                    recived = p.recived,
                    notes = p.notes,
                    EmployeeName = p.EmployeeName,

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
                return File(stream, "aaplication/pdf", "اتعاب القضيه.pdf");

            }
            return RedirectToAction("HavntAccess", "Employees");
        }
        #endregion


        #region Print all attachments

        //all att
        public ActionResult printatt(int id)
        {

            string name = Session["empName"].ToString();

            var emp = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
            var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == emp.id).FirstOrDefault();
            var infoes = _db.Info.FirstOrDefault();

            if (Session["empName"] != null)
            {
                var cases = _db.Cases.Find(id);
                string empid = emp.id.ToString();
                if (cases.employeeName == empid || _allowAll.AccessAll == true)
                {


                    ReportDocument rd = new ReportDocument();

                    rd.Load(Path.Combine(Server.MapPath("~/Report/PrintAttachments.rpt")));
                    rd.SetDataSource(_db.attachements.Where(f => f.CaseID == id).Select(p => new
                    {
                        titlte = p.titlte,
                        AttType = _db.attachmentType.Where(f => f.Id == p.AttType).Select(f => f.AttType).FirstOrDefault(),
                        ImagePath = p.ImagePath.Replace(@"~\", ""),

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
                    return File(stream, "aaplication/pdf", " تفاصيل القضيه.pdf");
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        public ActionResult printOneAtt(int id)
        {



            ReportDocument rd = new ReportDocument();
            var infoes = _db.Info.FirstOrDefault();
            rd.Load(Path.Combine(Server.MapPath("~/Report/PrintAttachments.rpt")));
            rd.SetDataSource(_db.attachements.Where(f => f.Atachment_id == id).Select(p => new
            {
                titlte = p.titlte,
                AttType = _db.attachmentType.Where(f => f.Id == p.AttType).Select(f => f.AttType).FirstOrDefault(),
                ImagePath = p.ImagePath.Replace(@"~\", ""),

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
            return File(stream, "aaplication/pdf", "تفاصيل المستند.pdf");

            //    }
            //}
            //return RedirectToAction("HavntAccess", "Employees");

        }


        #endregion

    }

}
