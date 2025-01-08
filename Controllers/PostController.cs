using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabloid.Data;
using Tabloid.Models;
using System.Security.Claims;


[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly TabloidDbContext _context;

    public PostController(TabloidDbContext context)
    {
        _context = context;
    }

    // Get all posts by logged-in user
[HttpGet("myposts")]
[Authorize]
public IActionResult GetMyPosts([FromQuery] int userId)
{
    // Validate the userId (optional if you trust the frontend)
    var user = _context.UserProfiles.FirstOrDefault(u => u.Id == userId);
    if (user == null)
    {
        return NotFound("User not found");
    }

    // Fetch posts authored by the user
    var posts = _context.Posts
        .Where(p => p.AuthorId == userId)
        .OrderByDescending(p => p.CreateDateTime)
        .Select(p => new
        {
            p.Id,
            p.Title,
            Category = p.Category != null ? p.Category.Name : "Uncategorized",
            p.CreateDateTime
        })
        .ToList();

    return Ok(posts);
}




    // Get details of a single post
    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetPostDetails(int id)
    {
        var post = _context.Posts
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.Title,
                p.Content,
                p.HeaderImageUrl,
                p.CreateDateTime,
                AuthorName = $"{p.Author.FirstName} {p.Author.LastName}"
            })
            .FirstOrDefault();

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    // Create a new post
[HttpPost]
[Authorize]
public IActionResult CreatePost([FromBody] Post newPost)
{
    // Validate the AuthorId in the incoming request
    var user = _context.UserProfiles.FirstOrDefault(up => up.Id == newPost.AuthorId);
    if (user == null) return Unauthorized("Invalid AuthorId");

    // Explicitly assign all properties to the newPost object
    var post = new Post
    {
        Title = newPost.Title,
        Content = newPost.Content,
        CategoryId = newPost.CategoryId,
        HeaderImageUrl = newPost.HeaderImageUrl,
        PublishDateTime = newPost.PublishDateTime,
        AuthorId = newPost.AuthorId,
        CreateDateTime = DateTime.Now, // Assign current creation time
    };

    // Add the post to the database
    _context.Posts.Add(post);
    _context.SaveChanges();

    // Return the created post details
    return CreatedAtAction(nameof(GetPostDetails), new { id = post.Id }, post);
}


    // Delete a post
  [HttpDelete("{id}")]
[Authorize]
public IActionResult DeletePost(int id, [FromQuery] int userId)
{
    var post = _context.Posts.FirstOrDefault(p => p.Id == id);
    if (post == null) return NotFound();

    // Validate the userId against the post's AuthorId
    if (post.AuthorId != userId) return Forbid();

    _context.Posts.Remove(post);
    _context.SaveChanges();
    return NoContent();
}


        // Update a post
 [HttpPut("{id}")]
// [Authorize]
public IActionResult UpdatePost(int id, [FromBody] Post updatedPost)
{

    // Log the incoming `updatedPost` values
    Console.WriteLine($"  Title: {updatedPost.Title}");
    Console.WriteLine($"  PublishDateTime: {updatedPost.PublishDateTime}");



    var post = _context.Posts.FirstOrDefault(p => p.Id == id);
    if (post == null) return NotFound();

    Console.WriteLine($"  Title: {post.Title}");
  

    // Validate the userId against the post's AuthorId
    // if (post.AuthorId != updatedPost.AuthorId)
    // {
    //     Console.WriteLine($"AuthorId mismatch:");
    //     Console.WriteLine($"  Post.AuthorId: {post.AuthorId}");
    //     Console.WriteLine($"  UpdatedPost.AuthorId: {updatedPost.AuthorId}");
    //     return Forbid();
    // }

    // Update the post
    post.Title = updatedPost.Title;
    post.Content = updatedPost.Content;
    post.CategoryId = updatedPost.CategoryId;
    post.HeaderImageUrl = updatedPost.HeaderImageUrl;
    post.PublishDateTime = updatedPost.PublishDateTime;

      // Log the updated post values
    Console.WriteLine($"Updated Post before saving:");
    Console.WriteLine($"  Title: {post.Title}");


    _context.SaveChanges();
    return NoContent();
}

[HttpGet]
[Authorize]
public IActionResult GetAllPosts()
{
    var posts = _context.Posts
        .OrderByDescending(p => p.CreateDateTime)
        .Select(p => new
        {
            p.Id,
            p.Title,
            p.Content,
            Category = p.Category != null ? p.Category.Name : "Uncategorized",
            AuthorName = $"{p.Author.FirstName} {p.Author.LastName}",
            p.CreateDateTime
        })
        .ToList();

    return Ok(posts);
}



}
