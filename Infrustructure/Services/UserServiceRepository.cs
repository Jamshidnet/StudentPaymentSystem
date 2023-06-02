using Application.Abstraction;
using Application.Interfaces;
using Application.Token;
using Domein.AccessEntities;

namespace Infrustructure.Services
{
    public delegate Task DeleteRefreshTokenDelegate(UserRefreshToken configuration);

    public class UserServiceRepository :IUserServiceRepository
    {
        private readonly IApplicationDbContext _db;

        private  DeleteRefreshTokenDelegate deleteRefresh;
        public UserServiceRepository(IApplicationDbContext db)
        {
            _db = db;
             deleteRefresh = DeleteRefreshToken;
        }

    public UserRefreshToken AddUserRefreshTokens(UserRefreshToken user)
        {
            _db.UserRefreshToken.Add(user);
            deleteRefresh.Invoke(user);
            return user;
        }

        public UserRefreshToken? GetSavedRefreshTokens(string refreshToken)
        {
            return _db.UserRefreshToken.FirstOrDefault(x => x.RefreshToken == refreshToken);

        }

        public async Task<int> SaveCommit()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task<UserRefreshToken> UpdateUserRefreshTokens(UserRefreshToken userRefreshTokens)
        {
            var user = _db.UserRefreshToken.FirstOrDefault(x => x.UserName == userRefreshTokens.UserName);
            if(user is not null)
            _db.UserRefreshToken.Remove(user);
            await _db.SaveChangesAsync();
            await _db.UserRefreshToken.AddAsync(userRefreshTokens);
            await _db.SaveChangesAsync();
            return userRefreshTokens;
        }

        public List<UserRefreshToken> GetAllUserRefreshTokens()
        {
            return _db.UserRefreshToken.ToList();
        }

        public UserRefreshToken? GetUserRefreshTokensById(Guid id)
        {
            return _db.UserRefreshToken.FirstOrDefault(x => x.Id == id);
        }

        public async Task DeleteRefreshToken(UserRefreshToken user)
        {
            await Task.Delay(TimeSpan.FromMinutes(3));
            _db.UserRefreshToken.Remove(user);
           await  _db.SaveChangesAsync();
        }
    }
}
