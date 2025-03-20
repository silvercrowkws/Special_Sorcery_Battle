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
    /// 턴 매니저
    /// </summary>
    //TurnManager turnManager;

    private void Start()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        currentMoney = 999;
    }

    private void OnEnable()
    {        
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        //SceneManager.sceneLoaded -= OnSceneLoaded;
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
#if UNITY_EDITOR


#endif
}
