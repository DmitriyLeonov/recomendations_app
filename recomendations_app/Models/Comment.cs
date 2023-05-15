using Microsoft.VisualBasic;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recomendations_app.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Comment too short")]
        [MaxLength(255, ErrorMessage = "Comment too long")]
        public string CommentBody { get; set; } = string.Empty;

        [Required] public DateTime DateOfCreationInUTC { get; set; }

        public int UserRating { get; set; } = 0;
        [Required] public string AuthorName { get; set; } = string.Empty;
        public string ReviewId { get; set; }
        public ReviewModel Review { get; set; }
        public NpgsqlTsVector SearchVector { get; set; }
    }
}
