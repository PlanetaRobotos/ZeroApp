using System.Collections.Generic;

namespace _Project.Scripts.Infrastructure
{
    public interface IDataConverter<TIn, TOut>
    {
        List<TIn> Convert(TOut leaderboardData, string userId);
    }
}