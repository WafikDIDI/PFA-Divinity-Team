﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DivinityPFA.Systems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveable
{
    #region PlayerDeath

    [SerializeField] private Transform deathUITransfrom = null;

    private void TriggerDeathUI ()
    {
        deathUITransfrom.gameObject.SetActive(true);
        isPlayerAbleToMove = false;
    }

    public void OnDeathRestartButton ()
    {
        Time.timeScale = 1f;
        isPlayerAbleToMove = true;
        playerController.SetTransform(checkPoints[checkPointIndex].position);
        print("ss");
        var playerHealthSystemRef = playerController.transform.GetComponent<PlayerHealth>();
        playerHealthSystemRef.healthSystem.Heal(1000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

    [HideInInspector] public bool isPlayerAbleToMove = true;
    public bool IsPlayerAbleToMove => isPlayerAbleToMove;

    [Space]
    [SerializeField] private PlayerController playerController = null;
    [Header("Save System")]
    [SerializeField] private SaveLoadSystem saveSystemReference = null; 

    [Header("CheckPoints Settings")]
    [SerializeField] private List<Transform> checkPoints = new List<Transform>();
    [SerializeField] private int checkPointIndex;
    private Vector3 startingPosition;

    public static GameManager instance;
    public static Action<int,int> OnAmmoChangeUI;

    [Space]
    [SerializeField] private List<Texture2D> cursorList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } 
        else if (instance != this)
        {
            Destroy(this);
        }

    }

    private void Start ()
    {
        PositionPlayerAtCheckPoint();
    }

    // CheckPoints Related 
    private void PositionPlayerAtCheckPoint ()
    {
        var checkPointPosition = checkPoints[checkPointIndex].position;

        var spawnPoint = new Vector3(
            checkPointPosition.x,
            playerController.transform.position.y,
            checkPointPosition.z
            );

        playerController.transform.position = checkPoints[checkPointIndex].position;
    }

    public void CheckPointPicked (Transform checkPointPickedTransform)
    {
        for (int i = 0; i < checkPoints.Count; i++)
        {
            if (checkPointPickedTransform == checkPoints[i])
            {
                checkPointIndex = i;
            }
        }

        saveSystemReference.Save();
    }

    // Game Logic

    #region Espace_Menu

    [SerializeField] private KeyCode pauseMenuToggleKey = KeyCode.Escape;
    [SerializeField] private GameObject pauseMenuUI = null;

    public void PauseMenu ()
    {
        if (Input.GetKeyDown(pauseMenuToggleKey))
        {
            PauseOrUnpause();
        }
    }

    public void PauseOrUnpause ()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        Time.timeScale = pauseMenuUI.activeSelf ? 0f : 1f;
        isPlayerAbleToMove = !pauseMenuUI.activeSelf;
    }

    #endregion

    private void Update ()
    {
        PauseMenu();
    }

    public void ChangeCursor(int value)
    {
        Cursor.SetCursor(cursorList[value], new Vector2(cursorList[value].width / 2, cursorList[value].height / 2), CursorMode.Auto);
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(cursorList[0], Vector2.zero, CursorMode.Auto);
        
    }

    public void BulletResiver(int value,int value2)
    {
        OnAmmoChangeUI?.Invoke(value,value2);
    }

    public void GameOver () {
        Time.timeScale = 0f;
        TriggerDeathUI();
    }

    // ISaveable Interface
    public object CaptureState ()
    {
        return new SavaData
        {
            checkPointIndex = this.checkPointIndex
        };
    }

    public void RestoreState (object state)
    {
        var data = (SavaData)state;

        this.checkPointIndex = data.checkPointIndex;
    }

    [Serializable]
    private struct SavaData
    {
        public int checkPointIndex;
    }

}
