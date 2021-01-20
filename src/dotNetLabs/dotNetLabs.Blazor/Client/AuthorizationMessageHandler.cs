using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace dotNetLabs.Blazor
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {

        private readonly ILocalStorageService _storageService;

        public AuthorizationMessageHandler(ILocalStorageService storageService)
        {
            _storageService = storageService; 
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(await _storageService.ContainKeyAsync("access_token"))
            {
                string accessToken = await _storageService.GetItemAsStringAsync("access_token");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken); 
            }

            return await base.SendAsync(request, cancellationToken);
        }

    }
}
