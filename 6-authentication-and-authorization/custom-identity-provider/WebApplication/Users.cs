using IdentityModel;
using IdentityServer4.Test;
using System.Security.Claims;

namespace WebApplication;

internal static class Users
{
    public static List<TestUser> Get()
    {
        return new List<TestUser> {
            new TestUser {
                SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                Username = "manager",
                Password = "password",
                Claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Email, "manager@gmail.com"),
                    new Claim(JwtClaimTypes.Role, "manager")
                }
            },
            new TestUser {
                SubjectId = "31B60B9A-EDC3-4481-9CDB-1614B6D10096",
                Username = "buyer",
                Password = "password",
                Claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Email, "buyer@gmail.com"),
                    new Claim(JwtClaimTypes.Role, "buyer")
                }
            }
        };
    }
}
