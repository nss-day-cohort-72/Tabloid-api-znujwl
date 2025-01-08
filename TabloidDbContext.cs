using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tabloid.Models;
using Microsoft.AspNetCore.Identity;

namespace Tabloid.Data;
public class TabloidDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;

    public DbSet<UserProfile> UserProfiles { get; set; }

    public DbSet<Post> Posts { get; set; }
public DbSet<Category> Categories { get; set; }


    public TabloidDbContext(DbContextOptions<TabloidDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

         // Define the one-to-one relationship between UserProfile and IdentityUser
    modelBuilder.Entity<UserProfile>()
        .HasOne(up => up.IdentityUser) // Navigation property in UserProfile
        .WithOne()                     // IdentityUser has no back-reference to UserProfile
        .HasForeignKey<UserProfile>(up => up.IdentityUserId); // Foreign key in UserProfile

        modelBuilder.Entity<Post>()
    .HasOne(p => p.Author)
    .WithMany() // Assuming a one-to-many relationship
    .HasForeignKey(p => p.AuthorId)
    .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            Name = "Admin",
            NormalizedName = "admin"
        });

   // Seed categories
    modelBuilder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "Technology" },
        new Category { Id = 2, Name = "Science" },
        new Category { Id = 3, Name = "Lifestyle" },
        new Category { Id = 4, Name = "Politics" },
        new Category { Id = 5, Name = "Health" },
        new Category { Id = 6, Name = "Entertainment" }
    );

    // Seed posts
    modelBuilder.Entity<Post>().HasData(
        new Post
        {
            Id = 1,
            Title = "Exploring the Future of AI",
            Content = "Artificial Intelligence is transforming the world...",
            HeaderImageUrl = "https://example.com/ai.jpg",
            CreateDateTime = new DateTime(2023, 7, 15),
            PublishDateTime = new DateTime(2023, 7, 20),
            CategoryId = 1,
            AuthorId = 1
        },
        new Post
        {
            Id = 2,
            Title = "The Wonders of Space Exploration",
            Content = "Space exploration has always fascinated humankind...",
            HeaderImageUrl = "https://example.com/space.jpg",
            CreateDateTime = new DateTime(2023, 6, 10),
            PublishDateTime = new DateTime(2023, 6, 15),
            CategoryId = 2,
            AuthorId = 2
        },
        new Post
        {
            Id = 3,
            Title = "10 Tips for a Healthy Lifestyle",
            Content = "Maintaining a healthy lifestyle is easier than you think...",
            HeaderImageUrl = "https://example.com/lifestyle.jpg",
            CreateDateTime = new DateTime(2023, 5, 20),
            PublishDateTime = new DateTime(2023, 5, 25),
            CategoryId = 3,
            AuthorId = 3
        },
        new Post
        {
            Id = 4,
            Title = "Politics in the Modern World",
            Content = "Understanding the ever-changing landscape of global politics.",
            HeaderImageUrl = "https://example.com/politics.jpg",
            CreateDateTime = new DateTime(2023, 4, 12),
            PublishDateTime = new DateTime(2023, 4, 15),
            CategoryId = 4,
            AuthorId = 1
        },
        new Post
        {
            Id = 5,
            Title = "Entertainment Trends in 2023",
            Content = "Explore the latest movies, music, and entertainment trends.",
            HeaderImageUrl = "https://example.com/entertainment.jpg",
            CreateDateTime = new DateTime(2023, 3, 20),
            PublishDateTime = new DateTime(2023, 3, 25),
            CategoryId = 6,
            AuthorId = 1
        },
          new Post
    {
        Id = 6,
        Title = "Advancements in Quantum Computing",
        Content = "Quantum computing is set to revolutionize technology.",
        HeaderImageUrl = "https://example.com/quantum.jpg",
        CreateDateTime = new DateTime(2023, 8, 15),
        PublishDateTime = new DateTime(2023, 8, 20),
        CategoryId = 1,
        AuthorId = 1
    },
    new Post
    {
        Id = 7,
        Title = "The Rise of Renewable Energy",
        Content = "Renewable energy sources are becoming mainstream.",
        HeaderImageUrl = "https://example.com/renewable.jpg",
        CreateDateTime = new DateTime(2023, 9, 5),
        PublishDateTime = new DateTime(2023, 9, 10),
        CategoryId = 2,
        AuthorId = 1
    });


        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser[]
        {
            new IdentityUser
            {
                Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                UserName = "Administrator",
                Email = "admina@strator.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df",
                UserName = "JohnDoe",
                Email = "john@doe.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "a7d21fac-3b21-454a-a747-075f072d0cf3",
                UserName = "JaneSmith",
                Email = "jane@smith.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "c806cfae-bda9-47c5-8473-dd52fd056a9b",
                UserName = "AliceJohnson",
                Email = "alice@johnson.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "9ce89d88-75da-4a80-9b0d-3fe58582b8e2",
                UserName = "BobWilliams",
                Email = "bob@williams.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "d224a03d-bf0c-4a05-b728-e3521e45d74d",
                UserName = "EveDavis",
                Email = "Eve@Davis.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },

        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>[]
        {
            new IdentityUserRole<string>
            {
                RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
            },
            new IdentityUserRole<string>
            {
                RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                UserId = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df"
            },

        });
        modelBuilder.Entity<UserProfile>().HasData(new UserProfile[]
        {
            new UserProfile
            {
                Id = 1,
                IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                FirstName = "Admina",
                LastName = "Strator",
                ImageLocation = "https://robohash.org/numquamutut.png?size=150x150&set=set1",
                CreateDateTime = new DateTime(2022, 1, 25)
            },
             new UserProfile
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe",
                CreateDateTime = new DateTime(2023, 2, 2),
                ImageLocation = "https://robohash.org/nisiautemet.png?size=150x150&set=set1",
                IdentityUserId = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df",
            },
            new UserProfile
            {
                Id = 3,
                FirstName = "Jane",
                LastName = "Smith",
                CreateDateTime = new DateTime(2022, 3, 15),
                ImageLocation = "https://robohash.org/molestiaemagnamet.png?size=150x150&set=set1",
                IdentityUserId = "a7d21fac-3b21-454a-a747-075f072d0cf3",
            },
            new UserProfile
            {
                Id = 4,
                FirstName = "Alice",
                LastName = "Johnson",
                CreateDateTime = new DateTime(2023, 6, 10),
                ImageLocation = "https://robohash.org/deseruntutipsum.png?size=150x150&set=set1",
                IdentityUserId = "c806cfae-bda9-47c5-8473-dd52fd056a9b",
            },
            new UserProfile
            {
                Id = 5,
                FirstName = "Bob",
                LastName = "Williams",
                CreateDateTime = new DateTime(2023, 5, 15),
                ImageLocation = "https://robohash.org/quiundedignissimos.png?size=150x150&set=set1",
                IdentityUserId = "9ce89d88-75da-4a80-9b0d-3fe58582b8e2",
            },
            new UserProfile
            {
                Id = 6,
                FirstName = "Eve",
                LastName = "Davis",
                CreateDateTime = new DateTime(2022, 10, 18),
                ImageLocation = "https://robohash.org/hicnihilipsa.png?size=150x150&set=set1",
                IdentityUserId = "d224a03d-bf0c-4a05-b728-e3521e45d74d",
            }
        });
    }
}