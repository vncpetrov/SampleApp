namespace SampleApp.Domain.Models
{
    using Contracts;
    using SampleApp.Domain.Models.Enums;
    using System;

    public class Car : IDomainModel
    {
        //private readonly Guid id;

        //public Car(Guid id)
        //{
        //    if (id != Guid.Empty)
        //        throw new ArgumentException("Empty GUIDs are not allowed.", nameof(id));

        //    this.id = id;
        //}

        //public Guid Id
        //    => this.id;

        private double price;
        private Guid userId;

        public Car(
            CarColour colour,
            CarCondition condition,
            double price,
            Guid userId)
        {
            this.Colour = colour;
            this.Condition = condition;
            this.Price = price;
            this.UserId = userId;
        }

        public CarColour Colour { get; set; }

        public CarCondition Condition { get; set; }

        public double Price
        {
            get => this.price;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(
                        nameof(price),
                        "The price must be a positive value.");

                this.price = value;
            }
        }

        public Guid UserId
        {
            get => this.userId;
            set
            {
                if (value == Guid.Empty)
                    throw new ArgumentException(
                        "Empty GUIDs are not allowed.", "userId");

                this.userId = value;
            }
        }
        public User User { get; set; }
    }
}
