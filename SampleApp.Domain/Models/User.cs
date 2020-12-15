namespace SampleApp.Domain.Models
{
    using SampleApp.Domain.Models.Contracts;
    using System;

    public class User : IDomainModel
    {
        //private readonly Guid id;

        //public User(Guid id)
        //{
        //    if (id != Guid.Empty)
        //        throw new ArgumentException("Empty GUIDs are not allowed.", nameof(id));

        //    this.id = id;
        //}

        //public Guid Id
        //    => this.id;

        public string Email { get; set; }
    }
}
