using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tabloid.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string HeaderImageUrl { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreateDateTime { get; set; }

        public DateTime? PublishDateTime { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public UserProfile Author { get; set; }
    }
}
