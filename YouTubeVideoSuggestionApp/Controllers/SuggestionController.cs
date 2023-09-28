using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using YouTubeVideoSuggestionApp.DTOs;
using YouTubeVideoSuggestionApp.Hubs;
using YouTubeVideoSuggestionApp.IRepositories;

namespace YouTubeVideoSuggestionApp.Controllers
{
    public class SuggestionController : Controller
    {
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly IHubContext<SuggestionHub> _hubContext;
        public SuggestionController(ISuggestionRepository suggestionRepository, IHubContext<SuggestionHub>hubContext)
        {
            _suggestionRepository=suggestionRepository;
            _hubContext=hubContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllSuggestions()
        {
            return Ok(_suggestionRepository.GetAllSuggestions());
        }
        public IActionResult AddSuggestion()
        {
            ViewData["title"] = "Add suggestion";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>AddSuggestion(AddSuggestionDTO addSuggestionDTO)
        {
            if (ModelState.IsValid)
            {
                await _suggestionRepository.AddSuggestion(addSuggestionDTO);
                TempData["success"] = "New suggestion added successfully!";
                await _hubContext.Clients.All.SendAsync("suggestionAdded");
                return RedirectToAction("Index", "Suggestion");
            }
            return View();
        }
        public async Task<IActionResult> UpdateSuggestion(int id)
        {
            ViewData["title"] = "Update suggestion";
            var result = await _suggestionRepository.GetSuggestion(id);
            if (result == null)
            {
                return RedirectToAction("Index", "Suggestion");
            }
            return View("AddSuggestion", result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSuggestion(int id, AddSuggestionDTO addSuggestionDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _suggestionRepository.UpdateSuggestion(id, addSuggestionDTO);
                if (result.Type)
                {
                    TempData["success"] = result.Message;
                    await _hubContext.Clients.All.SendAsync("suggestionUpdated");
                    return RedirectToAction("Index", "Suggestion");
                }
                if (result.Message == null)
                    {
                        return RedirectToAction("Index", "Suggestion");
                    }
                    ViewData["error"] = result.Message;
                ViewData["title"] = "Update suggestion";
            }
            return View("AddSuggestion");
        }

        public async Task<IActionResult>DeleteSuggestion(int id)
        {
            var result = await _suggestionRepository.GetSuggestion(id);
            if (result == null)
            {
                return RedirectToAction("Index", "Suggestion");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>DeleteSuggestion(int id, DeleteSuggestionDTO deleteSuggestionDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _suggestionRepository.DeleteSuggestion(id, deleteSuggestionDTO);
                if (result.Type)
                {
                    TempData["success"] = result.Message;
                    await _hubContext.Clients.All.SendAsync("suggestionDeleted");
                    return RedirectToAction("Index", "Suggestion");
                }
                if (result.Message == null)
                {
                    return RedirectToAction("Index", "Suggestion");
                }
                ViewData["error"] = result.Message;
            }
            return View();
        }
    }
}
