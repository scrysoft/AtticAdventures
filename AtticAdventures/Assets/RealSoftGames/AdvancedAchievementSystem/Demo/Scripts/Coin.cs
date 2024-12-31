///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames.AdvancedAchievementSystem.Demo
{
    public class Coin : MonoBehaviour
    {
        private void Update()
        {
            transform.Rotate(new Vector3(0, 0, 1));
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.name == "Player")
            {
                Player.AddCurrency(1);
                //AchievementManager.Instance.UpdateAchievementsByKey(AchievementKey.Coin);
                Destroy(gameObject);
            }
        }
    }
}