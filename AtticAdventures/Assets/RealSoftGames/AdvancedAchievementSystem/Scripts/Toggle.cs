///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames.AdvancedAchievementSystem
{
    public class Toggle : MonoBehaviour
    {
        public GameObject parent;

        public void ToggleObject()
        {
            parent.SetActive(false);
        }
    }
}