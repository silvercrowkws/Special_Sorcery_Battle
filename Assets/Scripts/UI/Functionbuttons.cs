using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Functionbuttons : MonoBehaviour
{
    /// <summary>
    /// 현상금 버튼
    /// </summary>
    Button bountyButton;

    /// <summary>
    /// 신수 버튼
    /// </summary>
    Button animalButton;

    /// <summary>
    /// 주술 생성 버튼(주술 생성 버튼은 어떤 등급의 주술이 생성되었다는 것만 알림 => 관리 및 속성 결정은 SorceryButtons 클래스 쪽에서)
    /// </summary>
    Button sorceryButton;

    /// <summary>
    /// 강화 버튼
    /// </summary>
    Button upgradeButton;

    /// <summary>
    /// 채굴 버튼
    /// </summary>
    Button miningButton;

    /// <summary>
    /// 주술 생성 가격 변경 UI용 텍스트매쉬프로
    /// </summary>
    TextMeshProUGUI sorceryText;

    /// <summary>
    /// 주술 생성 가격
    /// </summary>
    int sorceryCost = 0;

    /// <summary>
    /// 게임 매니저
    /// </summary>
    GameManager gameManager;

    /// <summary>
    /// 게임 오브젝트(현상금 등 버튼을 눌렀을 때 필요)
    /// </summary>
    public Image UIImage;

    /// <summary>
    /// 특정한 버튼이 눌러졌을 때 보일 UI
    /// </summary>
    public Image ButtonStateImage;

    /// <summary>
    /// UIImage의 RectTransform
    /// </summary>
    RectTransform imageRect;

    Vector2 upVector = new Vector2(0, 150);
    Vector2 downVector = new Vector2(0, 0);

    /// <summary>
    /// 현상금 버튼이 활성화 되어있는지 확인하는 bool 변수
    /// </summary>
    bool isbounty = false;

    /// <summary>
    /// 신수 버튼이 활성화 되어있는지 확인하는 bool 변수
    /// </summary>
    bool isanimal = false;

    /// <summary>
    /// 마지막으로 눌린 버튼을 추적(UIImage가 올라가있는지)
    /// </summary>
    private enum LastButton { None, Bounty, Animal, upgradeButton, miningButton }
    private LastButton lastButton = LastButton.None;

    /// <summary>
    /// 강화된 정도
    /// </summary>
    public int sorceryUpgrade = 0;

    /// <summary>
    /// 어떤 등급의 주술이 생성되었는지 알리는 델리게이트
    /// 0 : 노말, 1 : 희귀, 2 : 영웅, 3 : 전설, 4 : 선조, 5 : 천벌
    /// </summary>
    public Action<int> onSorceryNumber;

    private void Awake()
    {
        bountyButton = transform.GetChild(0).GetComponent<Button>();
        bountyButton.onClick.AddListener(OnBountyButton);

        animalButton = transform.GetChild(1).GetComponent<Button>();
        animalButton.onClick.AddListener(OnAnimalButton);

        sorceryButton = transform.GetChild(2).GetComponent<Button>();
        sorceryButton.onClick.AddListener(OnSorceryButton);

        upgradeButton = transform.GetChild(3).GetComponent<Button>();
        upgradeButton.onClick.AddListener(OnUpgradeButton);

        miningButton = transform.GetChild(4).GetComponent<Button>();
        miningButton.onClick.AddListener(OnMiningButton);

        sorceryText = sorceryButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        sorceryCost = 20;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;

        imageRect = UIImage.GetComponent<RectTransform>();

        ButtonStateImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// 현상금 버튼
    /// </summary>
    private void OnBountyButton()
    {
        if (lastButton == LastButton.Bounty)
        {
            // 현상금 버튼을 연속으로 누르면 내려감
            imageRect.anchoredPosition = downVector;
            lastButton = LastButton.None;
        }
        else
        {
            // 다른 버튼을 누른 후, 현상금 버튼을 누르면 올라감
            imageRect.anchoredPosition = upVector;
            lastButton = LastButton.Bounty;
        }

        if(imageRect.anchoredPosition == upVector)
        {
            ButtonStateImage.gameObject.SetActive(true);
            ButtonStateImage.transform.GetChild(0).gameObject.SetActive(true);      // 0번째 자식 Bountys 활성화
            ButtonStateImage.transform.GetChild(1).gameObject.SetActive(false);     // 1번째 자식 Animal 비활성화
        }
        else
        {
            ButtonStateImage.gameObject.SetActive(false);
            ButtonStateImage.transform.GetChild(0).gameObject.SetActive(false);      // 0번째 자식 Bountys 비활성화
        }
    }

    /// <summary>
    /// 신수 버튼
    /// </summary>
    private void OnAnimalButton()
    {
        if (lastButton == LastButton.Animal)
        {
            // 신수 버튼을 연속으로 누르면 내려감
            imageRect.anchoredPosition = downVector;
            lastButton = LastButton.None;
        }
        else
        {
            // 다른 버튼을 누른 후, 신수 버튼을 누르면 올라감
            imageRect.anchoredPosition = upVector;
            lastButton = LastButton.Animal;
        }

        if (imageRect.anchoredPosition == upVector)
        {
            ButtonStateImage.gameObject.SetActive(true);
            ButtonStateImage.transform.GetChild(1).gameObject.SetActive(true);      // 0번째 자식 Animal 활성화
            ButtonStateImage.transform.GetChild(0).gameObject.SetActive(false);     // 0번째 자식 Bountys 비활성화
        }
        else
        {
            ButtonStateImage.gameObject.SetActive(false);
            ButtonStateImage.transform.GetChild(1).gameObject.SetActive(false);      // 0번째 자식 Animal 비활성화
        }
        // 타일맵에 SetTile 하는 방식으로 하자
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

        if(gameManager.Money >= sorceryCost)
        {
            gameManager.Money -= sorceryCost;                           // 돈 차감
            sorceryCost++;                                              // 가격++
            sorceryText.text = sorceryCost.ToString();                  // UI 변경

            float randomValue = UnityEngine.Random.Range(0f, 100f);     // 난수 생성

            // 주술 생성 부분(강화된 정도에 따라)
            switch (sorceryUpgrade)
            {
                // 기본 확률
                case 0:
                    if (randomValue < 84.98f)
                    {
                        Debug.Log("일반 (84.98%)");
                        onSorceryNumber?.Invoke(0);
                    }
                    else if (randomValue < 84.98f + 11.32f)
                    {
                        Debug.Log("희귀 (11.32%)");
                        onSorceryNumber?.Invoke(1);
                    }
                    else if (randomValue < 84.98f + 11.32f + 3f)
                    {
                        Debug.Log("영웅 (3%)");
                        onSorceryNumber?.Invoke(2);
                    }
                    else if (randomValue < 84.98f + 11.32f + 3f + 0.5f)
                    {
                        Debug.Log("전설 (0.5%)");
                        onSorceryNumber?.Invoke(3);
                    }
                    else if (randomValue < 84.98f + 11.32f + 3f + 0.5f + 0.2f)
                    {
                        Debug.Log("선조 (0.2%)");
                        onSorceryNumber?.Invoke(4);
                    }
                    else
                    {
                        Debug.Log("천벌 (0%)");
                        onSorceryNumber?.Invoke(5);
                    }
                    break;

                case 1:
                    if (randomValue < 78.535f)
                    {
                        Debug.Log("일반 (78.535%)");
                        onSorceryNumber?.Invoke(0);
                    }
                    else if (randomValue < 78.535f + 15.51f)
                    {
                        Debug.Log("희귀 (15.51%)");
                        onSorceryNumber?.Invoke(1);
                    }
                    else if (randomValue < 78.535f + 15.51f + 4.6f)
                    {
                        Debug.Log("영웅 (4.6%)");
                        onSorceryNumber?.Invoke(2);
                    }
                    else if (randomValue < 78.535f + 15.51f + 4.6f + 1f)
                    {
                        Debug.Log("전설 (1%)");
                        onSorceryNumber?.Invoke(3);
                    }
                    else if (randomValue < 78.535f + 15.51f + 4.6f + 1f + 0.35f)
                    {
                        Debug.Log("선조 (0.35%)");
                        onSorceryNumber?.Invoke(4);
                    }
                    else
                    {
                        Debug.Log("천벌 (0.005%)");
                        onSorceryNumber?.Invoke(5);
                    }
                    break;

                case 2:
                    if (randomValue < 72.95f)
                    {
                        Debug.Log("일반 (72.95%)");
                        onSorceryNumber?.Invoke(0);
                    }
                    else if (randomValue < 72.95f + 18.75f)
                    {
                        Debug.Log("희귀 (18.75%)");
                        onSorceryNumber?.Invoke(1);
                    }
                    else if (randomValue < 72.95f + 18.75f + 6.12f)
                    {
                        Debug.Log("영웅 (6.12%)");
                        onSorceryNumber?.Invoke(2);
                    }
                    else if (randomValue < 72.95f + 18.75f + 6.12f + 1.6f)
                    {
                        Debug.Log("전설 (1.6%)");
                        onSorceryNumber?.Invoke(3);
                    }
                    else if (randomValue < 72.95f + 18.75f + 6.12f + 1.6f + 0.55f)
                    {
                        Debug.Log("선조 (0.55%)");
                        onSorceryNumber?.Invoke(4);
                    }
                    else
                    {
                        Debug.Log("천벌 (0.03%)");
                        onSorceryNumber?.Invoke(5);
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// 강화 버튼
    /// </summary>
    private void OnUpgradeButton()
    {

    }

    /// <summary>
    /// 채굴 버튼
    /// </summary>
    private void OnMiningButton()
    {

    }
}
