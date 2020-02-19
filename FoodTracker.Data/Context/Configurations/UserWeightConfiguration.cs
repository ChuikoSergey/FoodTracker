namespace FoodTracker.Data.Context.Configurations
{
    using FoodTracker.Core.Entities;
    using FoodTracker.Data.Context.Configurations.Base;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserWeightConfiguration : BaseEntityConfiguration<UserWeight>
    {
        public override void Configure(EntityTypeBuilder<UserWeight> builder)
        {
            builder
                .HasOne(uw => uw.User)
                .WithMany(u => u.UserWeights)
                .HasForeignKey(uw => uw.UserId);

            
            base.Configure(builder);
        }
    }
}