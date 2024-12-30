using UnityEngine;

namespace RealSoftGames.AdvancedAchievementSystem
{
    public abstract class AchievementReward : ScriptableObject
    {
        public abstract void Execute();
    }
}
