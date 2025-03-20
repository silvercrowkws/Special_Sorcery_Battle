using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ButtonStateImage : MonoBehaviour
{
    Button[] bountysButtons;

    Button[] animalsButtons;

    Button[] upgradeButtons;

    Button[] miningButtons;

    /// <summary>
    /// SorceryButtons 클래스
    /// </summary>
    SorceryButtons sorceryButtons;

    public Tilemap animalTileMap;

    public TileBase animal_1_Tile;
    public TileBase animal_2_Tile;
    public TileBase animal_3_Tile;

    List<Vector3Int> animalSpawnPositions;

    /// <summary>
    /// 소환 확률이 강화된 정도
    /// </summary>
    public int sorceryUpgrade = 0;

    /// <summary>
    /// 게임 매니저
    /// </summary>
    GameManager gameManager;

    InfoUI infoUI;

    /// <summary>
    /// 몬스터 스포너
    /// </summary>
    EnemySpawner enemySpawner;

    private void Awake()
    {
        bountysButtons = new Button[5];
        animalsButtons = new Button[3];
        upgradeButtons = new Button[5];
        miningButtons = new Button[3];

        animalSpawnPositions = new List<Vector3Int>
        {
            new Vector3Int(6, 2, 0),
            new Vector3Int(6, 0, 0),
            new Vector3Int(6, -2, 0)
        };

        for (int i = 0; i < bountysButtons.Length; i++)
        {
            int index = i;
            bountysButtons[index] = transform.GetChild(0).GetChild(index).GetComponent<Button>();
            bountysButtons[index].onClick.AddListener(() => BountyInstantiate(index));
        }

        for(int i = 0; i < animalsButtons.Length; i++)
        {
            int index = i;
            animalsButtons[index] = transform.GetChild(1).GetChild(index).GetComponent<Button>();
            animalsButtons[index].onClick.AddListener(() => AnimalInstantiate(index));
        }

        for(int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = i;
            upgradeButtons[index] = transform.GetChild(2).GetChild(index).GetComponent<Button>();
            upgradeButtons[index].onClick.AddListener(() => Upgrade(index));
        }

        for (int i = 0; i < miningButtons.Length; i++)
        {
            int index = i;
            miningButtons[index] = transform.GetChild(3).GetChild(index).GetComponent<Button>();
            miningButtons[index].onClick.AddListener(() => Mining(index));
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        sorceryButtons = FindAnyObjectByType<SorceryButtons>();
        infoUI = FindAnyObjectByType<InfoUI>();
        GameObject spawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        enemySpawner = spawner.GetComponent<EnemySpawner>();
    }

    /// <summary>
    /// 신수를 소환하는 함수
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private void AnimalInstantiate(int index)
    {
        switch(index)
        {
            case 0:
                // 일반 주술이 3종류 다 있으면
                if (sorceryButtons.stoneSorcery[0] > 0 && sorceryButtons.fireSorcery[0] > 0 && sorceryButtons.waterSorcery[0] > 0)
                {
                    animalTileMap.SetTile(animalSpawnPositions[0], animal_1_Tile);
                    animalSpawnPositions.RemoveAt(0);
                    animalsButtons[index].gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("주술이 모자르다");
                }
                break;
            case 1:
                // 희귀 주술이 3종류 다 있으면
                if (sorceryButtons.stoneSorcery[1] > 0 && sorceryButtons.fireSorcery[1] > 0 && sorceryButtons.waterSorcery[1] > 0)
                {
                    animalTileMap.SetTile(animalSpawnPositions[0], animal_2_Tile);
                    animalSpawnPositions.RemoveAt(0);
                    animalsButtons[index].gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("주술이 모자르다"); 
                }
                break;
            case 2:
                // 영웅 주술이 3종류 다 있으면
                if (sorceryButtons.stoneSorcery[2] > 0 && sorceryButtons.fireSorcery[2] > 0 && sorceryButtons.waterSorcery[2] > 0)
                {
                    animalTileMap.SetTile(animalSpawnPositions[0], animal_3_Tile);
                    animalSpawnPositions.RemoveAt(0);
                    animalsButtons[index].gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("주술이 모자르다");
                }
                break;
        }
    }
    
    /// <summary>
    /// 소환 확률, 주술, 신수 등을 강화하는 함수
    /// </summary>
    /// <param name="index"></param>
    private void Upgrade(int index)
    {
        switch (index)
        {
            // 소환 확률 강화(일단 강화는 0 1 2 까지만)
            case 0:
                if(gameManager.Money > 74 && sorceryUpgrade < 2)
                {
                    gameManager.Money -= 75;
                    sorceryUpgrade++;
                }
                else
                {
                    Debug.Log("돈이 모자르거나 강화 횟수 최대");
                }
                break;

            // 바위 주술 강화
            case 1:
                // 소울 차감
                if(gameManager.Soul > 0)
                {
                    gameManager.Soul--;
                    // 바위 주술 데미지 강화 부분
                }
                else
                {
                    Debug.Log($"소울이 모자르다 : {gameManager.Soul}");
                }
                break;

            // 불 주술 강화
            case 2:                
                if (gameManager.Soul > 0)
                {
                    gameManager.Soul--;
                    // 불 주술 데미지 강화 부분
                }
                else
                {
                    Debug.Log($"소울이 모자르다 : {gameManager.Soul}");
                }
                break;

            // 물 주술 강화
            case 3:
                if (gameManager.Soul > 0)
                {
                    gameManager.Soul--;
                    // 물 주술 데미지 강화 부분
                }
                else
                {
                    Debug.Log($"소울이 모자르다 : {gameManager.Soul}");
                }
                break;

            // 신수 강화
            case 4:
                if (gameManager.Soul > 0)
                {
                    gameManager.Soul--;
                    // 신수 공격력 강화 부분
                }
                else
                {
                    Debug.Log($"소울이 모자르다 : {gameManager.Soul}");
                }
                break;

        }
    }

    /// <summary>
    /// 채굴 함수
    /// </summary>
    /// <param name="index"></param>
    private void Mining(int index)
    {
        int random = UnityEngine.Random.Range(0, 3);
        switch(index)
        {
            case 0:
                // 소울 1개 소모해서 영웅 등급
                if(gameManager.Soul > 0)
                {
                    gameManager.Soul -= 1;

                    switch (random)
                    {
                        // 바위 주술 채굴
                        case 0:
                            sorceryButtons.stoneSorcery[2]++;
                            break;
                        // 불 주술 채굴
                        case 1:
                            sorceryButtons.fireSorcery[2]++;
                            break;
                        // 물 주술 채굴
                        case 2:
                            sorceryButtons.waterSorcery[2]++;
                            break;
                    }
                    sorceryButtons.UpdateSorceryCount();        // 주술 갱신 함수
                }
                else
                {
                    Debug.Log($"소울이 모자르다 : {gameManager.Soul}");
                }
                break;
            case 1:
                // 소울 3개 소모해서 전설 등급
                if (gameManager.Soul > 2)
                {
                    gameManager.Soul -= 2;

                    switch (random)
                    {
                        // 바위 주술 채굴
                        case 0:
                            sorceryButtons.stoneSorcery[3]++;
                            break;
                        // 불 주술 채굴
                        case 1:
                            sorceryButtons.fireSorcery[3]++;
                            break;
                        // 물 주술 채굴
                        case 2:
                            sorceryButtons.waterSorcery[3]++;
                            break;
                    }
                    sorceryButtons.UpdateSorceryCount();        // 주술 갱신 함수
                }
                else
                {
                    Debug.Log($"소울이 모자르다 : {gameManager.Soul}");
                }
                break;
            case 2:
                // 소울 7개 소모해서 선조 등급
                if (gameManager.Soul > 6)
                {
                    gameManager.Soul -= 7;

                    switch (random)
                    {
                        // 바위 주술 채굴
                        case 0:
                            sorceryButtons.stoneSorcery[4]++;
                            break;
                        // 불 주술 채굴
                        case 1:
                            sorceryButtons.fireSorcery[4]++;
                            break;
                        // 물 주술 채굴
                        case 2:
                            sorceryButtons.waterSorcery[4]++;
                            break;
                    }
                    sorceryButtons.UpdateSorceryCount();        // 주술 갱신 함수
                }
                else
                {
                    Debug.Log($"소울이 모자르다 : {gameManager.Soul}");
                }
                break;
        }
    }

    /// <summary>
    /// 현상금을 스폰하는 함수
    /// </summary>
    /// <param name="index"></param>
    private void BountyInstantiate(int index)
    {
        if (infoUI.bounty)
        {
            enemySpawner.BountyEnemySpawn(index);
            infoUI.ObBountyFC();
        }
        else
        {
            Debug.Log("아직 현상금을 못찾았다!");
        }
    }
}
