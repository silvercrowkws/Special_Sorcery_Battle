using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 게임상태
/// </summary>
public enum GameState
{
    Main = 0,                   // 기본 상태
    GameStart,                  // 게임 시작 선택 상태
    PlayerDie,                  // 플레이어 사망 상태
    GameEnd,                    // 게임이 완료된 상태
}

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 현재 게임상태
    /// </summary>
    public GameState gameState = GameState.Main;

    /// <summary>
    /// 현재 게임상태 변경시 알리는 프로퍼티
    /// </summary>
    public GameState GameState
    {
        get => gameState;
        set
        {
            if (gameState != value)
            {
                gameState = value;
                switch (gameState)
                {
                    case GameState.Main:
                        Debug.Log("메인 상태");
                        break;
                    case GameState.GameStart:
                        Debug.Log("게임 시작 상태");
                        onGameStart?.Invoke();
                        break;
                    case GameState.PlayerDie:
                        Debug.Log("플레이어 사망 상태");
                        onPlayerDie?.Invoke();
                        break;
                    case GameState.GameEnd:
                        Debug.Log("게임 완료 상태");
                        onGameEnd?.Invoke();
                        break;
                }
            }
        }
    }


    // 게임상태 델리게이트
    public Action onGameStart;
    public Action onPlayerDie;
    public Action onGameEnd;

    /// <summary>
    /// 플레이어
    /// </summary>
    Player player;

    public Player Player
    {
        get
        {
            if (player == null)
                player = FindAnyObjectByType<Player>();
            return player;
        }
    }

    /// <summary>
    /// 현재 가지고 있는 돈
    /// </summary>
    int currentMoney;

    /// <summary>
    /// 돈 프로퍼티
    /// </summary>
    public int Money
    {
        get => currentMoney;
        set
        {
            if (currentMoney != value)
            {
                //currentMoney = value;
                currentMoney = Mathf.Clamp(value, 0, 999);
                Debug.Log($"남은 돈 : {currentMoney}");
                moneyChange?.Invoke(currentMoney);
            }
        }
    }

    /// <summary>
    /// 돈이 변경되었음을 알리는 델리게이트(UI 수정용)
    /// </summary>
    public Action<int> moneyChange;

    /// <summary>
    /// 현재 가지고 있는 소울
    /// </summary>
    int currentSoul;

    /// <summary>
    /// 소울 프로퍼티
    /// </summary>
    public int Soul
    {
        get => currentSoul;
        set
        {
            if (currentSoul != value)
            {
                //currentSoul = value;
                currentSoul = Mathf.Clamp(value, 0, 999);
                Debug.Log($"남은 소울 : {currentSoul}");
                soulChange?.Invoke(currentSoul);
            }
        }
    }

    /// <summary>
    /// 소울이 변경되었음을 알리는 델리게이트(UI 수정용)
    /// </summary>
    public Action<int> soulChange;

    /// <summary>
    /// 게임 종료 패널
    /// </summary>
    GameObject quitPanel;

    /// <summary>
    /// 게임 종료 버튼
    /// </summary>
    Button quitButton;

    /// <summary>
    /// 인풋 시스템
    /// </summary>
    GameControlActions gameControlActions;

    /// <summary>
    /// 턴 매니저
    /// </summary>
    //TurnManager turnManager;

    private void Start()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        currentMoney = 999;

        quitButton.onClick.AddListener(OnQuit);
        quitPanel.SetActive(false);
    }

    private void OnEnable()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        gameControlActions = new GameControlActions();

        Transform child = transform.GetChild(0);
        quitPanel = child.GetChild(0).gameObject;
        quitButton = quitPanel.GetComponentInChildren<Button>();

        gameControlActions.Controls.Enable();
        gameControlActions.Controls.ESC.performed += OnESC;
    }

    private void OnDisable()
    {
        //SceneManager.sceneLoaded -= OnSceneLoaded;
        if (gameControlActions != null)
        {
            gameControlActions.Controls.ESC.performed -= OnESC;
            gameControlActions.Controls.Disable();
        }
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        player = FindAnyObjectByType<Player>();        

        //turnManager = FindAnyObjectByType<TurnManager>();
        //turnManager.OnInitialize2();
    }

    private void Update()
    {

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        switch(scene.buildIndex)
        {
            case 0:
                Debug.Log("메인 씬");
                gameState = GameState.Main;

                break;
            case 1:
                Debug.Log("게임 시작 씬");
                gameState = GameState.GameStart;

                break;
            case 2:
                Debug.Log("플레이어 사망 씬");
                gameState = GameState.PlayerDie;

                break;
            case 3:
                Debug.Log("전투 완료 씬");
                gameState = GameState.GameEnd;

                break;
        }
    }

    /// <summary>
    /// ESC 키가 눌러졌을 때 패널을 활성화/비활성화 하는 함수
    /// </summary>
    /// <param name="context"></param>
    private void OnESC(InputAction.CallbackContext context)
    {
        //quitPanel.SetActive(true);
        quitPanel.SetActive(!quitPanel.activeSelf);

        if (quitPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    /// <summary>
    /// 게임 종료 버튼으로 게임을 종료시키는 함수
    /// </summary>
    private void OnQuit()
    {
        Application.Quit();

        // 에디터에서 실행 시 종료 테스트 (에디터에서는 실제로 종료되지 않음)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
#if UNITY_EDITOR


#endif
}
