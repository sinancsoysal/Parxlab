using System;

namespace Parxlab.Data.Dtos
{
    public record RefreshTokenDto
    {
        public Guid Id { get; init; }
        public string JwtId { get; init; }
        public DateTime ExpirationDate { get; init; }
        public bool IsUsed { get; init; }
        public bool IsInvalidated { get; init; }
    }
}
