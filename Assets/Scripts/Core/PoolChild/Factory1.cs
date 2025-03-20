using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*/// <summary>
/// 오브젝트 풀을 사용하는 오브젝트의 종류
/// </summary>
public enum PoolObjectType
{
    Stone_01,
    Stone_02,
    Stone_03,
    Stone_04,
    Stone_05,
    Fire_01,
    Fire_02,
    Fire_03,
    Fire_04,
    Fire_05,
    Water_01,
    Water_02,
    Water_03,
    Water_04,
    Water_05,
}*/

public class Factory1 : Singleton<Factory1>
{
    // 오브젝트 풀들
    Stone_01_Pool stone_01;     // 바위 공격_1
    Stone_02_Pool stone_02;     // 바위 공격_2
    Stone_03_Pool stone_03;     // 바위 공격_3
    Stone_04_Pool stone_04;     // 바위 공격_4
    Stone_05_Pool stone_05;     // 바위 공격_5

    Fire_01_Pool fire_01;       // 불 공격_1
    Fire_02_Pool fire_02;       // 불 공격_2
    Fire_03_Pool fire_03;       // 불 공격_3
    Fire_04_Pool fire_04;       // 불 공격_4
    Fire_05_Pool fire_05;       // 불 공격_5

    Water_01_Pool water_01;    // 물 공격_01
    Water_02_Pool water_02;    // 물 공격_02
    Water_03_Pool water_03;    // 물 공격_03
    Water_04_Pool water_04;    // 물 공격_04
    Water_05_Pool water_05;    // 물 공격_05

    /// <summary>
    /// 씬이 로딩 완료될 때마다 실행되는 초기화 함수
    /// </summary>
    protected override void OnInitialize()
    {
        base.OnInitialize();

        // 풀 컴포넌트 찾고, 찾으면 초기화하기

        // 바위 공격1
        stone_01 = GetComponentInChildren<Stone_01_Pool>();
        if (stone_01 != null)
        {
            //Debug.Log("Stone_01_Pool 초기화");
            stone_01.Initialize();
        }
        else
        {
            Debug.LogError("Stone_01_Pool 찾을 수 없습니다.");
        }

        // 바위 공격2
        stone_02 = GetComponentInChildren<Stone_02_Pool>();
        if (stone_02 != null)
        {
            //Debug.Log("Stone_02_Pool 초기화");
            stone_02.Initialize();
        }
        else
        {
            Debug.LogError("Stone_02_Pool 찾을 수 없습니다.");
        }

        // 바위 공격3
        stone_03 = GetComponentInChildren<Stone_03_Pool>();
        if (stone_03 != null)
        {
            //Debug.Log("Stone_03_Pool 초기화");
            stone_03.Initialize();
        }
        else
        {
            Debug.LogError("Stone_03_Pool 찾을 수 없습니다.");
        }

        // 바위 공격4
        stone_04 = GetComponentInChildren<Stone_04_Pool>();
        if (stone_04 != null)
        {
            //Debug.Log("Stone_04_Pool 초기화");
            stone_04.Initialize();
        }
        else
        {
            Debug.LogError("Stone_04_Pool 찾을 수 없습니다.");
        }

        // 바위 공격5
        stone_05 = GetComponentInChildren<Stone_05_Pool>();
        if (stone_05 != null)
        {
            //Debug.Log("Stone_05_Pool 초기화");
            stone_05.Initialize();
        }
        else
        {
            Debug.LogError("Stone_05_Pool 찾을 수 없습니다.");
        }

        // 불 공격 1
        fire_01 = GetComponentInChildren<Fire_01_Pool>();
        if (fire_01 != null)
        {
            //Debug.Log("Fire_01_Pool 초기화");
            fire_01.Initialize();
        }
        else
        {
            Debug.LogError("Fire_01_Pool 찾을 수 없습니다.");
        }

        // 불 공격 2
        fire_02 = GetComponentInChildren<Fire_02_Pool>();
        if (fire_02 != null)
        {
            //Debug.Log("Fire_02_Pool 초기화");
            fire_02.Initialize();
        }
        else
        {
            Debug.LogError("Fire_02_Pool 찾을 수 없습니다.");
        }

        // 불 공격3
        fire_03 = GetComponentInChildren<Fire_03_Pool>();
        if (fire_03 != null)
        {
            //Debug.Log("Fire_03_Pool 초기화");
            fire_03.Initialize();
        }
        else
        {
            Debug.LogError("Fire_03_Pool 찾을 수 없습니다.");
        }

        // 불 공격4
        fire_04 = GetComponentInChildren<Fire_04_Pool>();
        if (fire_04 != null)
        {
            //Debug.Log("Fire_04_Pool 초기화");
            fire_04.Initialize();
        }
        else
        {
            Debug.LogError("Stone_04_Pool 찾을 수 없습니다.");
        }

        // 불 공격5
        fire_05 = GetComponentInChildren<Fire_05_Pool>();
        if (fire_05 != null)
        {
            //Debug.Log("Fire_05_Pool 초기화");
            fire_05.Initialize();
        }
        else
        {
            Debug.LogError("Fire_05_Pool 찾을 수 없습니다.");
        }

        // 물 공격 1
        water_01 = GetComponentInChildren<Water_01_Pool>();
        if (water_01 != null)
        {
            //Debug.Log("Water_01_Pool 초기화");
            water_01.Initialize();
        }
        else
        {
            Debug.LogError("Water_01_Pool 찾을 수 없습니다.");
        }

        // 물 공격 2
        water_02 = GetComponentInChildren<Water_02_Pool>();
        if (water_02 != null)
        {
            //Debug.Log("Water_02_Pool 초기화");
            water_02.Initialize();
        }
        else
        {
            Debug.LogError("Water_02_Pool 찾을 수 없습니다.");
        }

        // 물 공격3
        water_03 = GetComponentInChildren<Water_03_Pool>();
        if (water_03 != null)
        {
            //Debug.Log("Water_03_Pool 초기화");
            water_03.Initialize();
        }
        else
        {
            Debug.LogError("Water_03_Pool 찾을 수 없습니다.");
        }

        // 물 공격4
        water_04 = GetComponentInChildren<Water_04_Pool>();
        if (water_04 != null)
        {
            //Debug.Log("Water_04_Pool 초기화");
            water_04.Initialize();
        }
        else
        {
            Debug.LogError("Water_04_Pool 찾을 수 없습니다.");
        }

        // 물 공격5
        water_05 = GetComponentInChildren<Water_05_Pool>();
        if (water_05 != null)
        {
            //Debug.Log("Water_05_Pool 초기화");
            water_05.Initialize();
        }
        else
        {
            Debug.LogError("Water_05_Pool 찾을 수 없습니다.");
        }
    }

