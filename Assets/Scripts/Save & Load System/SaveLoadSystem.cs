﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace DivinityPFA.Systems
{
    public class SaveLoadSystem : MonoBehaviour
    {
        [SerializeField] private Button continueButton = null;

        private string savePath => Application.persistentDataPath + "/save.txt";

        private void Awake () => Load();

        private void Start ()
        {
            if(continueButton != null)
            {
                continueButton.interactable = File.Exists(savePath);
            }
        }

        [ContextMenu("Save")]
        public void Save ()
        {
            var state = LoadFile();
            CaptureState(state);
            SaveFile(state);
        }

        [ContextMenu("Delete Save")]
        public void DeleteSave ()
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                continueButton.interactable = File.Exists(savePath);
            }
        }

        public void Load ()
        {
            var state = LoadFile();
            RestoreState(state);
        }

        private Dictionary<string, object> LoadFile ()
        {
            if (!File.Exists(savePath))
            {
                return new Dictionary<string, object>();
            }

            using(var stream = File.Open(savePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void SaveFile (object state)
        {
            using (var stream = File.Open(savePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private void CaptureState (Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.Id] = saveable.CaptureState();
            }
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                if(state.TryGetValue(saveable.Id, out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
}