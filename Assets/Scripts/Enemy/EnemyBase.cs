using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    /// <summary>
    /// 게임 매니저
    /// </summary>
    GameManager gameManager;

    /// <summary>
    /// 웨이포인트
    /// </summary>
    Transform[] waypoints;

    /// <summary>
    /// 적 스폰하는 오브젝트
    /// </summary>
    EnemySpawner enemySpawner;

    /// <summary>
    /// 현재 목표 웨이포인트 인덱스
    /// </summary>
    int currentWaypointIndex = 0;

    /// <summary>
    /// 이동 속도
    /// </summary>
    protected float moveSpeed = 2.0f;

    /// <summary>
    /// 몬스터가 죽었을 때 주는 돈
    /// </summary>
    protected int dieMoney = 1;

    /// <summary>
    /// 몬스터가 죽었을 때 주는 소울
    /// </summary>
    protected int dieSoul = 0;

    /// <summary>
    /// 최대 체력
    /// </summary>
    protected int maxHP = 100;

    /// <summary>
    /// 몬스터의 현재 체력
    /// </summary>
    protected int currentHp = 30;

    /// <summary>
    /// 어택 베이스
    /// </summary>
    //AttackBase attackBase;

    /// <summary>
    /// 몬스터 체력 프로퍼티
    /// </summary>
    public int HP
    {
        get => currentHp;
        set
        {
            if (currentHp != value)
            {
                //currentHp = value;
                currentHp = Mathf.Clamp(value, 0, maxHP);
                if (currentHp < 1)
                {
                    currentHp = 0;                      // 몬스터의 hp가 0이 된다면
                    gameManager.Money += dieMoney;      // 돈 증가
                    gameManager.Soul += dieSoul;        // 소울 증가
                    
                    //attackBase.attackList.Remove(this);
                    Destroy(gameObject);        // 게임 오브젝트 파괴
                    Debug.Log("사망");
                }
            }
        }
    }

    /// <summary>
    /// 웨이포인트에 도착하면 대기하는 시간
    /// </summary>
    protected float waitTime = 1.0f;

    /// <summary>
    /// 웨이포인트의 개수
    /// </summary>
    int waypointCount;

    /// <summary>
    /// 턴 매니저
    /// </summary>
    TurnManager turnManager;

    /// <summary>
    /// 웨이포인트를 모두 순회한 몬스터의 숫자(플레이어에 부딪힌 몬스터의 숫자)
    /// </summary>
    static int playerArriveMonster = 0;

    /// <summary>
    /// 현재 오브젝트의 최상위 부모(EnemySpawner)
    /// </summary>
    Transform rootParent;

    /// <summary>
    /// 최상위 부모의 0번째 자식(웨이포인트0)
    /// </summary>
    Transform firstChild;

    /// <summary>
    /// 최상위 부모의 1번째 자식(웨이포인트1)
    /// </summary>
    Transform secondChild;

    bool first = true;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;

        // 현재 오브젝트의 최상위 부모를 찾음
        rootParent = transform.root;

        // 최상위 부모의 0번째 자식
        firstChild = rootParent.GetChild(0);

        // 최상위 부모의 1번째 자식
        secondChild = rootParent.GetChild(1);

        //enemySpawner = FindAnyObjectByType<EnemySpawner>();

        //waypointCount = enemySpawner.transform.childCount;      // enemySpawner의 자식 개수
        waypointCount = 2;      // enemySpawner의 자식 개수

        waypoints = new Transform[waypointCount];

        for (int i = 0; i < waypointCount; i++)
        {
            waypoints[i] = rootParent.GetChild(i).transform;
        }

        // 처음 웨이포인트로 이동 시작
        if (waypoints.Length > 0)
        {
            StartCoroutine(MoveToNextWaypoint());
        }

        /*for (int i = 0; i < waypointCount; i++)
        {
            waypoints[i] = enemySpawner.transform.GetChild(i).transform;
        }*/

        turnManager = FindAnyObjectByType<TurnManager>();
        first = false;

        //attackBase = FindAnyObjectByType<AttackBase>();

        //monsterDieCount = 0;
        //playerArriveMonster = 0;
    }

    protected void OnEnable()
    {
        if (!first)
        {
            // 현재 오브젝트의 최상위 부모를 찾음
            rootParent = transform.root;

            // 최상위 부모의 0번째 자식
            firstChild = rootParent.GetChild(0);

            // 최상위 부모의 1번째 자식
            secondChild = rootParent.GetChild(1);

            for (int i = 0; i < waypointCount; i++)
            {
                waypoints[i] = rootParent.GetChild(i).transform;
            }

            // 처음 웨이포인트로 이동 시작
            if (waypoints.Length > 0)
            {
                StartCoroutine(MoveToNextWaypoint());
            }
        }

    }

    /// <summary>
    /// 다음 웨이포인트로 움직이는 코루틴
    /// </summary>
    IEnumerator MoveToNextWaypoint()
    {
        while (currentWaypointIndex < waypointCount)
        {
            if (waypoints.Length == 0)
                yield break;

            // 현재 목표 웨이포인트 위치
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;

            // 몬스터가 목표 위치에 도달할 때까지 이동
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // 다음 웨이포인트로 인덱스 증가 (순환하게 하기 위해 % 사용)
            //currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            currentWaypointIndex++;

            // 잠시 대기
            yield return new WaitForSeconds(waitTime);
        }

        // 여기부터는 모든 웨이포인트를 순회했음

        // 여기에 문에 도착한 적을 누적 시키는 부분 필요
        playerArriveMonster++;
        Debug.Log($"플레이어에 도착한 적 : {playerArriveMonster}");

        //IncrementMonsterDieCount();
        Destroy(gameObject);        // 게임 오브젝트 파괴
    }

    /// <summary>
    /// 적이 데미지를 받을 때 호출되는 메서드
    /// </summary>
    public void TakeDamage(int damage)
    {
        HP -= damage; // 데미지만큼 HP를 감소시킴
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {

        }
    }*/
}
