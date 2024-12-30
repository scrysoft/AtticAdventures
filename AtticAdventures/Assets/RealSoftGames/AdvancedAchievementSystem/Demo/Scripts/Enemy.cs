///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RealSoftGames.AdvancedAchievementSystem.Demo
{
    public class Enemy : MonoBehaviour
    {
        public int health = 100;
        public Slider hpBar;

        //protected virtual void OnEnable()
        //{
        //    //AchievementManager.OnUpdateAchievement += UpdateAchievement;
        //    //AchievementManager.OnAchievementComplete += OnCompleteAchievement;
        //}
        //
        //protected virtual void OnDisable()
        //{
        //    //AchievementManager.OnUpdateAchievement -= UpdateAchievement;
        //    //AchievementManager.OnAchievementComplete += OnCompleteAchievement;
        //}

        protected virtual void OnUpdateAchievement(string achievement)
        {
            Debug.Log("OnUpdateAchievement : " + achievement);
        }

        protected virtual void OnCompleteAchievement(string achievement)
        {
            Debug.Log("OnCompleteAchievement : " + achievement);
        }

        public virtual void TakeDamage(int dmg)
        {
            if (health > 0)
            {
                health -= dmg;
                hpBar.value = health;
            }
            else if (health < 0)
                health = 0;

            if (health == 0)
                Death();
        }

        protected virtual void Death()
        {
            Destroy(gameObject);
        }
    }
}