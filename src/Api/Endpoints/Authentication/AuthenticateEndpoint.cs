using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Endpoints.Authentication.Requests;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Api.Endpoints.Authentication;

public class AuthenticateEndpoint :Endpoint<LoginRequest>
{
    public override void Configure()
    {
        Post("/api/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        // if (await authService.CredentialsAreValid(req.Username, req.Password, ct))
        //mocking the auth// check with db users
        if (true)
        {
            //var jwtToken = JwtBearer.CreateToken(
            //    o =>
            //    {
            //        o.SigningKey = "A secret token signing key";
            //        o.ExpireAt = DateTime.UtcNow.AddDays(1);
            //        o.User.Roles.Add("Manager", "Auditor");
            //        o.User.Claims.Add(("UserName", req.Username));
            //        o.User["UserId"] = "001"; //indexer based claim setting
            //    });
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nRAStewNgwTdo2k5Y75qag3gseg64gh6hgfgh"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, req.Username),
                //new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(issuer: "moq", audience: "moq",
                claims: claims, expires: DateTime.Now.AddMinutes(int.Parse("60")),
                signingCredentials: credentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            await SendAsync(
                new
                {
                    req.Username,
                    Token = encodedToken
                });
        }
        else
            ThrowError("The supplied credentials are invalid!");
    }
}
