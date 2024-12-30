using System;
using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> list = new List<SerializableKeyValuePair<TKey, TValue>>();

        // Use a non-serialized dictionary to provide fast lookup capabilities that won't be directly serialized by Unity.
        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public void Add(TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                list.Find(i => EqualityComparer<TKey>.Default.Equals(i.key, key)).value = value;
                dictionary[key] = value;
            }
            else
            {
                list.Add(new SerializableKeyValuePair<TKey, TValue>(key, value));
                dictionary[key] = value;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public void Clear()
        {
            dictionary.Clear();
            list.Clear();
        }

        // Implement ISerializationCallbackReceiver
        public void OnBeforeSerialize()
        { }

        public void OnAfterDeserialize()
        {
            dictionary.Clear();
            foreach (var pair in list)
            {
                dictionary[pair.key] = pair.value;
            }
        }
    }
}