using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    /// <summary>
    /// 턴 매니저
    /// </summary>
    TurnManager turnManager;

    /// <summary>
    /// 게임 매니저
    /// </summary>
    GameManager gameManager;

    /// <summary>
    /// SorceryButtons 클래스
    /// </summary>
    SorceryButtons sorceryButtons;

    /// <summary>
    /// 웨이브 표시용 텍스트 매쉬 프로
    /// </summary>
    TextMeshProUGUI waveText;

    /// <summary>
    /// 다음 웨이브까지 남은 시간을 알리는 텍스트 매쉬 프로
    /// </summary>
    TextMeshProUGUI countDownText;

    TextMeshProUGUI moneyText;
    TextMeshProUGUI soulText;
    TextMeshProUGUI sorceryCountText;


    private void Awake()
    {
        Transform child = transform.GetChild(0);
        child = child.GetChild(0);      // WavePanel
        waveText = child.GetChild(1).GetComponent<TextMeshProUGUI>();
        waveText.text = "1";

        child = transform.GetChild(0);
        child = child.GetChild(1);
        countDownText = child.GetComponent<TextMeshProUGUI>();
        countDownText.text = "00:15";

        child = transform.GetChild(1);      // CashPanel
        moneyText = child.GetChild(0).GetChild(1).GetComponent <TextMeshProUGUI>();
        soulText = child.GetChild(1).GetChild(1).GetComponent <TextMeshProUGUI>();
        sorceryCountText = child.GetChild(2).GetChild(1).GetComponent <TextMeshProUGUI>();

        moneyText.text = "100";
        soulText.text = "0";
        sorceryCountText.text = "0";
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.moneyChange += OnMoneyChange;
        gameManager.soulChange += OnSoulChange;

        turnManager = TurnManager.Instance;
        turnManager.onTurnStart += OnCountDownStart;

        sorceryButtons = FindAnyObjectByType<SorceryButtons>();
        sorceryButtons.onSorceryCountChange += OnSorceryCountChange;
    }

    /// <summary>
    /// 턴 시작으로 카운트 다운을 시작하는 함수
    /// </summary>
    /// <param name="turnNumber">현재 턴 숫자</param>
    private void OnCountDownStart(int turnNumber)
    {
        waveText.text = turnNumber.ToString();

        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        float timeLeft = 15f;                               // 15초로 초기화
        while (timeLeft > 0)
        {
            int seconds = Mathf.FloorToInt(timeLeft);       // 초 계산

            // 텍스트 UI 업데이트 (문자열 보간 사용, 분은 항상 00으로 고정)
            countDownText.text = $"00:{seconds:00}";        // 초는 2자리로 표시

            // 1초 대기
            yield return new WaitForSeconds(1f);

            // 1초 감소
            timeLeft -= 1f;
        }

        // 카운트다운이 끝나면 "00:00"으로 설정
        countDownText.text = "00:00";

        turnManager.OnTurnEnd2();
    }

    /// <summary>
    /// 돈 UI 변경
    /// </summary>
    /// <param name="currentMoney">소지금</param>
    private void OnMoneyChange(int currentMoney)
    {
        moneyText.text = currentMoney.ToString();
    }

    /// <summary>
    /// 소울 UI 변경
    /// </summary>
    /// <param name="currentSoul"></param>
    private void OnSoulChange(int currentSoul)
    {
        soulText.text = currentSoul.ToString();
    }

    /// <summary>
    /// 주술 개수 UI 변경
    /// </summary>
    /// <param name="currentSorecy"></param>
    private void OnSorceryCountChange(int currentSorecy)
    {
        sorceryCountText.text = currentSorecy.ToString();
    }
}
