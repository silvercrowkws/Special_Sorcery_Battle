using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : EnemyBase
{
    protected override void Start()
    {
        moveSpeed = 1;
        waitTime = 0;
        currentHp = 50;
        maxHP = currentHp;
        dieMoney = 20;
        base.Start();
    }
}
