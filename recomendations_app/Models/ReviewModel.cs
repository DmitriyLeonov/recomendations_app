using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using NpgsqlTypes;
using Recomendations_app.CustomValidationAttributes;

namespace Recomendations_app.Models
{
    public class ReviewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(255, ErrorMessage = "Maximum tile length is 255")]
        [MinLength(2, ErrorMessage = "Minimum title length is 2")]
        public string Title { get; set; } = string.Empty;
        [Required]
        public Category ReviewCategory { get; set; }
        [Required]
        [Range(0, 10)]
        public int AuthorGrade { get; set; }
        [Required(ErrorMessage = "Review body is required")]
        [MaxLength(5000, ErrorMessage = "Review body is too long")]
        [MinLength(1, ErrorMessage = "Review body is too short")]
        public string ReviewBody { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfCreationInUTC { get; set; }
        [Required]
        //public long SubjectId { get; set; }
        //public SubjectModel Subject { get; set; } = new();
        [MaxFileSize(1 * 3840 * 3840)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public string? ImageLink { get; set; }
        public string? ImageStorageName { get; set; }
        [Required]
        public string AuthorName { get; set; } = string.Empty;
        //public UserModel? Author { get; set; }
        [NotMapped]
        public int? CommentsAmount { get; set; }

        [MaxLength(15, ErrorMessage = "Max amount of tags is exceeded")]
        public List<TagModel>? Tags { get; set; } = new();
        public List<LikeModel> Likes { get; set; } = new();

        public NpgsqlTsVector SearchVector { get; set; }
    }

    public enum Category
    {
        Other, Book, Movie, Music, Device, Person, Game
    }

    public enum SortBy
    {
        Newest, Popular
    }
}
