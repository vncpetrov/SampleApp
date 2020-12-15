namespace SampleApp.Domain.Contracts
{
    using System.Threading.Tasks;

    public interface ICommandHandler<TCommand>
    {
        Task HandleAsync(TCommand command); 
    }
} 