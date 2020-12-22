using DivinityPFA.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour, ISaveable
{
    [SerializeField] private List<Turret> turrets = new List<Turret>();

    public object CaptureState ()
    {
        var states = new SaveData[turrets.Count];

        for (int i = 0; i < turrets.Count; i++)
        {
            states[i] = new SaveData(i, turrets[i].gameObject.activeSelf);
        }

        return states;
    }

    public void RestoreState (object state)
    {
        var states = (SaveData[])state;

        for (int i = 0; i < turrets.Count; i++)
        {
            turrets[i].gameObject.SetActive(states[i].isActive);
        }
    }

    [Serializable]
    private struct SaveData
    {
        public int TurretIndex;
        public bool isActive;

        public SaveData(int index, bool activeState)
        {
            TurretIndex = index;
            isActive = activeState;
        }
    }
}
