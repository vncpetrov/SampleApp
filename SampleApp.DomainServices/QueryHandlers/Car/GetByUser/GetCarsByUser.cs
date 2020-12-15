namespace SampleApp.DomainServices.QueryHandlers.Car.GetByUser
{
    using Domain.Contracts;
    using System;
    using System.Collections.Generic;
    using Car = Domain.Models.Car;

    public class GetCarsByUser : IQuery<IEnumerable<Car>>
    {
        public Guid UserId { get; set; }
    }
} 