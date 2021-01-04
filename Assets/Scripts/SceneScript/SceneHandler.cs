using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{

    public float timeLoadScene = 0;
    public int loadSceneId;
    public bool isIntroScene=false;



    public void OnLoadScene()
    {
        SceneManager.LoadScene(loadSceneId);
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(timeLoadScene);
        SceneManager.LoadScene(loadSceneId);
    }

    private void Update()
    {
        if (isIntroScene)
        {
            StartCoroutine(LoadScene());
        }
    }
}
