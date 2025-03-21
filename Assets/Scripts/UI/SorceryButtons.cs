using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SorceryButtons : MonoBehaviour
{
    /// <summary>
    /// 바위 주술 버튼의 배열
    /// 각 번호의 배열에 숫자 : 그 주술이 몇개 있는지(UI 갱신도 해야 됨)
    /// </summary>
    Button[] stoneSorceryButtons;

    /// <summary>
    /// 불 주술 버튼의 배열
    /// </summary>
    Button[] fireSorceryButtons;

    /// <summary>
    /// 물 주술 버튼의 배열
    /// </summary>
    Button[] waterSorceryButtons;

    /// <summary>
    /// 바위 주술 버튼 배열의 숫자 텍스트
    /// </summary>
    TextMeshProUGUI[] stoneSorceryButtonsText;

    /// <summary>
    /// 불 주술 버튼 배열의 숫자 텍스트
    /// </summary>
    TextMeshProUGUI[] fireSorceryButtonsText;

    /// <summary>
    /// 물 주술 버튼 배열의 숫자 텍스트
    /// </summary>
    TextMeshProUGUI[] waterSorceryButtonsText;

    public int[] stoneSorcery;
    public int[] fireSorcery;
    public int[] waterSorcery;

    /// <summary>
    /// 각 버튼의 캔버스그룹
    /// </summary>
    CanvasGroup[] canvasGroup;

    /*public int[] StoneSorcery
    {
        get => stoneSorcery;
        set
        {
            if(stoneSorcery != value)
            {
                stoneSorcery = value;
                UpdateSorceryCount();
            }
        }
    }

    public int[] FireSorcery
    {
        get => fireSorcery;
        set
        {
            if (fireSorcery != value)
            {
                fireSorcery = value;
                UpdateSorceryCount();
            }
        }
    }

    public int[] WaterSorcery
    {
        get => waterSorcery;
        set
        {
            if (waterSorcery != value)
            {
                waterSorcery = value;
                UpdateSorceryCount();
            }
        }
    }*/

    /// <summary>
    /// 주술 개수
    /// </summary>
    public int sorceryCount;

    /// <summary>
    /// 주술 개수가 변경되었을 때 알리는 델리게이트
    /// </summary>
    public Action<int> onSorceryCountChange;

    /// <summary>
    /// 최대 주술 개수
    /// </summary>
    public int maxSorceryCount = 25;

    /// <summary>
    /// 주술 버튼 배열의 크기
    /// </summary>
    int sorceryButtonsCount = 15;

    /// <summary>
    /// 기능 버튼들
    /// </summary>
    Functionbuttons functionbuttons;

    private void Awake()
    {
        // 배열 초기화
        stoneSorceryButtons = new Button[5];
        fireSorceryButtons = new Button[5];
        waterSorceryButtons = new Button[5];
        stoneSorcery = new int[5];
        fireSorcery = new int[5];
        waterSorcery = new int[5];
        stoneSorceryButtonsText = new TextMeshProUGUI[5];
        fireSorceryButtonsText = new TextMeshProUGUI[5];
        waterSorceryButtonsText = new TextMeshProUGUI[5];
        canvasGroup = new CanvasGroup[15];

        // 버튼을 할당하는 반복문
        for (int i = 0; i < sorceryButtonsCount; i++)
        {
            Button button = transform.GetChild(i).GetComponent<Button>();
            int index = i;      // 로컬 변수로 저장

            // 5로 나눈 몫을 기준으로
            // 0 : 0 1 2 3 4
            // 1 : 5 6 7 8 9
            // 2 : 10 11 12 13 14
            switch (i / 5)
            {
                case 0:     // i 값이 0, 1, 2, 3, 4인 경우 (바위 주술)
                    stoneSorceryButtons[index] = button;
                    stoneSorceryButtons[index].onClick.AddListener(() => StoneSynthesis(index));
                    stoneSorceryButtonsText[index] = button.GetComponentInChildren<TextMeshProUGUI>();
                    stoneSorceryButtonsText[index].text = "0";
                    break;
                case 1:     // i 값이 5, 6, 7, 8, 9인 경우 (불 주술)
                    fireSorceryButtons[index - 5] = button;         // 인덱스를 0부터 시작하게 하기 위해서 5를 뺌
                    fireSorceryButtons[index - 5].onClick.AddListener(() => FireSynthesis(index - 5));
                    fireSorceryButtonsText[index - 5] = button.GetComponentInChildren<TextMeshProUGUI>();
                    fireSorceryButtonsText[index - 5].text = "0";
                    break;
                case 2:     // i 값이 10, 11, 12, 13, 14인 경우 (물 주술)
                    waterSorceryButtons[index - 10] = button;       // 인덱스를 0부터 시작하게 하기 위해서 10을 뺌
                    waterSorceryButtons[index - 10].onClick.AddListener(() => WaterSynthesis(index - 10));
                    waterSorceryButtonsText[index - 10] = button.GetComponentInChildren<TextMeshProUGUI>();
                    waterSorceryButtonsText[index - 10].text = "0";
                    break;
            }

            canvasGroup[i] = button.GetComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        functionbuttons = FindAnyObjectByType<Functionbuttons>();
        functionbuttons.onSorceryNumber += OnSorceryNumber;
    }

    private void Update()
    {
        for(int i = 0; i < stoneSorceryButtons.Length; i++)
        {
            if (stoneSorcery[i] > 0)
            {
                canvasGroup[i].alpha = 1;
            }
            else
            {
                canvasGroup[i].alpha = 0.25f;
            }
        }

        for(int i = 5; i < fireSorceryButtons.Length + 5; i++)
        {
            if (fireSorcery[i - 5] > 0)
            {
                canvasGroup[i].alpha = 1;
            }
            else
            {
                canvasGroup[i].alpha = 0.25f;
            }
        }

        for(int i = 10; i < waterSorceryButtons.Length + 10; i++)
        {
            if (waterSorcery[i - 10] > 0)
            {
                canvasGroup[i].alpha = 1;
            }
            else
            {
                canvasGroup[i].alpha = 0.25f;
            }
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
                stoneSorceryButtonsText[sorceryGrade].text = stoneSorcery[sorceryGrade].ToString();
                break;

            // 불 속성
            case 1:
                fireSorcery[sorceryGrade]++;
                fireSorceryButtonsText[sorceryGrade].text = fireSorcery[sorceryGrade].ToString();
                break;

                // 물 속성
            case 2:
                waterSorcery[sorceryGrade]++;
                waterSorceryButtonsText[sorceryGrade].text = waterSorcery[sorceryGrade].ToString();
                break;
        }

        UpdateSorceryCount();   // 주술 개수 갱신
    }

    /// <summary>
    /// 바위 합성
    /// </summary>
    /// <param name="index">몇번째 버튼인지</param>
    private void StoneSynthesis(int index)
    {
        Debug.Log($"{index} 번째 바위 버튼 누름");
        // 마지막 버튼은 합성X
        if(index < 4)
        {
            // 누적이 3이상이면
            if (stoneSorcery[index] > 2)
            {
                // 합성
                stoneSorcery[index] -= 3;
                stoneSorceryButtonsText[index].text = stoneSorcery[index].ToString();

                int random = UnityEngine.Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                        stoneSorcery[index + 1]++;
                        stoneSorceryButtonsText[index + 1].text = stoneSorcery[index + 1].ToString();
                        break;
                    case 1:
                        fireSorcery[index + 1]++;
                        fireSorceryButtonsText[index + 1].text = fireSorcery[index + 1].ToString();
                        break;
                    case 2:
                        waterSorcery[index + 1]++;
                        waterSorceryButtonsText[index + 1].text = waterSorcery[index + 1].ToString();
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
                fireSorceryButtonsText[index].text = fireSorcery[index].ToString();

                int random = UnityEngine.Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                        stoneSorcery[index + 1]++;
                        stoneSorceryButtonsText[index + 1].text = stoneSorcery[index + 1].ToString();
                        break;
                    case 1:
                        fireSorcery[index + 1]++;
                        fireSorceryButtonsText[index + 1].text = fireSorcery[index + 1].ToString();
                        break;
                    case 2:
                        waterSorcery[index + 1]++;
                        waterSorceryButtonsText[index + 1].text = waterSorcery[index + 1].ToString();
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
                waterSorceryButtonsText[index].text = waterSorcery[index].ToString();

                int random = UnityEngine.Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                        stoneSorcery[index + 1]++;
                        stoneSorceryButtonsText[index + 1].text = stoneSorcery[index + 1].ToString();
                        break;
                    case 1:
                        fireSorcery[index + 1]++;
                        fireSorceryButtonsText[index + 1].text = fireSorcery[index + 1].ToString();
                        break;
                    case 2:
                        waterSorcery[index + 1]++;
                        waterSorceryButtonsText[index + 1].text = waterSorcery[index + 1].ToString();
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
        onSorceryCountChange?.Invoke(sorceryCount);
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
}
