// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer.AuthorizationCodeGrant
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {new ApiResource("api1","Test Api1") };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client{
                    ClientId="mvc client",
                    ClientSecrets={ new Secret("MvcSecret".Sha256()) },
                    AllowedGrantTypes=GrantTypes.Code,
                    RequireConsent=false,
                    RequirePkce=true,

                    
            // where to redirect to after login
            RedirectUris = { "http://localhost:5002/signin-oidc" },
             // where to redirect to after logout
            PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

            AllowedScopes=new List<string>{
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "api1"
            },
            AllowOfflineAccess=true
                }
            };


        public static List<TestUser> Users =>
            new List<TestUser>
            {
                new TestUser{SubjectId="1",Username="arwen",Password="arwen",Claims={
                    new Claim(JwtClaimTypes.Name,"arwen xyz"),
                    new Claim(JwtClaimTypes.GivenName,"arwen"),
                    new Claim(JwtClaimTypes.FamilyName,"xyz"),
                    new Claim(JwtClaimTypes.Email,"123456@qq.com"),
                    new Claim(JwtClaimTypes.EmailVerified,"true",ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite,"http://arwen.xyz"),
                    new Claim(JwtClaimTypes.Address,@"{'street_address':'One Way','country':'ShenZhen'}",IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    } },
                new TestUser{ SubjectId="2",Username="bob",Password="bob",Claims={
                    new Claim(JwtClaimTypes.Name, "bob xyz"),
                    new Claim(JwtClaimTypes.GivenName, "bob"),
                    new Claim(JwtClaimTypes.FamilyName, "xyz"),
                    new Claim(JwtClaimTypes.Email, "456789@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim("location", "somewhere")
                    }

                }

            };
    }
}