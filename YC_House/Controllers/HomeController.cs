using System.Linq;
using System.Web.Mvc;
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
    }
}