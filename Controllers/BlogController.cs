using CleanArchitecture.Domain.Entities;
using crudCleanAApplication.Services;
using Microsoft.AspNetCore.Mvc;
namespace crudCleanA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await blogService.GetAllAsync();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await blogService.GetByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            var createdBlog = await blogService.CreateAsync(blog);
            return CreatedAtAction(nameof(GetById), new { id = createdBlog.Id }, createdBlog);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Blog updatedBlog)
        {
            int existingBlog = await blogService.UpdateAsync(id, updatedBlog);
            if (existingBlog == 0)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int blog = await blogService.DeleteAsync(id);
            if (blog == 0)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
