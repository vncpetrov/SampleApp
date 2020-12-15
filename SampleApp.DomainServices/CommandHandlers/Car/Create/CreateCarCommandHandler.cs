namespace SampleApp.DomainServices.CommandHandlers.Car.Create
{
    using Domain.Contracts;
    using System;
    using System.Threading.Tasks;
    using Car = Domain.Models.Car;

    public class CreateCarCommandHandler : ICommandHandler<CreateCarCommand>
    {
        private readonly ICarDataWriter carDataWriter;
        private readonly IUserContext userContext;

        public CreateCarCommandHandler(
            ICarDataWriter carDataWriter,
            IUserContext userContext)
        {
            if (carDataWriter is null)
                throw new ArgumentNullException(nameof(carDataWriter));

            if (userContext is null)
                throw new ArgumentNullException(nameof(userContext));

            this.carDataWriter = carDataWriter;
            this.userContext = userContext;
        }

        public async Task HandleAsync(CreateCarCommand command)
        {
            Guid loggedUserId = this.userContext.GetCurrentUserId();

            Car car = new Car(
                command.Colour,
                command.Condition, 
                command.Price,
                loggedUserId);

            await this.carDataWriter.CreateAsync(
                id: Guid.NewGuid(),
                car: car);
        }
    }
}
