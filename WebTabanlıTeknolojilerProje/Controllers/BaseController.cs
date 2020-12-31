using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTabanlıTeknolojilerProje.DB;

namespace WebTabanlıTeknolojilerProje.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected DB.PAUCENGEntities context { get; private set; }
         
        public BaseController()
        {
            context = new DB.PAUCENGEntities();
            ViewBag.MenuCategories = context.Categories.Where(x => x.Parent_Id == null).ToList();
            
        }

        protected DB.Members CurrentUser()
        {
            if (Session["LogonUser"] == null )
            {
                return null;
            }
            return (DB.Members)Session["LogonUser"];
        }

        protected int CurrentUserId()
        {
            if (Session["LogonUser"] == null)
            {
                return 0;
            }
            return ((DB.Members)Session["LogonUser"]).Id;
        }

        protected bool IsLogon()
        {
            if (Session["LogonUser"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected List<Categories> GetChildCategories(Categories cat)
        {
            var result = new List<Categories>();
            result.AddRange(cat.Categories1);
            foreach(var item in cat.Categories1)
            {
                var list = GetChildCategories(item);
                result.AddRange(list);
            }
            return result;
        }
    }
}