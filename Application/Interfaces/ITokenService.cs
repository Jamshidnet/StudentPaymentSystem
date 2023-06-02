using Application.Token;
using Domein.AccessEntities;
using Domein.Models;
using Domein.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public  interface ITokenService
    {
        public Task<Tokens> CreateTokensAsync(AUser user);
        public Task<Tokens> CreateTokensFromRefresh(ClaimsPrincipal principal, UserRefreshToken savedRefreshToken);
        public ClaimsPrincipal GetClaimsFromExpiredToken(string token);

        public Task<bool> AddRefreshToken(UserRefreshToken tokens);
        public bool Update(UserRefreshToken tokens);
        public IQueryable<UserRefreshToken> Get(Expression<Func<UserRefreshToken, bool>> predicate);
        public bool Delete(UserRefreshToken token);
    }
}
