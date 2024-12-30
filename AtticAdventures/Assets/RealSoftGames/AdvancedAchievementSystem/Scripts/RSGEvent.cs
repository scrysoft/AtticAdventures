/*using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace RealSoftGames.AdvancedAchievementSystem
{
    [System.Serializable]
    public class RSGEvent : UnityEvent
    {
        private int listenerCount;
        public int ListenerCount { get { return listenerCount; } }

        public new void AddListener(UnityAction call)
        {
            Debug.Log("Add Listener");
            //base.AddListener(call);
            listenerCount++;
        }

        public new void RemoveListener(UnityAction call)
        {
            Debug.Log("Remove Listener");
            //base.RemoveListener(call);
            listenerCount--;
        }
    }
}*/