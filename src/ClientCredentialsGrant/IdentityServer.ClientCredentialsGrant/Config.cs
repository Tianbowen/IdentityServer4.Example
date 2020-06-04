// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer.ClientCredentialsGrant
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("api1","Test Api")
            };
        
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId="clientCredentials",
                    //没有交互用户，使用clientid 或 secret 来进行身份验证
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    //用于身份验证的密钥
                    ClientSecrets={ 
                        new Secret("ClientCredentialsSecret".Sha256())
                    },
                    AllowedScopes={ "api1" }

                }
            };        
    }
}