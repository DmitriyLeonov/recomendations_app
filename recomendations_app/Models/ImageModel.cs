using Recomendations_app.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace Recomendations_app.Models
{
    public class ImageModel
    {
        public long Id { get; set; }
        [MaxFileSize(1 * 3840 * 3840)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public string? ImageLink { get; set; }
        public string? ImageStorageName { get; set; }
        [Required]
        public string ReviewId { get; set; }
        public ReviewModel? Review { get; set; }
    }
}
