namespace SampleApp.Domain.Contracts
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICarDataReader : IDataReader
    {
        Task<IEnumerable<Car>> GetByUserAsync(Guid userId);
    }
} 