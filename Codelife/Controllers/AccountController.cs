using Codelife.Models;
using Codelife.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Codelife.Controllers
{
    public class AccountController : Controller
    {
        private static string ModuleName = "AccountController";

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(NewAuthor author)
        {
            try
            {
                var author = new 
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "Register", "Error Registering Author" + ex + "\n");
                //ViewBag.ModalLaunch = ShowModal("message-modal", "show", "Error Validating User Details.<br/> Please ensure you are connected to the Enterprise Network");
                return View();
            }
        }

        /*
        public string ShowMessage(string message)
        {
            //return "<script>$(document).ready(function () {   $(\"#" + id + "\").modal(" + options + ");  });</script>";
            return "<script>$(document).ready(function () { alert('" + message + "') });</script>";
        }
        public string ShowModal(string id, string options, string message)
        {
            return "<script>$(document).ready(function () { document.getElementById('message-modal-body').innerHTML = '" + message + "';  jQuery('#" + id + "').modal('" + options + "');  });</script>";
        }

    */


    }

}