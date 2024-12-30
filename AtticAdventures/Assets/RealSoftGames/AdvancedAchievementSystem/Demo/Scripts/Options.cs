///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RealSoftGames.AdvancedAchievementSystem.Demo
{
    public class Options : MonoBehaviour
    {
        public GameObject options;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                options.gameObject.SetActive(!options.activeSelf);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene("Demo");
        }
    }
}