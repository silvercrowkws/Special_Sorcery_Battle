using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Factory factory;

    /// <summary>
    /// 애니메이터
    /// </summary>
    protected Animator animator;

    /*public GameObject stone_1;      // 근접 공격
    public GameObject stone_2;      // 근접 공격
    public GameObject stone_3;      // 보호막
    public GameObject stone_4;      // 4명 공격
    public GameObject stone_5;

    public GameObject fire_1;       // 적에게 닿으면 파괴되면서 데미지
    public GameObject fire_2;       // 적을 최대 5마리까지 공격하면서 데미지
    public GameObject fire_3;       // 바닥에 불 장판
    public GameObject fire_4;       // 전방으로 움직이며 공격
    public GameObject fire_5;

    public GameObject water_1;      // 적에게 닿으면 파괴되면서 데미지
    public GameObject water_2;      // 적을 최대 5마리까지 공격하면서 데미지
    public GameObject water_3;      // 바닥에 얼음 장판
    public GameObject water_4;      // 최대 체력이 제일 높은 적 우선 공격
    public GameObject water_5;*/

    /// <summary>
    /// SorceryButtons 클래스
    /// </summary>
    protected SorceryButtons sorceryButtons;

    /// <summary>
    /// 각 공격의 쿨다운 시간을 저장할 변수
    /// </summary>
    protected float[] stoneCooldownTimers = new float[5];     // stoneSorcery[0]~[4]에 대한 쿨다운 타이머
    protected float[] fireCooldownTimers = new float[5];      // fireSorcery[0]~[4]에 대한 쿨다운 타이머
    protected float[] waterCooldownTimers = new float[5];     // waterSorcery[0]~[4]에 대한 쿨다운 타이머

    /// <summary>
    /// 각 공격의 쿨다운 시간 (초 단위)
    /// </summary>
    public float[] cooldownDurations = new float[5] { 1.0f, 1.5f, 2.0f, 2.5f, 3.0f };

    protected virtual void Awake()
    {
        factory = Factory.Instance;
        animator = GetComponent<Animator>();
        animator.SetTrigger("Attack");
    }

    protected virtual void Start()
    {
        sorceryButtons = FindAnyObjectByType<SorceryButtons>();

        // 쿨다운 초기화
        for (int i = 0; i < stoneCooldownTimers.Length; i++)
        {
            stoneCooldownTimers[i] = 0f;
        }

        for (int i = 0; i < fireCooldownTimers.Length; i++)
        {
            fireCooldownTimers[i] = 0f;
        }

        for (int i = 0; i < waterCooldownTimers.Length; i++)
        {
            waterCooldownTimers[i] = 0f;
        }
    }

    protected virtual void Update()
    {
        // 바위 공격
        // 각 stoneSorcery 배열을 확인하고, 쿨다운이 끝난 공격이 있다면 실행
        for (int i = 0; i < sorceryButtons.stoneSorcery.Length; i++)
        {
            if (sorceryButtons.stoneSorcery[i] > 0 && stoneCooldownTimers[i] <= 0f)
            {
                // 해당 공격을 실행
                ExecuteAttack(0,i);

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
        for (int i = 0; i < sorceryButtons.fireSorcery.Length; i++)
        {
            if (sorceryButtons.fireSorcery[i] > 0 && fireCooldownTimers[i] <= 0f)
            {
                // 해당 공격을 실행
                ExecuteAttack(1, i);

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
        for (int i = 0; i < sorceryButtons.waterSorcery.Length; i++)
        {
            if (sorceryButtons.waterSorcery[i] > 0 && waterCooldownTimers[i] <= 0f)
            {
                // 해당 공격을 실행
                ExecuteAttack(2,i);

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
    }


    /// <summary>
    /// 각 속성 별로 공격 시키는 함수(팩토리로 생성)
    /// </summary>
    /// <param name="element"> 0 : 바위, 1 : 불, 2 : 물</param>
    /// <param name="index"></param>
    private void ExecuteAttack(int element, int index)
    {
        switch (element)
        {
            // 바위
            case 0:
                switch (index)
                {
                    case 0:
                        factory.GetStone_01(transform.position);
                        break;
                    case 1:
                        factory.GetStone_02(transform.position);
                        break;
                    case 2:
                        factory.GetStone_03(transform.position);
                        break;
                    case 3:
                        factory.GetStone_04(transform.position);
                        break;
                    case 4:
                        factory.GetStone_05(transform.position);
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
                        factory.GetFire_01(transform.position);
                        break;
                    case 1:
                        factory.GetFire_02(transform.position);
                        break;
                    case 2:
                        factory.GetFire_03(transform.position);
                        break;
                    case 3:
                        factory.GetFire_04(transform.position);
                        break;
                    case 4:
                        factory.GetFire_05(transform.position);
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
                        factory.GetWater_01(transform.position);
                        break;
                    case 1:
                        factory.GetWater_02(transform.position);
                        break;
                    case 2:
                        factory.GetWater_03(transform.position);
                        break;
                    case 3:
                        factory.GetWater_04(transform.position);
                        break;
                    case 4:
                        factory.GetWater_05(transform.position);
                        break;
                    default:
                        break;
                }
                break;
        }
    }
}
