using FinalExam_Indigo.DAL;
using FinalExam_Indigo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam_Indigo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Post> posts = await _context.Posts.ToListAsync();
            return View(posts);
        }
    }
}
