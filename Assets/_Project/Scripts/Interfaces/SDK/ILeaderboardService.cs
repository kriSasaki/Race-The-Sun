namespace Project.Interfaces.SDK
{
    public interface ILeaderboardService
    {
        bool IsPlayerAuthorized { get; }
        void AuthorizePlayer();
        // LoadPlayers(Action<List<LeaderboardPlayer>, int> onLoadCallback);

        void SetPlayerScore(int score);
    }
}