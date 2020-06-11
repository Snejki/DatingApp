namespace DatingApp.Infrastructure.Services.Abstract
{
    public interface IJwtService : IService
    {
        public string GenerateToken(int userId);
    }
}
