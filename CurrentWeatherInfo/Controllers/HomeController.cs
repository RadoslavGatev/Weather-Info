using System;
using System.Linq;
using System.Web.Mvc;

namespace CurrentWeatherInfo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Home/SearchWeatherByCity
        public ActionResult SearchWeatherByCity()
        {
            return View();
        }

    }
}
