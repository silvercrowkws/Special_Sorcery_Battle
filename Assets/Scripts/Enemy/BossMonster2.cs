using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster2 : EnemyBase
{
    protected override void Start()
    {
        moveSpeed = 0.5f;
        waitTime = 0;
        currentHp = 600;
        maxHP = currentHp;
        dieMoney = 20;
        dieSoul = 5;
        base.Start();
    }
}
