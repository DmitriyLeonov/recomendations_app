using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recomendations_app.Models
{
    public class Comment
    {
        public long Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Comment too short")]
        [MaxLength(255, ErrorMessage = "Comment too long")]
        public string CommentBody { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfCreationInUTC { get; set; }
        [Required]
        public string AuthorName { get; set; } = string.Empty;
        public string ReviewId { get; set; }
        public ReviewModel Review { get; set; }}
}
