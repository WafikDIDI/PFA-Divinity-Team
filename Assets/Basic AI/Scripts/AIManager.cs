using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {
    public static AIManager Instance { get; private set; }
    public List<Enemy> Enemies = new List<Enemy>();

    private void Awake () {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(this);
        }

        Enemies = new List<Enemy>();
    }

}
