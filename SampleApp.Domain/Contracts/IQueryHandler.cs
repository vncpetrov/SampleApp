namespace SampleApp.Domain.Contracts
{
    using System.Threading.Tasks;

    public interface IQueryHandler<TQuery, TOutput>
        where TQuery : IQuery<TOutput>
    {
        Task<TOutput> HandleAsync(TQuery query);
    }
} 