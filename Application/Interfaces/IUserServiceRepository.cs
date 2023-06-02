using Application.Token;
using Domein.AccessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserServiceRepository
    {
        UserRefreshToken AddUserRefreshTokens(UserRefreshToken user);
        Task<UserRefreshToken> UpdateUserRefreshTokens(UserRefreshToken user);
        UserRefreshToken? GetSavedRefreshTokens(string refreshToken);
        Task<int> SaveCommit();
        List<UserRefreshToken> GetAllUserRefreshTokens();
        UserRefreshToken? GetUserRefreshTokensById(Guid id);
        Task DeleteRefreshToken(UserRefreshToken user);

    }
}
