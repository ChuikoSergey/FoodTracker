namespace FoodTracker.Core.Entities.Base
{
    using System;

    public interface IEntity
    {
        Guid Id { get; set; }
    }
}