using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Recomendations_app.Models
{
    [Table("Tags")]
    public class TagModel
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Tag is too long")]
        [MinLength(1, ErrorMessage = "Tag is too short")]
        public string Name { get; set; } = string.Empty;

        public List<ReviewModel>? Reviews { get; set; } = new();

        public TagModel(string name)
        {
            Name = name;
        }

        public TagModel()
        {
        }


    }
}