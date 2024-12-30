///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:56
///-----------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames.AdvancedAchievementSystem.Demo
{
    public class GoblinKing : Enemy
    {
        private void Awake()
        {
            AchievementManager.Oninitialized += OnInitialized;
        }

        private void OnInitialized()
        {
            Debug.Log("AchievementManager.Instance Has been initialized");
            //AchievementManager.Instance.Database.Achievements.Find(i => i.Achievement == AchievementList.Goblin_King).OnComplete += OnAchievementComplete;
        }

        private void OnEnable()
        {
            if (AchievementManager.Instance != null)
            {
                //AchievementManager.Instance.Database.Achievements.Find(i => i.Achievement == AchievementList.Goblin_King).OnComplete += OnAchievementComplete;
            }
            else
                Debug.Log("AchievementManager.Instance Has not been initialized yet");
        }

        public override void TakeDamage(int dmg)
        {
            base.TakeDamage(dmg);
        }

        //protected override void Death()
        //{
        //    //We update achievement by keys rather than individually by key here as the goblin king is a goblin and
        //    //We want the kings goblin progress to count towards other achievements but we do not want to call it 2 times,
        //    //So this call ensures an achievement is only updated 1 time

        //    AchievementManager.Instance.UpdateAchievementsByKeys(
        //        new AchievementKey[] {
        //            AchievementKey.Goblin,
        //            AchievementKey.Goblin_King
        //        },
        //        OnAchievementCompleteCallback: OnAchievementComplete);

        //    base.Death();
        //    AchievementManager.Oninitialized -= OnInitialized;
        //}

        //private void OnAchievementComplete(AchievementList achieevment)
        //{
        //    if (achieevment == AchievementList.Goblin_Slayer_King)
        //    {
        //        //Debug.Log("Achievement Completed Goblin King Granting Reward of 100 silver");
        //        Player.AddCurrency(100);
        //    }
        //}

        private void OnAchievementComplete()
        {
            Debug.Log("Achievement Completed Goblin King");
        }

        void OnDestroy()
        {
            AchievementManager.Oninitialized -= OnInitialized;
        }
    }
}