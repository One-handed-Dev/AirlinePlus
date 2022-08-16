using System;
using System.Linq;
using Common.Infrastructure;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Common.Application.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Common.Application
{
    public sealed class AuthenticationService : Contracts.IAuthenticationService
    {
        #region Init
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor contextAccessor;

        public AuthenticationService(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.contextAccessor = contextAccessor;
        }
        #endregion

        public AuthenticationDto GetCurrentAccountInfo()
        {
            if (!IsAuthenticated()) return new();

            var claims = contextAccessor.HttpContext.User.Claims.ToList();
            var roleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);

            var result = new AuthenticationDto()
            {
                RoleId = roleId,
                Role = DefinedRoles.GetRoleBy(roleId),
                Fullname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value,
                Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value),
            };

            return result;
        }

        public async Task<bool> PlaceCookiesAsync(AuthenticationDto command)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, command.Fullname),
                new Claim("PhoneNumber", command.PhoneNumber),
                new Claim("AccountId", command.Id.ToString()),
                new Claim(ClaimTypes.Role, command.RoleId.ToString()),
            };

            var expiration = configuration.GetValue<int>("ExpireTimeSpan");
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(expiration) };
            await contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return true;
        }

        public bool IsAuthenticated() => contextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public void RemoveCookies() => contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        public long GetCurrentAccountId() => IsAuthenticated() ? long.Parse(contextAccessor.HttpContext.User.Claims.First(x => x.Type == "AccountId")?.Value) : 0;
    }
}