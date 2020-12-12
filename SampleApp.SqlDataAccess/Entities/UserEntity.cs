namespace SampleApp.SqlDataAccess.Entities
{
    using Contracts;
    using Microsoft.AspNetCore.Identity;

    public class UserEntity : IdentityUser<int>,
                              IEntityModel
    {
    }
} 