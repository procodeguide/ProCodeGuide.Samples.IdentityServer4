using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProCodeGuide.Samples.IdentityServer4.IdentityConfiguration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "weatherApi",
                    ClientName = "ASP.NET Core Weather Api",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
                    AllowedScopes = new List<string> {"weatherApi.read"}
                },
                new Client
                {
                    ClientId = "oidcMVCApp",
                    ClientName = "Sample ASP.NET Core MVC Web App",
                    ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
    
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string> {"https://localhost:44346/signin-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "weatherApi.read"
                    },

                    RequirePkce = true,
                    AllowPlainTextPkce = false
                }
            };
        }
    }
}
