using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> Apis => new[]
        {
            new ApiResource("product.api", "product api")
        };

        public static IEnumerable<Client> Client => new[]
        {
            new Client
            {
                ClientId = "webclient",
                ClientName = "web client",
                RequireConsent = false,
                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                RequirePkce = false,
                ClientSecrets = { new Secret("e41316e77ea871fe63180c82bc268544a361fa4b".Sha256()) },
                UserSsoLifetime = 3600,
                RedirectUris = { "https://localhost:44381/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44381/",
                PostLogoutRedirectUris = { "https://localhost:44381/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "product.api" }
            },
            new Client
            {
                ClientId = "spaclient",
                ClientName = "SPA client",
                ClientUri = "https://localhost:44382",
                RequireConsent = false,
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = { "https://localhost:44382/index.html" },
                PostLogoutRedirectUris = { "https://localhost:44382/index.html" },
                AllowedCorsOrigins = { "https://localhost:44382" },
                AllowedScopes = { "openid", "profile", "product.api" }
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("product.api", new[]
            {
                "role"
            })
        };
    }
}
