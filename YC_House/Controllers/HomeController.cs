using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using YC_House.Models;
using YC_House.ViewModels;

namespace YC_House.Controllers
{
    public class HomeController : Controller
    {
        DBManager dbm = new DBManager();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read(StudentBasicInfoViewModel post)
        {
            var result = dbm.QueryStudentInfoTable()
                .Where(v => string.IsNullOrEmpty(post.StudentId) ? true : (v.StudentId.Contains(post.StudentId)))
                .Where(v => string.IsNullOrEmpty(post.Name) ? true : (v.Name.Contains(post.Name)));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RowRead(string keyValue)
        {
            var result = dbm.QueryStudentInfoTable().Where(v => v.StudentId == keyValue).FirstOrDefault();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(StudentBasicInfoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                dbm.CreateStudentBasicInfo(vm, "add");
                dbm.CreateStudentGrades(vm, "add");
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var errMsg = String.Join(" ", ModelState.Values.SelectMany(x => x.Errors.Select(f => f.ErrorMessage)).ToList());
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(errMsg);
            }
        }

        public JsonResult Update(StudentBasicInfoViewModel vm)
        {
            dbm.CreateStudentBasicInfo(vm, "update");
            dbm.CreateStudentGrades(vm, "update");

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Destroy(string studentId)
        {
            dbm.DeleteStudentBasicInfo(studentId);
            dbm.DeleteStudentGrades(studentId);

            return Json(studentId, JsonRequestBehavior.AllowGet);
        }
    }
}