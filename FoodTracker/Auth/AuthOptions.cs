namespace FoodTracker.Auth
{
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public class AuthOptions
    {
        private const string SecretKey = "food-tracker-secret--key";
        public const string Issuer = "FoodTrackerApi";
        public const string Audience = "FoodTrackerClient";
        public const int Lifetime = 9999;
        public static SymmetricSecurityKey Key 
        {
            get 
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
            }
        }
    }
}