    /// <summary>
    /// 풀에 있는 게임 오브젝트 하나 가져오기
    /// </summary>
    /// <param name="type">가져올 오브젝트의 종류</param>
    /// <param name="position">오브젝트가 배치될 위치</param>
    /// <param name="angle">오브젝트의 초기 각도</param>
    /// <returns>활성화된 오브젝트</returns>
    public GameObject GetObject(PoolObjectType type, Vector3? position = null, Vector3? euler = null)
    {
        GameObject result = null;
        switch (type)
        {
            case PoolObjectType.Fire_01:                            // 불 공격 1
                result = fire_01.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Fire_02:                            // 불 공격 2
                result = fire_02.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Fire_03:                            // 불 공격 3
                result = fire_03.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Fire_04:                            // 불 공격 4
                result = fire_04.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Fire_05:                            // 불 공격 5
                result = fire_05.GetObject(position, euler).gameObject;
                break;

            case PoolObjectType.Stone_01:                            // 바위 공격 1
                result = stone_01.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Stone_02:                            // 바위 공격 2
                result = stone_02.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Stone_03:                            // 바위 공격 3
                result = stone_03.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Stone_04:                            // 바위 공격 4
                result = stone_04.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Stone_05:                            // 바위 공격 5
                result = stone_05.GetObject(position, euler).gameObject;
                break;

            case PoolObjectType.Water_01:                            // 물 공격 1
                result = water_01.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Water_02:                            // 물 공격 2
                result = water_02.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Water_03:                            // 물 공격 3
                result = water_03.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Water_04:                            // 물 공격 4
                result = water_04.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Water_05:                            // 물 공격 5
                result = water_05.GetObject(position, euler).gameObject;
                break;
        }

        return result;
    }

    // 불 --------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 불 공격 1 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Fire_01 GetFire_01()
    {
        return fire_01.GetObject();
    }

    /// <summary>
    /// 불 공격 1 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Fire_01 GetFire_01(Vector3 position, float angle = 0.0f)
    {
        return fire_01.GetObject(position, angle * Vector3.forward);

        /*Fire_01 fire = fire_01.GetObject(position, angle * Vector3.forward);
        Debug.Log($"Fire_01 생성 위치: {fire.transform.position} (요청한 위치: {position})");
        return fire;*/
    }

    /// <summary>
    /// 불 공격 2 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Fire_02 GetFire_02()
    {
        return fire_02.GetObject();
    }

