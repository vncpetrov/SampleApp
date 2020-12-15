namespace SampleApp.DomainServices.QueryHandlers.Car.GetStatisticsByUser
{
    using Domain.Contracts;
    using System;

    public class GetStatisticsByUser : IQuery<GetStatisticsByUserOutputModel>
    {
        public Guid UserId { get; set; }
    }
} 