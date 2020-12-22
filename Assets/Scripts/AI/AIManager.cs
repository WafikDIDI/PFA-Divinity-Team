using DivinityPFA.Systems;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveableEntity))]
public class AIManager : MonoBehaviour, ISaveable {

    public List<Enemy> Enemies = new List<Enemy>();

    public object CaptureState ()
    {
        SaveData[] saveDatas = new SaveData[Enemies.Count];

        for (int i = 0; i < Enemies.Count; i++)
        {
            saveDatas[i] = new SaveData(i, Enemies[i].gameObject.activeSelf);
        }

        return saveDatas;
    }

    public void RestoreState (object state)
    {
        var saveDatas = (SaveData[])state;

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].gameObject.SetActive(saveDatas[i].isActive);
        }
    }

    [Serializable]
    private struct SaveData
    {
        public int EnemyIndex;
        public bool isActive;

        public SaveData(int index, bool state)
        {
            EnemyIndex = index;
            isActive = state;
        }
    }
}
