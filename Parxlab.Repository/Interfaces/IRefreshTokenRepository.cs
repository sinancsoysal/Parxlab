using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parxlab.Data.Dtos;
using Parxlab.Entities;

namespace Parxlab.Repository.Interfaces
{
    public interface IRefreshTokenRepository:IRepository<RefreshToken>
    {

        Task<IEnumerable<RefreshTokenDto>> GetUnusedToken(Guid userId);
        Task<IEnumerable<RefreshTokenDto>> GetByToken(string refreshToken);
        Task<int> SetUsed(Guid id);
    }
}
