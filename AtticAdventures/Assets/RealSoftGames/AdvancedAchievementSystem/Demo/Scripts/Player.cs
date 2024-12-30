///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace RealSoftGames.AdvancedAchievementSystem.Demo
{
    public class Player : MonoBehaviour
    {
        public static Action<int> OnCurrencyUpdate;
        private static int currency = 0;


        private NavMeshAgent agent;
        private GameObject target;
        public GameObject bullet;
        public GameObject shootFrom;

        private float fireRate = 0.25f, nextFire;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //    Debug.Log("Application.Datapath: " + Application.dataPath);

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (target)
            {
                var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);

                Shoot();
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        //Lookat target
                        target = hit.collider.gameObject;
                    }
                    else
                    {
                        target = null;
                        agent.destination = hit.point;
                        //AchievementManager.Instance.UpdateAchievementsByKey(AchievementKey.Click, OnUpdateClick);
                    }
                }
            }
        }

        private void Shoot()
        {
            if (Time.time > nextFire)
            {
                GameObject go = Instantiate(bullet, shootFrom.transform.position, shootFrom.transform.rotation);
                go.transform.LookAt(target.transform);
                nextFire = Time.time + fireRate;
                //AchievementManager.Instance.UpdateAchievementsByKey(AchievementKey.Bullet, OnUpdateBullet);
            }
        }

        //private void OnUpdateBullet(AchievementList achievement)
        //{
        //    if (achievement == AchievementList.Novice_Weapon_User)
        //    {
        //    }
        //    //Debug.Log("Pew Pew");
        //}

        private void OnBulletComplete()
        {
            //Debug.Log("OnBulletComplete");
        }

        //private void OnUpdateClick(AchievementList achievement)
        //{
        //    if (achievement == AchievementList.Rapid_Clicker)
        //    {
        //        Debug.Log("Click to move OnUpdate Callback");
        //    }
        //}

        public static void AddCurrency(int amount)
        {
            currency += amount;
            OnCurrencyUpdate?.Invoke(currency);
        }
    }
}