using IdentityServer4.Models;
using System.Collections.Generic;

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
                RedirectUris = { "https://localhost:44382/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44382/",
                PostLogoutRedirectUris = { "https://localhost:44382/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "product.api" }
            },
            new Client
            {
                ClientId = "spaclient",
                ClientName = "SPA client",
                ClientUri = "http://localhost:3000",
                RequireConsent = false,
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = { "http://localhost:3000/post-login", "http://localhost:3000/post-logout" },
                PostLogoutRedirectUris = { "http://localhost:3000/post-logout" },
                AllowedCorsOrigins = { "http://localhost:3000" },
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
