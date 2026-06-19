using System;

namespace Register.APPLICATION.Interface
{
    public interface ICachableQuery
    {
        string CacheKey { get; }
        string? VersionKey { get; }
        TimeSpan? Expiration { get; }
    }
}
