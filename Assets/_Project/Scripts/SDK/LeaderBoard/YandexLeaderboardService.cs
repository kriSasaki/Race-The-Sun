using System;
using System.Collections.Generic;
using Project.Interfaces.SDK;
using YG;

namespace Project.SDK.Leaderboard
{
    public class YandexLeaderboardService : ILeaderboardService
    {
        private const string LeaderboardName = "Leaderboard";

        public bool IsPlayerAuthorized => YandexGame.auth;

        public void SetPlayerScore(int score)
        {
            if (!IsPlayerAuthorized)
                return;

            YandexGame.NewLeaderboardScores(LeaderboardName, score);
        }
        
        public void AuthorizePlayer()
        {
            YandexGame.AuthDialog();
        }
    }
}