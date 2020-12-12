namespace SampleApp.SqlDataAccess.Entities
{
    using Contracts;
    using Microsoft.AspNetCore.Identity;

    public class RoleEntity : IdentityRole<int>, IEntityModel
    {
    }
} 