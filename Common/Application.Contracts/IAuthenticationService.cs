using System.Threading.Tasks;

namespace Common.Application.Contracts
{
    public interface IAuthenticationService
    {
        void RemoveCookies();
        bool IsAuthenticated();
        long GetCurrentAccountId();
        AuthenticationDto GetCurrentAccountInfo();
        Task<bool> PlaceCookiesAsync(AuthenticationDto command);
    }
}
