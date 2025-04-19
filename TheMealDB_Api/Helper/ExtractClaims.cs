using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TheMealDB_Api.Helper
{
    public class ExtractClaims
    {
        public static int? ExtractUserId(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var JwtToken = tokenHandler.ReadJwtToken(token);

                var userIdClaims = JwtToken.Claims.FirstOrDefault(t => t.Type == ClaimTypes.NameIdentifier);

                if (userIdClaims != null && int.TryParse(userIdClaims.Value, out int userId))
                {
                    return userId;

                }
                return null;

            }
            catch (Exception)
            {

                return null;
            }


        }
    }
}
