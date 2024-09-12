using Eapproval.Models;

namespace Eapproval.Services.IServices;

public interface IJwtTokenService{
    string GenerateToken(User user);
    User ParseToken(string token);
}