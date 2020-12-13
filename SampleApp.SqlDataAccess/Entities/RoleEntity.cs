namespace SampleApp.SqlDataAccess.Entities
{
    using Contracts;
    using Microsoft.AspNetCore.Identity;
    using System;

    public class RoleEntity : IdentityRole<Guid>,
                              IEntityModel
    {
    }
} 