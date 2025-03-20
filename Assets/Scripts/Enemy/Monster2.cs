using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : EnemyBase
{
    protected override void Start()
    {
        moveSpeed = 1;
        waitTime = 0;
        currentHp = 75;
        maxHP = currentHp;
        dieMoney = 30;
        base.Start();
    }
}
