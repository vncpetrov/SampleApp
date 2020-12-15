namespace SampleApp.Domain.Contracts
{
    using System;

    public interface IUserContext
    {  
        Guid GetCurrentUserId();
    }
} 