using Microsoft.AspNetCore.Mvc;

namespace YouTubeVideoSuggestionApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error404(int code)
        {
            return View();
        }
        public IActionResult Error500()
        {
            return View();
        }
    }
}
