namespace SampleApp.Web.Controllers
{
    using Domain.Contracts;
    using Domain.Models;
    using DomainServices.CommandHandlers.Car.Create;
    using DomainServices.QueryHandlers.Car.GetByUser;
    using Microsoft.AspNetCore.Mvc;
    using Models.Car;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CarsController : Controller
    {
        // Commands
        private readonly ICommandHandler<CreateCarCommand> createCar;

        // Queries
        private readonly IQueryHandler<GetCarsByUser, IEnumerable<Car>> getUserCars;

        private readonly IUserContext userContext;

        public CarsController(
            ICommandHandler<CreateCarCommand> createCar,
            IQueryHandler<GetCarsByUser, IEnumerable<Car>> getUserCars,
            IUserContext userContext)
        {
            if (createCar is null)
                throw new ArgumentNullException(nameof(createCar));

            if (getUserCars is null)
                throw new ArgumentNullException(nameof(getUserCars));

            if (userContext is null)
                throw new ArgumentNullException(nameof(userContext));

            this.createCar = createCar;
            this.getUserCars = getUserCars;
            this.userContext = userContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarRequestModel model)
        {
            CreateCarCommand command = new CreateCarCommand()
            {
                Colour = model.Colour,
                Condition = model.Condition,
                Price = model.Price
            };

            await this.createCar.HandleAsync(command);

            return Ok();
        }

        public async Task<IActionResult> List()
        {
            GetCarsByUser query = new GetCarsByUser()
            {
                UserId = this.userContext.GetCurrentUserId()
            };

            var userCars = await this.getUserCars.HandleAsync(query);

            List<CarListingModel> model = new List<CarListingModel>();
            foreach (var userCar in userCars)
            {
                model.Add(new CarListingModel
                {
                    Colour = userCar.Colour,
                    Condition = userCar.Condition,
                    Price = userCar.Price
                });
            }

            return PartialView("_UserCarsList", model);
        }
    }
}
