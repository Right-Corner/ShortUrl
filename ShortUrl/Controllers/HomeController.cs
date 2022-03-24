using Microsoft.AspNetCore.Mvc;
using ShortUrl.Models;
using System.Diagnostics;

namespace ShortUrl.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _db = new AppDbContext();
        public const string safeCode = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult RedirectToOriginal(string code)
        {
            var longUrl = _db.UrlDetails.Where(x => x.Code == code).FirstOrDefault().LongUrl;
            if (!string.IsNullOrEmpty(longUrl))
            {
                return Redirect(longUrl);
            }
            return RedirectToAction("Error404");
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult GenerateShortUrl(string longUrl)
        {
            var code = GenerateCode();
            while(_db.UrlDetails.ToList().Where(x => x.Code == code).Any())
            {
                code = GenerateCode();
            }
            var url = new UrlDetail()
            {
                LongUrl = longUrl,
                Code = code
            };
            _db.UrlDetails.Add(url);
            _db.SaveChanges();
            return Json(new { code=code });
        }

        public string GenerateCode()
        {
            return safeCode.Substring(new Random().Next(0, safeCode.Length), new Random().Next(2, 6));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}