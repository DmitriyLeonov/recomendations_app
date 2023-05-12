using Microsoft.AspNetCore.Identity;

namespace Recomendations_app.Models
{
    public class UserModel:IdentityUser
    {
        public int LikesCount { get; set; } = 0;
        public List<ReviewModel> Reviews { get; set; } = new();
    }
}
