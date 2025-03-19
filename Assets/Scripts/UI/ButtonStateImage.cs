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

    SorceryButtons sorceryButtons;

    public Tilemap animalTileMap;

    public TileBase animal_1_Tile;
    public TileBase animal_2_Tile;
    public TileBase animal_3_Tile;

    List<Vector3Int> animalSpawnPositions;

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
        }

        for (int i = 0; i < miningButtons.Length; i++)
        {
            int index = i;
            miningButtons[index] = transform.GetChild(0).GetChild(index).GetComponent<Button>();
        }
    }

    private void Start()
    {
        sorceryButtons = FindAnyObjectByType<SorceryButtons>();
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
}
