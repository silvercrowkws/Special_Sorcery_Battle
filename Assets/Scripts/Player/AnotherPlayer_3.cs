using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AnotherPlayer_3 : Player
{
    Factory3 factory3;

    public int[] stoneSorcery;
    public int[] fireSorcery;
    public int[] waterSorcery;

    public Tilemap animalTileMap;

    public TileBase animal_1_Tile;
    public TileBase animal_2_Tile;
    public TileBase animal_3_Tile;

    List<Vector3Int> animalSpawnPositions;

    /// <summary>
    /// 주술 개수
    /// </summary>
    public int sorceryCount;

    /// <summary>
    /// 최대 주술 개수
    /// </summary>
    public int maxSorceryCount = 25;

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
                Debug.Log($"다른 플레이어 남은 돈 : {currentMoney}");
            }
        }
    }

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
                Debug.Log($"다른 플레이어 남은 소울 : {currentSoul}");
            }
        }
    }

    /// <summary>
    /// 주술 생성 가격
    /// </summary>
    int sorceryCost = 0;

    /// <summary>
    /// 소환 확률이 강화된 정도
    /// </summary>
    public int sorceryUpgrade = 0;

    /// <summary>
    /// 현상금이 가능한지 알리는 bool 변수(true : 가능, false : 불가능)
    /// </summary>
    public bool bounty = false;

    EnemySpawner enemySpawner;

    protected override void Awake()
    {
        factory3 = Factory3.Instance;
        stoneSorcery = new int[5];
        fireSorcery = new int[5];
        waterSorcery = new int[5];
        sorceryCost = 20;

        animalSpawnPositions = new List<Vector3Int>
        {
            new Vector3Int(-3, 2, 0),
            new Vector3Int(-3, 0, 0),
            new Vector3Int(-3, -2, 0)
        };

        base.Awake();
    }

    protected override void Start()
    {
        currentMoney = 500;
        GameObject spawner = GameObject.FindGameObjectWithTag("EnemySpawner_3");
        enemySpawner = spawner.GetComponent<EnemySpawner>();

        StartCoroutine(BountyCountDown());
        base.Start();
    }

    protected override void Update()
    {
        // 현재 소지금이 주술 생성비용보다 많으면
        if(Money >= sorceryCost)
        {
            OnSorceryButton();
        }

        AutoAnimalInstantiate();        // 신수 소환 가능하면 소환

        AutoMining();                   // 채굴 가능하면 채굴

        AutoBountyInstantiate();        // 현상금이 가능하면 현상금

        AutoUpgrade();                  // 강화 가능하면 강화

        // 바위 공격
        // 각 stoneSorcery 배열을 확인하고, 쿨다운이 끝난 공격이 있다면 실행
        for (int i = 0; i < stoneSorcery.Length; i++)
        {
            if (stoneSorcery[i] > 0 && stoneCooldownTimers[i] <= 0f)
            {
                // 해당 공격을 실행
                ExecuteAttack2(0, i);

                // 쿨다운 타이머 초기화
                stoneCooldownTimers[i] = cooldownDurations[i];
            }
        }

        // 쿨다운 타이머 업데이트
        for (int i = 0; i < stoneCooldownTimers.Length; i++)
        {
            if (stoneCooldownTimers[i] > 0f)
            {
                stoneCooldownTimers[i] -= Time.deltaTime;  // 타이머 감소
            }
        }

        // 불 공격
        // 각 fireSorcery 배열을 확인하고, 쿨다운이 끝난 공격이 있다면 실행
        for (int i = 0; i < fireSorcery.Length; i++)
        {
            if (fireSorcery[i] > 0 && fireCooldownTimers[i] <= 0f)
            {
                // 해당 공격을 실행
                ExecuteAttack2(1, i);

                // 쿨다운 타이머 초기화
                fireCooldownTimers[i] = cooldownDurations[i];
            }
        }

        // 쿨다운 타이머 업데이트
        for (int i = 0; i < fireCooldownTimers.Length; i++)
        {
            if (fireCooldownTimers[i] > 0f)
            {
                fireCooldownTimers[i] -= Time.deltaTime;  // 타이머 감소
            }
        }

        // 물 공격
        // 각 waterSorcery 배열을 확인하고, 쿨다운이 끝난 공격이 있다면 실행
        for (int i = 0; i < waterSorcery.Length; i++)
        {
            if (waterSorcery[i] > 0 && waterCooldownTimers[i] <= 0f)
            {
                // 해당 공격을 실행
                ExecuteAttack2(2, i);

                // 쿨다운 타이머 초기화
                waterCooldownTimers[i] = cooldownDurations[i];
            }
        }

        // 쿨다운 타이머 업데이트
        for (int i = 0; i < waterCooldownTimers.Length; i++)
        {
            if (waterCooldownTimers[i] > 0f)
            {
                waterCooldownTimers[i] -= Time.deltaTime;  // 타이머 감소
            }
        }

        AutoSynthesis();
    }

    /// <summary>
    /// 주술 생성 버튼
    /// 확률
    /// - 일반 84.98	/ 78.535    / 72.95
    /// - 희귀 11.32	/ 15.51	    / 18.75
    /// - 영웅 3		/ 4.6	    / 6.12
    /// - 전설 0.5	    / 1	        / 1.6
    /// - 선조 0.2	    / 0.35	    / 0.55
    /// - 천벌 0		/ 0.005	    / 0.03
    /// </summary>
    private void OnSorceryButton()
    {
        Debug.Log("주술 생성 버튼 클릭");

        // 소지금이 생성 비용보다 크고, 주술 개수가 최대 25개를 넘지 않으면
        if (Money >= sorceryCost && sorceryCount < maxSorceryCount)
        {
            Money -= sorceryCost;                           // 돈 차감
            sorceryCost++;                                              // 가격++

            float randomValue = UnityEngine.Random.Range(0f, 100f);     // 난수 생성

            // 주술 생성 부분(강화된 정도에 따라)
            switch (sorceryUpgrade)
            {
                // 기본 확률
                case 0:
                    if (randomValue < 84.98f)
                    {
                        Debug.Log("일반 (84.98%)");
                        OnSorceryNumber(0);
                    }
                    else if (randomValue < 84.98f + 11.32f)
                    {
                        Debug.Log("희귀 (11.32%)");
                        OnSorceryNumber(1);
                    }
                    else if (randomValue < 84.98f + 11.32f + 3f)
                    {
                        Debug.Log("영웅 (3%)");
                        OnSorceryNumber(2);
                    }
                    else if (randomValue < 84.98f + 11.32f + 3f + 0.5f)
                    {
                        Debug.Log("전설 (0.5%)");
                        OnSorceryNumber(3);
                    }
                    else if (randomValue < 84.98f + 11.32f + 3f + 0.5f + 0.2f)
                    {
                        Debug.Log("선조 (0.2%)");
                        OnSorceryNumber(4);
                    }
                    else
                    {
                        Debug.Log("천벌 (0%)");
                        OnSorceryNumber(5);
                    }
                    break;

                case 1:
                    if (randomValue < 78.535f)
                    {
                        Debug.Log("일반 (78.535%)");
                        OnSorceryNumber(0);
                    }
                    else if (randomValue < 78.535f + 15.51f)
                    {
                        Debug.Log("희귀 (15.51%)");
                        OnSorceryNumber(1);
                    }
                    else if (randomValue < 78.535f + 15.51f + 4.6f)
                    {
                        Debug.Log("영웅 (4.6%)");
                        OnSorceryNumber(2);
                    }
                    else if (randomValue < 78.535f + 15.51f + 4.6f + 1f)
                    {
                        Debug.Log("전설 (1%)");
                        OnSorceryNumber(3);
                    }
                    else if (randomValue < 78.535f + 15.51f + 4.6f + 1f + 0.35f)
                    {
                        Debug.Log("선조 (0.35%)");
                        OnSorceryNumber(4);
                    }
                    else
                    {
                        Debug.Log("천벌 (0.005%)");
                        OnSorceryNumber(5);
                    }
                    break;

                case 2:
                    if (randomValue < 72.95f)
                    {
                        Debug.Log("일반 (72.95%)");
                        OnSorceryNumber(0);
                    }
                    else if (randomValue < 72.95f + 18.75f)
                    {
                        Debug.Log("희귀 (18.75%)");
                        OnSorceryNumber(1);
                    }
                    else if (randomValue < 72.95f + 18.75f + 6.12f)
                    {
                        Debug.Log("영웅 (6.12%)");
                        OnSorceryNumber(2);
                    }
                    else if (randomValue < 72.95f + 18.75f + 6.12f + 1.6f)
                    {
                        Debug.Log("전설 (1.6%)");
                        OnSorceryNumber(3);
                    }
                    else if (randomValue < 72.95f + 18.75f + 6.12f + 1.6f + 0.55f)
                    {
                        Debug.Log("선조 (0.55%)");
                        OnSorceryNumber(4);
                    }
                    else
                    {
                        Debug.Log("천벌 (0.03%)");
                        OnSorceryNumber(5);
                    }
                    break;
            }
        }
        else
        {
            Debug.Log($"다른 플레이어 소지금 : {Money}");
            Debug.Log($"다른 플레이어의 현재 주술 개수 : {sorceryButtons.sorceryCount}");
            Debug.Log($"다른 플레이어의 최대 주술 개수 : {sorceryButtons.maxSorceryCount}");
        }
    }

    /// <summary>
    /// functionbuttons 클래스의 onSorceryNumber 델리게이트를 받아 랜덤하게 속성을 결정하고 stoneSorceryButtons 배열에 누적할 함수
    /// </summary>
    /// <param name="sorceryGrade">생성될 주술 등급(0 : 일반, 1 : 희귀, 2 : 영웅, 3 : 전설, 4 : 선조, 5 : 천벌)</param>
    private void OnSorceryNumber(int sorceryGrade)
    {
        // 0 ~ 2 사이의 숫자 랜덤 선택
        int randomIndex = UnityEngine.Random.Range(0, 3);

        switch (randomIndex)
        {
            // 바위 속성
            case 0:
                // 바위 속성의 sorceryGrade 등급 주술 생성 -> 배열에 넣는건데..
                stoneSorcery[sorceryGrade]++;
                break;

            // 불 속성
            case 1:
                fireSorcery[sorceryGrade]++;
                break;

            // 물 속성
            case 2:
                waterSorcery[sorceryGrade]++;
                break;
        }

        UpdateSorceryCount();   // 주술 개수 갱신
    }

    /// <summary>
    /// 자동으로 합성시켜주는 함수
    /// </summary>
    void AutoSynthesis ()
    {
        for(int i = 0; i < 4; i++)
        {
            int index = i;
            if(stoneSorcery[index] > 2)
            {
                StoneSynthesis(index);
            }

            if (fireSorcery[index] > 2)
            {
                FireSynthesis(index);
            }

            if (waterSorcery[index] > 2)
            {
                WaterSynthesis(index);
            }
        }
    }

    /// <summary>
    /// 바위 합성
    /// </summary>
    /// <param name="index">몇번째 버튼인지</param>
    private void StoneSynthesis(int index)
    {
        Debug.Log($"{index} 번째 바위 버튼 누름");
        // 마지막 버튼은 합성X
        if (index < 4)
        {
            // 누적이 3이상이면
            if (stoneSorcery[index] > 2)
            {
                // 합성
                stoneSorcery[index] -= 3;

                int random = UnityEngine.Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                        stoneSorcery[index + 1]++;
                        break;
                    case 1:
                        fireSorcery[index + 1]++;
                        break;
                    case 2:
                        waterSorcery[index + 1]++;
                        break;
                }

                UpdateSorceryCount(); // 주술 개수 갱신
            }
        }
    }

    private void FireSynthesis(int index)
    {
        Debug.Log($"{index} 번째 불 버튼 누름");
        // 마지막 버튼은 합성X
        if (index < 4)
        {
            // 누적이 3이상이면
            if (fireSorcery[index] > 2)
            {
                // 합성
                fireSorcery[index] -= 3;

                int random = UnityEngine.Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                        stoneSorcery[index + 1]++;
                        break;
                    case 1:
                        fireSorcery[index + 1]++;
                        break;
                    case 2:
                        waterSorcery[index + 1]++;
                        break;
                }

                UpdateSorceryCount(); // 주술 개수 갱신
            }
        }
    }

    private void WaterSynthesis(int index)
    {
        Debug.Log($"{index} 번째 물 버튼 누름");
        // 마지막 버튼은 합성X
        if (index < 4)
        {
            // 누적이 3이상이면
            if (waterSorcery[index] > 2)
            {
                // 합성
                waterSorcery[index] -= 3;

                int random = UnityEngine.Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                        stoneSorcery[index + 1]++;
                        break;
                    case 1:
                        fireSorcery[index + 1]++;
                        break;
                    case 2:
                        waterSorcery[index + 1]++;
                        break;
                }

                UpdateSorceryCount(); // 주술 개수 갱신
            }
        }
    }

    /// <summary>
    /// sorceryCount를 갱신하는 함수
    /// </summary>
    public void UpdateSorceryCount()
    {
        sorceryCount = GetArraySum(stoneSorcery) + GetArraySum(fireSorcery) + GetArraySum(waterSorcery);
    }

    /// <summary>
    /// 배열의 합을 구하는 헬퍼 함수
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    private int GetArraySum(int[] array)
    {
        int sum = 0;
        if (array != null)
        {
            foreach (int value in array)
            {
                sum += value;
            }
        }
        return sum;
    }

    private void ExecuteAttack2(int element, int index)
    {
        switch (element)
        {
            // 바위
            case 0:
                switch (index)
                {
                    case 0:
                        factory3.GetStone_01(this.gameObject.transform.position);
                        break;
                    case 1:
                        factory3.GetStone_02(this.gameObject.transform.position);
                        break;
                    case 2:
                        factory3.GetStone_03(this.gameObject.transform.position);
                        break;
                    case 3:
                        factory3.GetStone_04(this.gameObject.transform.position);
                        break;
                    case 4:
                        factory3.GetStone_05(this.gameObject.transform.position);
                        break;
                    default:
                        break;
                }
                break;

            // 불
            case 1:
                switch (index)
                {
                    case 0:
                        factory3.GetFire_01(this.gameObject.transform.position);
                        break;
                    case 1:
                        factory3.GetFire_02(this.gameObject.transform.position);
                        break;
                    case 2:
                        factory3.GetFire_03(this.gameObject.transform.position);
                        break;
                    case 3:
                        factory3.GetFire_04(this.gameObject.transform.position);
                        break;
                    case 4:
                        factory3.GetFire_05(this.gameObject.transform.position);
                        break;
                    default:
                        break;
                }
                break;

            // 물
            case 2:
                switch (index)
                {
                    case 0:
                        factory3.GetWater_01(this.gameObject.transform.position);
                        break;
                    case 1:
                        factory3.GetWater_02(this.gameObject.transform.position);
                        break;
                    case 2:
                        factory3.GetWater_03(this.gameObject.transform.position);
                        break;
                    case 3:
                        factory3.GetWater_04(this.gameObject.transform.position);
                        break;
                    case 4:
                        factory3.GetWater_05(this.gameObject.transform.position);
                        break;
                    default:
                        break;
                }
                break;
        }
    }

    bool isAnimal_1 = false;
    bool isAnimal_2 = false;
    bool isAnimal_3 = false;

    /// <summary>
    /// 신수를 소환하는 함수
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private void AutoAnimalInstantiate()
    {
        // 일반 주술이 3종류 다 있으면
        if (!isAnimal_1)
        {
            if (stoneSorcery[0] > 0 && fireSorcery[0] > 0 && waterSorcery[0] > 0)
            {
                animalTileMap.SetTile(animalSpawnPositions[0], animal_1_Tile);
                animalSpawnPositions.RemoveAt(0);
                isAnimal_1 = true;
            }
        }

        // 희귀 주술이 3종류 다 있으면
        if (!isAnimal_2)
        {
            if (stoneSorcery[1] > 0 && fireSorcery[1] > 0 && waterSorcery[1] > 0)
            {
                animalTileMap.SetTile(animalSpawnPositions[0], animal_2_Tile);
                animalSpawnPositions.RemoveAt(0);
                isAnimal_2 = true;
            }
        }

        // 영웅 주술이 3종류 다 있으면
        if (!isAnimal_1)
        {
            if (stoneSorcery[2] > 0 && fireSorcery[2] > 0 && waterSorcery[2] > 0)
            {
                animalTileMap.SetTile(animalSpawnPositions[0], animal_3_Tile);
                animalSpawnPositions.RemoveAt(0);
                isAnimal_3 = true;
            }
        }
    }

    /// <summary>
    /// 채굴 함수
    /// </summary>
    /// <param name="index"></param>
    private void AutoMining()
    {
        int random = UnityEngine.Random.Range(0, 3);

        // 전설 등급만 자동으로 되도록
        if (Soul > 2)
        {
            Soul -= 2;

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
    }

    /// <summary>
    /// 현상금을 스폰하는 함수
    /// </summary>
    private void AutoBountyInstantiate()
    {
        if (bounty)
        {
            enemySpawner.BountyEnemySpawn(0);
            StartCoroutine(BountyCountDown());
        }
    }

    /// <summary>
    /// 현상금 카운트 다운 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator BountyCountDown()
    {
        float timeLeft = 60f;                               // 60초로 초기화
        bounty = false;
        while (timeLeft > 0)
        {
            int seconds = Mathf.FloorToInt(timeLeft);       // 초 계산

            // 1초 대기
            yield return new WaitForSeconds(1f);

            // 1초 감소
            timeLeft -= 1f;
        }
        bounty = true;
    }

    /// <summary>
    /// 소환 확률, 주술, 신수 등을 강화하는 함수
    /// </summary>
    private void AutoUpgrade()
    {
        // 소환 확률만 강화
        if (Money > 74 && sorceryUpgrade < 2)
        {
            Money -= 75;
            sorceryUpgrade++;
        }
    }
}
