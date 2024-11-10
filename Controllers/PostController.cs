using Microsoft.AspNetCore.Mvc;
using RestApiExample.Services;
using System.Threading.Tasks;

namespace RestApiExample.Controllers
{
    public class PostController : Controller
    {
        private readonly ApiService _apiService;

        public PostController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
             var posts = await _apiService.GetPostsAsync();
            return View(posts); 
           
        }
    }
}
