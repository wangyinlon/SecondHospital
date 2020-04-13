using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppReadCard.Utils;

namespace WebAppReadCard.Controllers
{
    public class CardController : Controller
    {
        // GET: Card
        public ActionResult Index(byte b)
        {
            
            return View();
        }
    }
}