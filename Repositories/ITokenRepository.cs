using Microsoft.AspNetCore.Identity;

namespace hotel_clone_api.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
