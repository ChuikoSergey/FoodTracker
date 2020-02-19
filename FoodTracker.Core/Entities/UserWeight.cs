namespace FoodTracker.Core.Entities
{
    using System;
    using FoodTracker.Core.Entities.Base;

    public class UserWeight : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public float Weight { get; set; }
        public DateTime Date { get; set; }
    }
}