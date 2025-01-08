using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabloid.Data;
using Tabloid.Models;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly TabloidDbContext _context;

    public CategoryController(TabloidDbContext context)
    {
        _context = context;
    }

    // Get all categories
    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _context.Categories
            .OrderBy(c => c.Name)
            .ToList();

        return Ok(categories);
    }

    // Create a new category
    [HttpPost]
    public IActionResult CreateCategory(Category newCategory)
    {
        _context.Categories.Add(newCategory);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCategories), new { id = newCategory.Id }, newCategory);
    }

    // Update a category
    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, Category updatedCategory)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null) return NotFound();

        category.Name = updatedCategory.Name;
        _context.SaveChanges();
        return NoContent();
    }

    // Delete a category
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null) return NotFound();

        // Set associated posts' category to null or handle re-assignment logic
        var posts = _context.Posts.Where(p => p.CategoryId == id).ToList();
        foreach (var post in posts)
        {
            post.CategoryId = null;
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();

        return NoContent();
    }
}
