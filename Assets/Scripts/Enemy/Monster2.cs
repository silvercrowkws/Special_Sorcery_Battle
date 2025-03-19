using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : EnemyBase
{
    protected override void Start()
    {
        moveSpeed = 5;
        waitTime = 0;
        currentHp = 50;
        maxHP = currentHp;
        dieMoney = 20;
        base.Start();
    }
}
