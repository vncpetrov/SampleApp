namespace SampleApp.SqlDataAccess.Entities
{
    using Contracts;
    using SampleApp.Domain.Models.Enums;
    using System;

    public class CarEntity : IEntityModel
    {
        private readonly Guid id;

        public CarEntity(Guid id)
        {
            if (id != Guid.Empty)
                throw new ArgumentException(
                    "Empty GUIDs are not allowed.", nameof(id));

            this.id = id;
            this.Price = 0d;
        }

        public Guid Id
            => this.id; 

        public CarCondition Condition { get; set; }

        public CarColour Colour { get; set; }

        public double Price { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
