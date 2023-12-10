using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DummyIdentityServer.Web.Controllers;

public class AuthorizeController : Controller
{
    [HttpPost]
    [AllowAnonymous]
    public ActionResult<string> IssueAuthorizationToken()
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "testid"),
            new Claim("mycustomclaim", "customclaimvalue")
        };

        var secretBytes = Encoding.UTF8.GetBytes(Constants.Constants.SecretKey);
        var key = new SymmetricSecurityKey(secretBytes);
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            Constants.Constants.Issuer,
            Constants.Constants.Audience,
            claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(1),
            signingCredentials);

        var tokenJsonString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return Ok(new
        {
            accessToken = tokenJsonString
        });
    }
}