    /// <summary>
    /// 불 공격 2 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Fire_02 GetFire_02(Vector3 position, float angle = 0.0f)
    {
        return fire_02.GetObject(position, angle * Vector3.forward);
    }

    /// <summary>
    /// 불 공격 3 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Fire_03 GetFire_03()
    {
        return fire_03.GetObject();
    }

    /// <summary>
    /// 불 공격 3 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Fire_03 GetFire_03(Vector3 position, float angle = 0.0f)
    {
        return fire_03.GetObject(position, angle * Vector3.forward);
    }

    /// <summary>
    /// 불 공격 4 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Fire_04 GetFire_04()
    {
        return fire_04.GetObject();
    }

    /// <summary>
    /// 불 공격 4 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Fire_04 GetFire_04(Vector3 position, float angle = 0.0f)
    {
        return fire_04.GetObject(position, angle * Vector3.forward);
    }

    /// <summary>
    /// 불 공격 5 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Fire_05 GetFire_05()
    {
        return fire_05.GetObject();
    }

    /// <summary>
    /// 불 공격 5 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Fire_05 GetFire_05(Vector3 position, float angle = 0.0f)
    {
        return fire_05.GetObject(position, angle * Vector3.forward);
    }

    // 바위 --------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 바위 공격 1 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Stone_01 GetStone_01()
    {
        return stone_01.GetObject();
    }

    /// <summary>
    /// 바위 공격 1 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Stone_01 GetStone_01(Vector3 position, float angle = 0.0f)
    {
        return stone_01.GetObject(position, angle * Vector3.forward);
    }

    /// <summary>
    /// 바위 공격 2 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Stone_02 GetStone_02()
    {
        return stone_02.GetObject();
    }

    /// <summary>
    /// 바위 공격 2 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Stone_02 GetStone_02(Vector3 position, float angle = 0.0f)
    {
        return stone_02.GetObject(position, angle * Vector3.forward);
    }

    /// <summary>
    /// 바위 공격 3 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Stone_03 GetStone_03()
    {
        return stone_03.GetObject();
    }

    /// <summary>
    /// 바위 공격 3 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Stone_03 GetStone_03(Vector3 position, float angle = 0.0f)
    {
        return stone_03.GetObject(position, angle * Vector3.forward);
    }

    /// <summary>
    /// 바위 공격 4 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Stone_04 GetStone_04()
    {
        return stone_04.GetObject();
    }

    /// <summary>
    /// 바위 공격 4 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Stone_04 GetStone_04(Vector3 position, float angle = 0.0f)
    {
        return stone_04.GetObject(position, angle * Vector3.forward);
    }

    /// <summary>
    /// 바위 공격 5 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Stone_05 GetStone_05()
    {
        return stone_05.GetObject();
    }

    /// <summary>
    /// 바위 공격 5 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Stone_05 GetStone_05(Vector3 position, float angle = 0.0f)
    {
        return stone_05.GetObject(position, angle * Vector3.forward);
    }

    // 물 --------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 물 공격 1 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Water_01 GetWater_01()
    {
        return water_01.GetObject();
    }

    /// <summary>
    /// 물 공격 1 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Water_01 GetWater_01(Vector3 position, float angle = 0.0f)
    {
        return water_01.GetObject(position, angle * Vector3.forward);
    }

    /// <summary>
    /// 물 공격 2 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Water_02 GetWater_02()
    {
        return water_02.GetObject();
    }

    /// <summary>
    /// 물 공격 2 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Water_02 GetWater_02(Vector3 position, float angle = 0.0f)
    {
        return water_02.GetObject(position, angle * Vector3.forward);
    }

    /// <summary>
    /// 물 공격 3 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Water_03 GetWater_03()
    {
        return water_03.GetObject();
    }

    /// <summary>
    /// 물 공격 3 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Water_03 GetWater_03(Vector3 position, float angle = 0.0f)
    {
        return water_03.GetObject(position, angle * Vector3.forward);
    }

    /// <summary>
    /// 물 공격 4 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Water_04 GetWater_04()
    {
        return water_04.GetObject();
    }

    /// <summary>
    /// 물 공격 4 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Water_04 GetWater_04(Vector3 position, float angle = 0.0f)
    {
        return water_04.GetObject(position, angle * Vector3.forward);
    }

    /// <summary>
    /// 물 공격 5 하나 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Water_05 GetWater_05()
    {
        return water_05.GetObject();
    }

    /// <summary>
    /// 물 공격 5 하나 가져와서 특정 위치에 배치하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Water_05 GetWater_05(Vector3 position, float angle = 0.0f)
    {
        return water_05.GetObject(position, angle * Vector3.forward);
    }
}
