///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames.AdvancedAchievementSystem.Demo
{
    public class Bullet : MonoBehaviour
    {
        private void Update()
        {
            transform.Translate(Vector3.forward * 20f * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Enemy")
            {
                col.GetComponent<Enemy>().TakeDamage(20);
            }
            //Debug.Log($"Col Enter {col.gameObject.name}");
            Destroy(gameObject);
        }
    }
}