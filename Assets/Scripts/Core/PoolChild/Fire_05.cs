using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_05 : RecycleObject
{
    /// <summary>
    /// 이 오브젝트의 수명
    /// </summary>
    public float lifeTime = 10f;

    /// <summary>
    /// 이 오브젝트의 이동 속도
    /// </summary>
    public float moveSpeed = 10f;  // 이동 속도 (초당 이동 거리)

    private Vector3 targetPosition;

    Player player;

    Vector3 startPosition;

    int damage = 50;


    protected override void OnEnable()
    {
        base.OnEnable();

        //player = GameManager.Instance.Player;
        //startPosition = player.transform.position;
        startPosition = transform.position;

        // 현재 위치에서 1초 동안 아래로 이동할 목표 위치를 설정
        targetPosition = startPosition - new Vector3(0, moveSpeed, 0); // Y축으로 이동

        StartCoroutine(MoveDown());
        StartCoroutine(LifeOver(lifeTime));
    }

    /// <summary>
    /// 생성 후 계속 아래로 이동하게 만드는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveDown()
    {
        float elapsedTime = 0f;
        //Vector3 startPosition = transform.position;

        // 1초 동안 부드럽게 이동
        while (elapsedTime < 1f)  // 1초 동안 이동
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 위치에 정확히 도달
        transform.position = targetPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("적과 충돌");

            // 충돌한 객체에서 EnemyBase 또는 그 자식 클래스를 가져오기
            EnemyBase enemyBase = collision.GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                // 데미지 주기
                enemyBase.TakeDamage(damage);
                //Debug.Log("적에게 데미지 주기");
            }

            // Fire_01 오브젝트 비활성화
            gameObject.SetActive(false);
        }
    }
}
