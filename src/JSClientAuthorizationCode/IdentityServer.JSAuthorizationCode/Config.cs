// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.CookiePolicy;
using System.Collections.Generic;

namespace IdentityServer.JSAuthorizationCode
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
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
                    ClientName="java script client",
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

    }
}