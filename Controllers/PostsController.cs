using EcoTrack.Blog.Models.DTOs;
using EcoTrack.Blog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcoTrack.Blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetById(int id)
        {
            try
            {
                var post = await _postService.GetByIdAsync(id);
                return Ok(post);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetByCategory(int categoryId)
        {
            var posts = await _postService.GetByCategoryIdAsync(categoryId);
            return Ok(posts);
        }

        [Authorize]
        [HttpGet("author/{authorId}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetByAuthor(int authorId)
        {
            var posts = await _postService.GetByAuthorIdAsync(authorId);
            return Ok(posts);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PostDto>> Create(PostCreateDto createDto)
        {
            try
            {
                var authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var post = await _postService.CreateAsync(authorId, createDto);
                return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<PostDto>> Update(int id, PostUpdateDto updateDto)
        {
            try
            {
                var authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                updateDto.Id = id;
                var post = await _postService.UpdateAsync(authorId, updateDto);
                return Ok(post);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _postService.DeleteAsync(authorId, id);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}