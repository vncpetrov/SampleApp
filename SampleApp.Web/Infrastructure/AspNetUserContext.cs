namespace SampleApp.Web.Infrastructure
{
    using Domain.Contracts;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Security.Claims;

    public class AspNetUserContext : IUserContext
    {
        private readonly IHttpContextAccessor accessor = new HttpContextAccessor();

        public AspNetUserContext()
        {
        }

        public Guid GetCurrentUserId()
            => Guid.Parse(
                this.accessor
                    .HttpContext
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
