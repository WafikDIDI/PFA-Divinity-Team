using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{

    public float timeLoadScene = 0;
    public int loadSceneId;
    public bool isIntroScene=false;

    public void LoadScene () => SceneManager.LoadScene(loadSceneId);

    public void LoadScene (int index) => SceneManager.LoadScene(index);

    public void OnExitButton () => Application.Quit();
}
