using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotNetLabs.Blazor
{
    public class LocalAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly ILocalStorageService _storageService;

        public LocalAuthenticationStateProvider(ILocalStorageService storageService)
        {
            _storageService = storageService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if(await _storageService.ContainKeyAsync("access_token"))
                {
                    string accessToken = await _storageService.GetItemAsStringAsync("access_token");

                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(accessToken);

                    var identity = new ClaimsIdentity(jwt.Claims, "Bearer");
                    var user = new ClaimsPrincipal(identity);

                    var state = new AuthenticationState(user);
                    NotifyAuthenticationStateChanged(Task.FromResult(state));

                    return state; 
                }
                else
                    return new AuthenticationState(new ClaimsPrincipal());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }

        public async Task LogoutAsync()
        {
            await _storageService.RemoveItemAsync("access_token");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal()))); 
        }
    }
}
