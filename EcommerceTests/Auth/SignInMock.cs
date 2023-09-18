using EcommerceAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcommerceTests.Auth
{
    public class SimpleUserConfirmation : IUserConfirmation<User>
    {
        public Task<bool> IsConfirmedAsync(UserManager<User> manager, User user)
        {
            return Task.FromResult(true);
        }
    }

    public class SignInManagerMock : SignInManager<User>
    {
        public SignInManagerMock(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<User> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await UserManager.FindByNameAsync(userName);

            if (user != null && await UserManager.CheckPasswordAsync(user, password))
            {
                var claimsPrincipal = await ClaimsFactory.CreateAsync(user);
                await Context.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal, null);
                return SignInResult.Success;
            }

            return SignInResult.Failed;
        }
    }
}