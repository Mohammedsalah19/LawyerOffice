using LawyerOffice.Models;
using LawyerOffice.Models.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LawyerOffice.Controllers
{
    public class MangeController : Controller
    {

        private LawyerContext _db = new LawyerContext();

        #region index --secure

        public ActionResult Index()
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    return View();
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        #endregion

        #region attachments --secure


        public ActionResult attachments()
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    return View(_db.attachmentType.ToList());
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        [HttpPost]
        public ActionResult NewattachmentsType(string attType, attachmentType att)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    if (attType != null || attType != " ")
                    {
                        att.AttType = attType;
                        _db.attachmentType.Add(att);
                        _db.SaveChanges();
                        return View("attachments", _db.attachmentType.ToList());
                    }
                    return View("attachments", _db.attachmentType.ToList());
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpGet]
        public ActionResult Delete(int id)

        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    attachmentType att = _db.attachmentType.Find(id);
                    if (att == null)
                    {
                        return HttpNotFound();
                    }
                    return View(att);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    attachmentType att = _db.attachmentType.Find(id);
                    _db.attachmentType.Remove(att);
                    _db.SaveChanges();
                    TempData["AttachmentDelete"] = "تم حذف نوع المستند";
                    return RedirectToAction("attachments");
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        #endregion

        #region Offices --secure


        public ActionResult OfficesIndexs()
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    return View(_db.Offices.ToList());
                }

            }
            return RedirectToAction("HavntAccess", "Employees");
        }

        [HttpPost]
        public ActionResult AddOffice(Offices offices, string _officeName, string _description)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    offices.OfficeName = _officeName;
                    offices.Description = _description;
                    _db.Offices.Add(offices);
                    _db.SaveChanges();
                    return RedirectToAction("OfficesIndexs");
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpGet]
        public ActionResult DeleteOffice(int id)

        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Offices _office = _db.Offices.Find(id);
                    if (_office == null)
                    {
                        return HttpNotFound();
                    }
                    return View(_office);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpPost, ActionName("DeleteOffice")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiremDelete(int id)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    Offices _office = _db.Offices.Find(id);
                    _db.Offices.Remove(_office);
                    _db.SaveChanges();
                    TempData["OfficeDelete"] = "تم حذف المكتب ";
                    return RedirectToAction("OfficesIndexs");
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        #endregion


        #region jobs Type --secure

        public ActionResult JobsIndexs()
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    return View(_db.Jobs.ToList());
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpPost]
        public ActionResult AddJob(Jobs job, string _job)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    job.jobs = _job;
                    _db.Jobs.Add(job);
                    _db.SaveChanges();
                    return RedirectToAction("JobsIndexs");
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpGet]
        public ActionResult Deletejob(int id)

        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Jobs _job = _db.Jobs.Find(id);
                    if (_job == null)
                    {
                        return HttpNotFound();
                    }
                    return View(_job);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpPost, ActionName("Deletejob")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiremDeleteJob(int id)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    Jobs jobs = _db.Jobs.Find(id);
                    _db.Jobs.Remove(jobs);
                    _db.SaveChanges();
                    TempData["JobsDelete"] = "تم حذف الوظيفه بنجاح";
                    return RedirectToAction("JobsIndexs");
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }

        #endregion

        #region Court Type --secure


        public ActionResult CourtsIndexs()
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    return View(_db.CourtType.ToList());
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }



        [HttpPost]
        public ActionResult AddCourtType(CourtType courtType, string _type)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    courtType.TypeName = _type;
                    _db.CourtType.Add(courtType);
                    _db.SaveChanges();
                    return RedirectToAction("CourtsIndexs");
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpGet]
        public ActionResult DeleteCourtType(int id)

        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    CourtType _courtType = _db.CourtType.Find(id);
                    if (_courtType == null)
                    {
                        return HttpNotFound();
                    }
                    return View(_courtType);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpPost, ActionName("DeleteCourtType")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiremDeleteCourt(int id)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    CourtType _courtType = _db.CourtType.Find(id);
                    _db.CourtType.Remove(_courtType);
                    _db.SaveChanges();
                    TempData["CourtDelete"] = "تم حذف نوع المحكمه بنجاح";
                    return RedirectToAction("CourtsIndexs");
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        #endregion

        #region catoger of expenses --secure


        public ActionResult CatogeryIndexs()
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    return View(_db.Expensestypes.ToList());
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpPost]
        public ActionResult AddCate(Expensestypes ExpType, string _type)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    ExpType.catogery = _type;
                    _db.Expensestypes.Add(ExpType);
                    _db.SaveChanges();
                    return RedirectToAction("CatogeryIndexs");
                }

            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpGet]
        public ActionResult DeleteCate(int id)

        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Expensestypes _expeType = _db.Expensestypes.Find(id);
                    if (_expeType == null)
                    {
                        return HttpNotFound();
                    }
                    return View(_expeType);
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }
        [HttpPost, ActionName("DeleteCate")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiremDeleteCate(int id)
        {
            if (Session["empName"] != null)
            {
                string name = Session["empName"].ToString();

                var ss = _db.Employee.Where(d => d.employeeName == name).FirstOrDefault();
                var _allowAll = _db.HowCanAcess.Where(s => s.employeeID == ss.id).FirstOrDefault();


                if (_allowAll.Setting == true)
                {
                    Expensestypes _expeType = _db.Expensestypes.Find(id);
                    _db.Expensestypes.Remove(_expeType);
                    _db.SaveChanges();
                    TempData["CateDelete"] = "تم حذف الصنف بنجاح";
                    return RedirectToAction("CatogeryIndexs");
                }
            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        #endregion


        #region info --secure

        [HttpGet]
        public ActionResult info()
        {
            if (Session["empName"].ToString() == "uptop")
            {
                return View(_db.Info.ToList());
            }

            return RedirectToAction("HavntAccess", "Employees");
        }

        [HttpPost]
        public ActionResult info(Info _info)
        {

            if (Session["empName"].ToString() == "uptop")
            {


                // create new folder

                var path = _db.Info.Select(f => f.PhyscialPath).FirstOrDefault();
                string foldername = "معلومات المكان";
                var folder = Path.Combine(path, foldername);
                Directory.CreateDirectory(folder);



                string filNameS = Path.GetFileNameWithoutExtension(_info.ImgFile.FileName);

                string exten = Path.GetExtension(_info.ImgFile.FileName);
                filNameS = filNameS + DateTime.Now.ToString("yymmssff") + exten;





                string paths = _info.DocumentPath + "\\" + foldername + "\\";
                _info.Img = paths + filNameS;
                filNameS = Path.Combine(Server.MapPath(paths), filNameS);

                _info.ImgFile.SaveAs(filNameS);
                _db.Info.Add(_info);
                _db.SaveChanges();
                return RedirectToAction("info", "Mange", _db.Info.ToList());

            }

            return RedirectToAction("HavntAccess", "Employees");

        }




        [HttpGet]
        public ActionResult DeleteInfo(int id)

        {
            if (Session["empName"].ToString() == "uptop")
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Info info = _db.Info.Find(id);
                if (info == null)
                {
                    return HttpNotFound();
                }
                return View(info);

            }
            return RedirectToAction("HavntAccess", "Employees");

        }


        [HttpPost, ActionName("DeleteInfo")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiremDeleteInfo(int id)
        {
            if (Session["empName"].ToString() == "uptop")
            {

                Info info = _db.Info.Find(id);
                _db.Info.Remove(info);
                _db.SaveChanges();
                TempData["InfoDelete"] = "تم حذف المعلومات بنجاح";
                return RedirectToAction("info");
            }

            return RedirectToAction("HavntAccess", "Employees");

        }



        #endregion

        #region delete all 

        [HttpGet]
        public ActionResult delletall()
        {
            if (Session["empName"].ToString() == "uptop")
            {


                ViewBag.tables = new SelectList(_db.Tables.ToList(), "id", "name");

                ViewBag.tables = _db.Tables.Select(f => f.values).ToList();
                var all = _db.Tables.ToList();

                return View(all);


            }
            return RedirectToAction("HavntAccess", "Employees");
        }

        public ActionResult DeleteAllOffices()
        {
            if (Session["empName"].ToString() == "uptop")
            {
                var all = _db.Offices.ToList();
                foreach (var item in all)
                {
                    _db.Offices.Remove(item);
                    _db.SaveChanges();
                }
                return View();
            }

            return RedirectToAction("HavntAccess", "Employees");

        }
        public ActionResult DeleteAllSalaries()
        {
            if (Session["empName"].ToString() == "uptop")
            {

                var all = _db.Salaries.ToList();
                foreach (var item in all)
                {
                    _db.Salaries.Remove(item);
                    _db.SaveChanges();


                }
                return View();
            }

            return RedirectToAction("HavntAccess", "Employees");

        }
        public ActionResult DeleteAllfees()
        {
            if (Session["empName"].ToString() == "uptop")
            {


                var all = _db.feeses.ToList();
                foreach (var item in all)
                {
                    _db.feeses.Remove(item);
                    _db.SaveChanges();


                }
                return View();
            }

            return RedirectToAction("HavntAccess", "Employees");
        }
        public ActionResult DeleteAllJobs()
        {
            if (Session["empName"].ToString() == "uptop")
            {

                var all = _db.Jobs.ToList();
                foreach (var item in all)
                {
                    _db.Jobs.Remove(item);
                    _db.SaveChanges();


                }

                return View();
            }
            return RedirectToAction("HavntAccess", "Employees");
        }
        public ActionResult DeleteAllInfoes()
        {
            if (Session["empName"].ToString() == "uptop")
            {

                var all = _db.Info.ToList();
                foreach (var item in all)
                {
                    _db.Info.Remove(item);
                    _db.SaveChanges();


                }
                return View();
            }

            return RedirectToAction("HavntAccess", "Employees");

        }
        public ActionResult DeleteAllCourts()
        {
            if (Session["empName"].ToString() == "uptop")
            {

                var all = _db.Court.ToList();
                foreach (var item in all)
                {
                    _db.Court.Remove(item);
                    _db.SaveChanges();


                }
                return View();
            }

            return RedirectToAction("HavntAccess", "Employees");

        }


        public ActionResult DeleteAllClients()
        {
            if (Session["empName"].ToString() == "uptop")
            {

                var all = _db.Client.ToList();
                foreach (var item in all)
                {
                    _db.Client.Remove(item);
                    _db.SaveChanges();


                }
                return View();
            }

            return RedirectToAction("HavntAccess", "Employees");

        }
        #endregion

        [HttpGet]
        public ActionResult UptoP()
        {
            if (Session["empName"].ToString() == "uptop")
            {
                return View();
            }
            return RedirectToAction("Login", "Employees");

        }
    }


}
