namespace FoodTracker.Data.Context.Configurations
{
    using FoodTracker.Core.Entities;
    using FoodTracker.Data.Context.Configurations.Base;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasIndex(u => u.Email)
                .IsUnique();
            builder
                .Property(u => u.Email)
                .IsRequired();

            builder
                .HasIndex(u => u.Password)
                .IsUnique();
            builder
                .Property(u => u.Password)
                .IsRequired();

            base.Configure(builder);
        }
    }
}