using FinalExam_Indigo.Areas.Admin.ViewModels;
using FinalExam_Indigo.DAL;
using FinalExam_Indigo.Models;
using FinalExam_Indigo.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam_Indigo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public PostController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Post> Posts = await _context.Posts.ToListAsync();
            return View();
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostVM postVM)
        {
            if (!ModelState.IsValid) return View();
            bool result = await _context.Posts.AnyAsync(p=>p.Title.ToLower().Trim()==postVM.Title.ToLower().Trim());
            if (result)
            {
                ModelState.AddModelError("Title", "Bu adli post artiq movcuddur");
                return View();
            }
            if (!postVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File tipi uygun deyil");
                return View();
            }
            if (!postVM.Photo.ValidateSize(2 * 1024))
            {
                ModelState.AddModelError("Photo", "File olcusu uygun deyil");
                return View();
            }

            string fileName = await postVM.Photo.CreateFile(_env.WebRootPath, "images");

            Post post = new Post
            {
                ImageUrl = fileName,
                Title = postVM.Title,
                Description = postVM.Description
            };
            
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            if(id<=0) return BadRequest();

            Post existed = await _context.Posts.FirstOrDefaultAsync(p=>p.Id==id);
            if (existed==null) return NotFound();

            UpdatePostVM postVm = new UpdatePostVM
            {
                ImageUrl = existed.ImageUrl,
                Title = existed.Title,
                Description = existed.Description,
            };

            return View(postVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdatePostVM postVM)
        {
            if(!ModelState.IsValid) return BadRequest();

            Post existed = await _context.Posts.FirstOrDefaultAsync(p=>p.Id==id);
            if (existed==null) return NotFound();

            bool result = await _context.Posts.AnyAsync(p => p.Title.ToLower().Trim() == postVM.Title.ToLower().Trim());
            if (result)
            {
                ModelState.AddModelError("Title", "Bu adli post artiq movcuddur");
                return View();
            }

            if(postVM.Photo!=null)
            {
                if (!postVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipi uygun deyil");
                    return View();
                }
                if (!postVM.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File olcusu uygun deyil");
                    return View();
                }

                string newImage = await postVM.Photo.CreateFile(_env.WebRootPath, "images");
                existed.ImageUrl.DeleteFile(_env.WebRootPath, "images");
                existed.ImageUrl = newImage;
            }

            existed.Title = postVM.Title;
            existed.Description = postVM.Description;
            existed.Photo = postVM.Photo;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details()
        {
            List<Post> posts = await _context.Posts.ToListAsync();
            return View(posts);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Post existed = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (existed == null) return NotFound();


            _context.Remove(existed);

            existed.ImageUrl.DeleteFile(_env.WebRootPath, "images");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
