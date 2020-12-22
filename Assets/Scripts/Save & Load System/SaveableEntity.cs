using System;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityPFA.Systems
{
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string id = string.Empty;
        public string Id => id;

        [ContextMenu("Generate ID")]
        public void GenerateID () => id = Guid.NewGuid().ToString();

        private void Reset () => GenerateID();

        public object CaptureState ()
        {
            var state = new Dictionary<string, object>();
            var saveables = GetComponents<ISaveable>();

            for (int i = 0; i < saveables.Length; i++)
            {
                state[saveables[i].GetType().ToString()] = saveables[i].CaptureState();
            }

            return state;
        }

        public void RestoreState (object state)
        {
            var stateDictionary = (Dictionary<string, object>)state;

            foreach (var saveable in GetComponents<ISaveable>())
            {
                string typeName = saveable.GetType().ToString();

                if (stateDictionary.TryGetValue(typeName, out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
}