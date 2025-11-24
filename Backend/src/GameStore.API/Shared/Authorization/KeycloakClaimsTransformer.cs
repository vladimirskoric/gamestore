using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GameStore.Api.Shared.Authorization;

public class KeycloakClaimsTransformer(ILogger<KeycloakClaimsTransformer> logger)
{
    public void Transform(TokenValidatedContext context)
    {
        var identity = context.Principal?.Identity as System.Security.Claims.ClaimsIdentity;

        var scopeClaim = identity?.FindFirst(ClaimTypes.Scope);

        if(scopeClaim is null)
        {
            return;
        }

        var claims = context.Principal?.Claims;

        foreach(var claim in claims)
        {
            logger.LogInformation("Claim Type: {Type} - Claim Value: {Value}", claim.Type, claim.Value);
        }

        var scopes = scopeClaim.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        identity?.RemoveClaim(scopeClaim);

        identity?.AddClaims(scopes.Select(scope => new System.Security.Claims.Claim(ClaimTypes.Scope, scope)));
    }
}
