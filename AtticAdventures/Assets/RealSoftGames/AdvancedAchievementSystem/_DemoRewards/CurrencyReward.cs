using UnityEngine;
using RealSoftGames.AdvancedAchievementSystem.Demo;

namespace RealSoftGames.AdvancedAchievementSystem
{
    [CreateAssetMenu(fileName = "CurrencyReward", menuName = "RealSoft Games/Advanced Achievement System/Currency", order = 1)]
    public class CurrencyReward : AchievementReward
    {
        public int RewardAmount;
        public override void Execute()
        {
            Player.AddCurrency(RewardAmount);
        }
    }
}