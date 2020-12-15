using Microsoft.AspNetCore.Mvc;
using SampleApp.Domain.Contracts;
using SampleApp.DomainServices.CommandHandlers.Car.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICommandHandler<CreateCarCommand> createCar;

        public CarsController(ICommandHandler<CreateCarCommand> createCar)
        {
            if (createCar is null)
                throw new ArgumentNullException(nameof(createCar));

            this.createCar = createCar;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
