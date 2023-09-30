using Player;
using System;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Manages win/lost states and time scaling
    /// </summary>
    public static class GameManager
    {
        public static int PlayerScore { get; private set; }
        public static bool IsGameOver   { get; private set; } = false;
        public static bool IsGamePaused { get => Time.timeScale == 0f; }

        public static event Action OnScoreUpdated;
        public static event Action OnGameLost;
        public static event Action OnGameWon;

        public static void Reset()
        {
            PlayerScore = 0;
            IsGameOver = false;
            EnableTimeScale(true);
        }

        public static void UpdateScore(int points)
        {
            PlayerScore += points;
            OnScoreUpdated?.Invoke();
        }
        
        public static void EnableTimeScale(bool enable) => Time.timeScale = enable ? 1f : 0f;

        public static void LevelLost()
        {
            IsGameOver = true;
            OnGameLost?.Invoke();
        }

        public static void LevelWon()
        {
            IsGameOver = true;
            OnGameWon?.Invoke();
        }
    }
}
