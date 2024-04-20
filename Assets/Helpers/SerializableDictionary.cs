using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers {
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver {
        [SerializeField] private List<Pair<TKey, TValue>> keys = new();


        public void OnBeforeSerialize() {
            keys.Clear();
            foreach (var pair in this) {
                keys.Add(new Pair<TKey, TValue>(pair.Key, pair.Value));
            }
        }

        public void OnAfterDeserialize() {
            Clear();

            foreach (var t in keys) Add(t.key, t.value);
        }
    }

    [Serializable]
    internal class Pair<TKey, TValue> {
        [SerializeField] public TKey key;
        [SerializeField] public TValue value;

        public Pair(TKey key, TValue value) {
            this.key = key;
            this.value = value;
        }
    }
}
