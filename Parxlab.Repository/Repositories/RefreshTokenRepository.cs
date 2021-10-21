using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Parxlab.Data;
using Parxlab.Data.Dtos;
using Parxlab.Entities;
using Parxlab.Repository.Interfaces;
using RepoDb;

namespace Parxlab.Repository.Repositories
{
    public class RefreshTokenRepository:Repository<RefreshToken>,IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context, IDbConnection connection) : base(context, connection)
        {
        }

        public Task<IEnumerable<RefreshTokenDto>> GetUnusedToken(Guid userId)
        {
            return connection.ExecuteQueryAsync<RefreshTokenDto>(@"SELECT [r].[Id]
            FROM [dbo].[RefreshToken] AS[r]
            WHERE [r].[IsUsed] = 0 AND [r].[UserId]= @userId", new {userId});
        }

        public Task<IEnumerable<RefreshTokenDto>> GetByToken(string refreshToken)
        {
            return connection.ExecuteQueryAsync<RefreshTokenDto>(@"SELECT [r].[Id], [r].[ExpirationDate], [r].[IsInvalidated], [r].[IsUsed], [r].[JwtId]
            FROM [dbo].[RefreshToken] AS[r]
            WHERE [r].[Token] = @refreshToken", new{refreshToken});
        }

        public Task<int> SetUsed(Guid id)
        {
           return connection.ExecuteNonQueryAsync("UPDATE [RefreshToken] SET [IsUsed] = 1 WHERE [Id] = @id", new {id});
        }
    }
}
