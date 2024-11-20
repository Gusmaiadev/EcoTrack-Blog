using EcoTrack.Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IPostService _postService;

    public HomeController(ICategoryService categoryService, IPostService postService)
    {
        _categoryService = categoryService;
        _postService = postService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();
        return View(categories);
    }

    public async Task<IActionResult> CategoryPosts(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null)
            return NotFound();

        var posts = await _postService.GetByCategoryIdAsync(id);
        ViewBag.CategoryName = category.Name;
        return View(posts);
    }
}