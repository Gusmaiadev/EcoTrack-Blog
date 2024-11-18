using Microsoft.AspNetCore.Mvc;
using EcoTrack.Blog.Models.DTOs;
using EcoTrack.Blog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EcoTrack.Blog.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IPostService _postService;

        public AuthorController(IAuthorService authorService, IPostService postService)
        {
            _authorService = authorService;
            _postService = postService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthorLoginDto loginDto)
        {
            try
            {
                var token = await _authorService.LoginAsync(loginDto);
                // Armazenar o token em cookie ou session
                HttpContext.Session.SetString("JWTToken", token);
                return RedirectToAction(nameof(Posts));
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View(loginDto);
            }
        }

        [Authorize]
        public async Task<IActionResult> Posts()
        {
            var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var posts = await _postService.GetByAuthorIdAsync(authorId);
            return View(posts);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}