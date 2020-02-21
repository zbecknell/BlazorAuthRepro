using System;
using System.Diagnostics.Tracing;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWasmTest
{
    public class TestAuthenticationStateProvider : AuthenticationStateProvider
    {
        static AuthenticationState SignedOutAuthenticationState => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "username")
        })));

        AuthenticationState _authenticationState = SignedOutAuthenticationState;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(_authenticationState);
        }

        public void SignIn()
        {
            var signedInIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "username")
            }, "someAuthTypeName");

            _authenticationState = new AuthenticationState(new ClaimsPrincipal(signedInIdentity));
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            Console.WriteLine("Signed In!");
        }

        public void SignOut()
        {
            _authenticationState = SignedOutAuthenticationState;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
