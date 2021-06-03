using System;
using Microsoft.AspNetCore.Mvc;

namespace Unite.Specimens.Feed.Web.Controllers
{
    [Route("api/")]
    public class DefaultController : Controller
    {
        public IActionResult Get()
        {
            var date = DateTime.UtcNow;

            return Json(date);
        }
    }
}
