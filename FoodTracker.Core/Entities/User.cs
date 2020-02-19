namespace FoodTracker.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using FoodTracker.Core.Entities.Base;

    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
        public byte Height { get; set; }
        public ICollection<UserWeight> UserWeights { get; set; }
    }
}