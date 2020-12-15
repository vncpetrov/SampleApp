namespace SampleApp.SqlDataAccess.Entities
{
    using Contracts;
    using Microsoft.AspNetCore.Identity;
    using System;

    public class UserEntity : IdentityUser<Guid>,
                              IEntityModel
    {
    }
}