using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPlayer : Player
{
    public int[] stoneSorcery;
    public int[] fireSorcery;
    public int[] waterSorcery;

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

    protected override void Awake()
    {
        stoneSorcery = new int[5];
        fireSorcery = new int[5];
        waterSorcery = new int[5];
        sorceryCost = 20;
        base.Awake();
    }

    protected override void Start()
    {
        currentMoney = 100;
        base.Start();
    }

    protected override void Update()
    {
        Debug.Log(this.gameObject.transform.position);
        // 현재 소지금이 주술 생성비용보다 많으면
        if(Money >= sorceryCost)
        {
            OnSorceryButton();
        }

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
                        factory.GetStone_01(this.gameObject.transform.position);
                        break;
                    case 1:
                        factory.GetStone_02(this.gameObject.transform.position);
                        break;
                    case 2:
                        factory.GetStone_03(this.gameObject.transform.position);
                        break;
                    case 3:
                        factory.GetStone_04(this.gameObject.transform.position);
                        break;
                    case 4:
                        factory.GetStone_05(this.gameObject.transform.position);
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
                        factory.GetFire_01(this.gameObject.transform.position);
                        break;
                    case 1:
                        factory.GetFire_02(this.gameObject.transform.position);
                        break;
                    case 2:
                        factory.GetFire_03(this.gameObject.transform.position);
                        break;
                    case 3:
                        factory.GetFire_04(this.gameObject.transform.position);
                        break;
                    case 4:
                        factory.GetFire_05(this.gameObject.transform.position);
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
                        factory.GetWater_01(this.gameObject.transform.position);
                        break;
                    case 1:
                        factory.GetWater_02(this.gameObject.transform.position);
                        break;
                    case 2:
                        factory.GetWater_03(this.gameObject.transform.position);
                        break;
                    case 3:
                        factory.GetWater_04(this.gameObject.transform.position);
                        break;
                    case 4:
                        factory.GetWater_05(this.gameObject.transform.position);
                        break;
                    default:
                        break;
                }
                break;
        }
    }
}
