using simpleRestApi.Data;
using simpleRestApi.Interfaces.Auth;

namespace simpleRestApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        DataContextEF _ef;
        public AuthRepository(IConfiguration config)
        {
            _ef = new DataContextEF(config);

        }
    }
}