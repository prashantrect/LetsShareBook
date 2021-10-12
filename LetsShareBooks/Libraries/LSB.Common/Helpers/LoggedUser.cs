//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using LSB.Contracts;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace LSB.Helpers
{
    [ExcludeFromCodeCoverage]
    public class LoggedUser : ILoggedUser
    {
        private readonly ClaimsPrincipal user;
        public LoggedUser(IHttpContextAccessor httpContext)
        {
            user = httpContext.HttpContext?.User;
            Name = user?.FindFirst(ClaimTypes.Name)?.Value;
            Email = user?.FindFirst(ClaimTypes.Email)?.Value;
        }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
