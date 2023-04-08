using Microsoft.Build.Framework;

namespace Recomendations_app.Models
{
    public class LikeModel
    {
        public long Id { get; set; }
        [Required]
        public string FromUserId { get; set; } = string.Empty;
        public UserModel? FromUser { get; set; }
        [Required]
        public string ToUserId { get; set; } = string.Empty;
        public UserModel? ToUser { get; set; }
        [Required]
        public string ReviewId { get; set; }
        public ReviewModel? Review { get; set; }
    }
}