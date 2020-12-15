namespace SampleApp.Domain.Contracts
{
    using Models;
    using System;
    using System.Threading.Tasks;

    public interface ICarDataWriter : IDataWriter
    {
        Task CreateAsync(Guid id, Car car);
    }
}