// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.CookiePolicy;
using System.Collections.Generic;
using IdentityServer4.Test;
using System.Security.Claims;
using IdentityModel;

namespace IdentityServer.JSAuthorizationCode
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api1","Test api1")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId="js",
                    ClientName="javascript client",
                    AllowedGrantTypes=GrantTypes.Code,
                    RequirePkce=true,
                    RequireClientSecret=false,

                    RedirectUris={ "http://localhost:5003/callback.html"},
                    PostLogoutRedirectUris={ "http://localhost:5003/index.html"},
                    AllowedCorsOrigins={ "http://localhost:5003"},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                }

            };

        public static List<TestUser> Users =>
            new List<TestUser>{
                new TestUser{SubjectId="1",Username="arwen",Password="arwen",Claims={
                    new Claim(JwtClaimTypes.Name,"arwen"),
                    new Claim(JwtClaimTypes.Name,"arwen xyz"),
                    new Claim(JwtClaimTypes.GivenName,"arwen"),
                    new Claim(JwtClaimTypes.FamilyName,"xyz"),
                    new Claim(JwtClaimTypes.Email,"123456@qq.com"),
                    new Claim(JwtClaimTypes.EmailVerified,"true",ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite,"http://arwen.xyz"),
                    new Claim(JwtClaimTypes.Address,@"{'street_address':'One Way','country':'ShenZhen'}",IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)


                }},
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