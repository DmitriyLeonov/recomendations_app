using System.ComponentModel.DataAnnotations;

namespace Recomendations_app.Models
{
    public class GradeModel
    {
        public long Id { get; set; }
        [Required]
        [Range(1, 5)]
        public int Value { get; set; }
        [Required]
        public string AuthorName { get; set; } = string.Empty;
        public UserModel? Author { get; set; }
    }
}