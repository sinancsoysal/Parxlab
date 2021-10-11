using System.Collections.Generic;

namespace Parxlab.Common.Api
{
    public record ApiResult
    {
        public bool IsSuccess { get; init; }
        public ApiResultStatusCode StatusCode { get; init; }
        public IEnumerable<string> Errors { get; init; }
    }

    public record ApiResult<TData> : ApiResult
        where TData : class
    {
        public TData Data { get; set; }
    }
}