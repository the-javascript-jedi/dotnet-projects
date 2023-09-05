using API.Entities;

namespace API.Interfaces;
// Any other class that implements this interface has to support this method and it has to return a string from that method. And it has to take an app user as an argument.
// It's like a contract between our interface and our implementation.
public interface ITokenService
{
    string CreateToken(AppUser user);
}
