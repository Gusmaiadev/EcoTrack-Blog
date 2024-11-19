using Microsoft.AspNetCore.Mvc;
using EcoTrack.Blog.Models.DTOs;
using EcoTrack.Blog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using System.Threading.Tasks;

namespace EcoTrack.Blog.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;

        public AuthorController(
            IAuthorService authorService,
            IPostService postService,
            ICategoryService categoryService)
        {
            _authorService = authorService;
            _postService = postService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Posts));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Posts));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AuthorRegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }

            try
            {
                await _authorService.RegisterAsync(registerDto);
                TempData["SuccessMessage"] = "Cadastro realizado com sucesso! Faça login para continuar.";
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View(registerDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthorLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            try
            {
                var token = await _authorService.LoginAsync(loginDto);

                // Armazenar o token em cookie
                Response.Cookies.Append("JWTToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(60) // Mesmo tempo do token
                });

                // Armazenar o token na sessão também (redundância)
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
            try
            {
                var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var posts = await _postService.GetByAuthorIdAsync(authorId);
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(posts);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditPost(int id)
        {
            try
            {
                var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var post = await _postService.GetByIdAsync(id);

                if (post.AuthorId != authorId)
                {
                    return Forbid();
                }

                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(post);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Posts));
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditPost(PostUpdateDto postDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(postDto);
            }

            try
            {
                var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _postService.UpdateAsync(authorId, postDto);
                TempData["SuccessMessage"] = "Post atualizado com sucesso!";
                return RedirectToAction(nameof(Posts));
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(postDto);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostCreateDto postDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(postDto);
            }

            try
            {
                var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _postService.CreateAsync(authorId, postDto);
                TempData["SuccessMessage"] = "Post criado com sucesso!";
                return RedirectToAction(nameof(Posts));
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(postDto);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _postService.DeleteAsync(authorId, id);
                TempData["SuccessMessage"] = "Post excluído com sucesso!";
                return RedirectToAction(nameof(Posts));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Posts));
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Remover o cookie de autenticação
            Response.Cookies.Delete("JWTToken");

            // Limpar a sessão
            HttpContext.Session.Clear();

            // Fazer logout da autenticação
            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var author = await _authorService.GetByIdAsync(authorId);
                return View(author);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Posts));
            }
        }
    }
}