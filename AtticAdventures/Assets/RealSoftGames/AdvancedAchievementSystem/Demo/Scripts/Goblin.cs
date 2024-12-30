///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames.AdvancedAchievementSystem.Demo
{
    public class Goblin : Enemy
    {
        public override void TakeDamage(int dmg)
        {
            base.TakeDamage(dmg);
        }

        protected override void Death()
        {
            //AchievementManager.Instance.UpdateAchievementsByKey(AchievementKey.Goblin, OnAchievementUpdate, OnAchievementComplete);
            base.Death();
        }

        //private void OnAchievementUpdate(AchievementList achievement)
        //{
        //    if (achievement == AchievementList.Goblin_Slayer)
        //        Debug.Log("Goblin Slayer OnAchievementUpdate");
        //}

        //private void OnAchievementComplete(AchievementList achievement)
        //{
        //    if (achievement == AchievementList.Goblin_Slayer)
        //        Debug.Log("Goblin Slayer OnAchievementComplete");
        //}
    }
}