using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace _Project.Core.Trackers
{
    public class PlayFabGameTracker : IGameTracker
    {
        private const string WinsTrackerName = "WinsScore";

        [Inject] private IPlayerProfileProvider _playerProvider;

        public void RecordWin()
        {
            PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(), OnGetAndIncrease, OnPlayFabError);
        }

        public void ResetWinsAmount()
        {
            PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(), OnGetAndReset, OnPlayFabError);
        }

        public void InitializeWins()
        {
            PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(), OnSetPlayerWinsAmount,
                OnPlayFabError);
        }

        private void OnSetPlayerWinsAmount(GetPlayerStatisticsResult result)
        {
            foreach (StatisticValue statistic in result.Statistics)
                if (statistic.StatisticName == WinsTrackerName)
                {
                    _playerProvider.WinsAmount = statistic.Value;
                    break;
                }
        }

        private void OnGetAndIncrease(GetPlayerStatisticsResult result)
        {
            var currentWins = 0;

            foreach (StatisticValue statistic in result.Statistics)
                if (statistic.StatisticName == WinsTrackerName)
                {
                    currentWins = statistic.Value;
                    break;
                }

            int playerProviderWinsAmount = currentWins + 1;
            UpdateWinsAmount(playerProviderWinsAmount);
        }

        private void OnGetAndReset(GetPlayerStatisticsResult result)
        {
            UpdateWinsAmount(0);
        }

        private void UpdateWinsAmount(int newWinsAmount)
        {
            UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new()
                    {
                        StatisticName = WinsTrackerName,
                        Value = newWinsAmount
                    }
                }
            };

            PlayFabClientAPI.UpdatePlayerStatistics(request, OnStatisticsUpdated, OnPlayFabError);
            _playerProvider.WinsAmount = newWinsAmount;
        }

        private void OnStatisticsUpdated(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("Player statistics updated successfully.");
        }

        private void OnPlayFabError(PlayFabError error)
        {
            Debug.LogError("Error updating player statistics: " + error.GenerateErrorReport());
        }
    }
}