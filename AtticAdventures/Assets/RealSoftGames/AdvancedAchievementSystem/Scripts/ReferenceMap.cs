using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames.AdvancedAchievementSystem
{
    [System.Serializable]
    public class ReferenceMap : MonoBehaviour
    {
        [SerializeField] private string referenceMapName;
        [SerializeField] private List<Map> maps = new List<Map>();

        public string ReferenceMapName
        {
            get => referenceMapName;
            set => referenceMapName = value;
        }

        public List<Map> Maps
        {
            get => maps;
            set => maps = value;
        }

        public T GetType<T>(MapType mapType) where T : Component
        {
            var comp = Maps.Find(i => i.MapType == mapType)?.GetValue<T>();
            if (comp == null)
                Debug.LogWarning($"Component of type {typeof(T)} not found for MapType {mapType}");

            return comp;
        }

        // New method to access the component's game object and get another component from it
        public T GetComponentFromReferenceMapComponent<T>(MapType mapType) where T : Component
        {
            var map = Maps.Find(i => i.MapType == mapType);
            if (map != null && map.Component != null)
            {
                return map.Component.gameObject.GetComponent<T>();
            }
            return null;
        }
    }
}
