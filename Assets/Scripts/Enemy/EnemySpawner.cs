using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// 생성된 몬스터의 숫자
    /// </summary>
    int monsterCount = 0;

    /// <summary>
    /// 최대 몬스터 숫자
    /// </summary>
    public int maxMonsterCount = 10;

    /// <summary>
    /// 턴 매니저
    /// </summary>
    TurnManager turnManager;

    // 몬스터 프리팹
    public Monster1 monster1;
    public Monster2 monster2;
    public BossMonster1 bossMonster1;
    public BossMonster2 bossMonster2;

    // 현상금 몬스터 프리팹
    public Bounty_01 bounty_01;
    public Bounty_02 bounty_02;
    public Bounty_03 bounty_03;
    public Bounty_04 bounty_04;
    public Bounty_05 bounty_05;

    /// <summary>
    /// 생성된 몬스터가 쌓일 장소
    /// </summary>
    GameObject monsterRepository;

    /// <summary>
    /// 몬스터 스폰 간격
    /// </summary>
    float spawnDelay = 0;

    private void Start()
    {

        monsterRepository = GameObject.Find("MonsterRepository");
        if (monsterRepository == null)
        {
            Debug.LogError("MonsterRepository를 찾을 수 없음");
        }
        else
        {
            DontDestroyOnLoad(monsterRepository);
            Debug.Log("MonsterRepository 초기화됨");
        }

        //DontDestroyOnLoad(monsterRepository);
        turnManager = FindAnyObjectByType<TurnManager>();
        if (turnManager != null)
        {
            turnManager.onTurnStart += OnTurnStartFC;
            Debug.Log("onTurnStart 이벤트 구독됨");
        }
        else
        {
            Debug.LogError("TurnManager를 찾을 수 없음");
        }

        //turnManager.onTurnStart += OnTurnStartFC;
    }

    private void OnDisable()
    {
        if (turnManager != null)
        {
            turnManager.onTurnStart -= OnTurnStartFC;
            Debug.Log("onTurnStart 이벤트 구독 해제됨");
        }
    }

    /// <summary>
    /// 현상금 몬스터를 스폰시키는 함수
    /// </summary>
    /// <param name="bountyIndex">현상금 번호</param>
    public void BountyEnemySpawn(int bountyIndex)
    {
        switch(bountyIndex)
        {
            case 0:
                Bounty_01 bounty1 = Instantiate(bounty_01, transform.position, Quaternion.identity);
                bounty1.name = $"Bounty_01";
                bounty1.transform.parent = transform;
                break;
            case 1:
                Bounty_02 bounty2 = Instantiate(bounty_02, transform.position, Quaternion.identity);
                bounty2.name = $"Bounty_02";
                bounty2.transform.parent = transform;
                break;
            case 2:
                Bounty_03 bounty3 = Instantiate(bounty_03, transform.position, Quaternion.identity);
                bounty3.name = $"Bounty_03";
                bounty3.transform.parent = transform;
                break;
            case 3:
                Bounty_04 bounty4 = Instantiate(bounty_04, transform.position, Quaternion.identity);
                bounty4.name = $"Bounty_04";
                bounty4.transform.parent = transform;
                break;
            case 4:
                Bounty_05 bounty5 = Instantiate(bounty_05, transform.position, Quaternion.identity);
                bounty5.name = $"Bounty_05";
                bounty5.transform.parent = transform;
                break;
        }
    }

    /// <summary>
    /// 몬스터를 스폰시키는 함수
    /// </summary>
    void SpawnerEnemy()
    {
        //monsterCount++;
        //Debug.Log($"{monsterCount} 마리째 몬스터 스폰");

        int cycle = (turnManager.turnNumber - 1) / 10;                  // 몬스터 주기 (0: 1-10턴, 1: 11-20턴)
        int turnInCycle = (turnManager.turnNumber - 1) % 10 + 1;        // 현재 주기 내의 턴 번호 (1-10)

        switch (turnInCycle)
        {
            case 1:
            case 2:
                if (cycle == 0)          // 1사이클이면
                {
                    // 몬스터1 소환
                    spawnDelay = 0.5f;
                    Monster1 m1 = Instantiate(monster1, transform.position, Quaternion.identity);
                    m1.name = $"Monster1_{monsterCount}";
                    m1.transform.parent = transform;
                    monsterCount++;
                }
                else if (cycle == 1)     // 2사이클이면
                {
                    // 몬스터2 소환
                    spawnDelay = 0.5f;
                    Monster2 m2 = Instantiate(monster2, transform.position, Quaternion.identity);
                    m2.name = $"Monster2_{monsterCount}";
                    m2.transform.parent = transform;
                    monsterCount++;
                }
                break;
            case 3:
            case 4:
                if (cycle == 0)          // 1사이클이면
                {
                    // 몬스터1 소환
                    spawnDelay = 0.5f;
                    Monster1 m1 = Instantiate(monster1, transform.position, Quaternion.identity);
                    m1.name = $"Monster1_{monsterCount}";
                    m1.transform.parent = transform;
                    monsterCount++;
                }
                else if (cycle == 1)     // 2사이클이면
                {
                    // 몬스터2 소환
                    spawnDelay = 0.5f;
                    Monster2 m2 = Instantiate(monster2, transform.position, Quaternion.identity);
                    m2.name = $"Monster2_{monsterCount}";
                    m2.transform.parent = transform;
                    monsterCount++;
                }
                break;
            case 5:
            case 6:
                if (cycle == 0)          // 1사이클이면
                {
                    // 몬스터1 소환
                    spawnDelay = 0.5f;
                    Monster1 m1 = Instantiate(monster1, transform.position, Quaternion.identity);
                    m1.name = $"Monster1_{monsterCount}";
                    m1.transform.parent = transform;
                    monsterCount++;
                }
                else if (cycle == 1)     // 2사이클이면
                {
                    // 몬스터2 소환
                    spawnDelay = 0.5f;
                    Monster2 m2 = Instantiate(monster2, transform.position, Quaternion.identity);
                    m2.name = $"Monster2_{monsterCount}";
                    m2.transform.parent = transform;
                    monsterCount++;
                }
                break;
            case 7:
                if (cycle == 0)          // 1사이클이면
                {
                    // 몬스터1 소환
                    spawnDelay = 0.5f;
                    Monster1 m1 = Instantiate(monster1, transform.position, Quaternion.identity);
                    m1.name = $"Monster1_{monsterCount}";
                    m1.transform.parent = transform;
                    monsterCount++;
                }
                else if (cycle == 1)     // 2사이클이면
                {
                    // 몬스터2 소환
                    spawnDelay = 0.5f;
                    Monster2 m2 = Instantiate(monster2, transform.position, Quaternion.identity);
                    m2.name = $"Monster2_{monsterCount}";
                    m2.transform.parent = transform;
                    monsterCount++;
                }
                break;
            case 8:
                if (cycle == 0)          // 1사이클이면
                {
                    // 몬스터1 소환
                    spawnDelay = 0.5f;
                    Monster1 m1 = Instantiate(monster1, transform.position, Quaternion.identity);
                    m1.name = $"Monster1_{monsterCount}";
                    m1.transform.parent = transform;
                    monsterCount++;
                }
                else if (cycle == 1)     // 2사이클이면
                {
                    // 몬스터2 소환
                    spawnDelay = 0.5f;
                    Monster2 m2 = Instantiate(monster2, transform.position, Quaternion.identity);
                    m2.name = $"Monster2_{monsterCount}";
                    m2.transform.parent = transform;
                    monsterCount++;
                }
                break;
            case 9:
                if (cycle == 0)          // 1사이클이면
                {
                    // 몬스터1 소환
                    spawnDelay = 0.5f;
                    Monster1 m1 = Instantiate(monster1, transform.position, Quaternion.identity);
                    m1.name = $"Monster1_{monsterCount}";
                    m1.transform.parent = transform;
                    monsterCount++;
                }
                else if (cycle == 1)     // 2사이클이면
                {
                    // 몬스터2 소환
                    spawnDelay = 0.5f;
                    Monster2 m2 = Instantiate(monster2, transform.position, Quaternion.identity);
                    m2.name = $"Monster2_{monsterCount}";
                    m2.transform.parent = transform;
                    monsterCount++;
                }
                break;
            case 10:
                if (cycle == 0)
                {
                    spawnDelay = 1.0f;
                    BossMonster1 b1 = Instantiate(bossMonster1, transform.position, Quaternion.identity);
                    b1.name = $"BossMonster1_{monsterCount}";
                    b1.transform.parent = transform;
                    monsterCount = 10;
                }
                else if (cycle == 1)
                {
                    spawnDelay = 1.0f;
                    BossMonster2 b2 = Instantiate(bossMonster2, transform.position, Quaternion.identity);
                    b2.name = $"BossMonster2_{monsterCount}";
                    b2.transform.parent = transform;
                    monsterCount = 10;
                }
                break;
        }
    }

    /// <summary>
    /// 턴이 시작되었을 때 코루틴을 시작시키는 함수
    /// </summary>
    /// <param name="_"></param>
    private void OnTurnStartFC(int _)
    {
        Debug.Log("OnTurnStartFC 실행");
        StartCoroutine(SpawnerEnemyCoroutine(3.0f));
    }

    /// <summary>
    /// 몬스터를 스폰시키는 코루틴
    /// </summary>
    /// <param name="delay">턴 시작 후 몬스터가 스폰될 때까지 대기하는 시간</param>
    /// <returns></returns>
    IEnumerator SpawnerEnemyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);             // delay 만큼 기다리고
        while (monsterCount < maxMonsterCount)              // 10번 실행
        {
            SpawnerEnemy();                                 // 몬스터 스폰
            yield return new WaitForSeconds(spawnDelay);    // delay 만큼 기다리고
        }
        monsterCount = 0;                                   // 몬스터를 전부 생성한 후 초기화
    }

#if UNITY_EDITOR
    public void Test_SpawnEnemy()
    {
        StartCoroutine(SpawnerEnemyCoroutine(3.0f));
    }
#endif
}
