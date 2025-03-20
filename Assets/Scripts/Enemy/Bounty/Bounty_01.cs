using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounty_01 : EnemyBase
{
    protected override void Start()
    {
        moveSpeed = 0.5f;
        waitTime = 0;
        currentHp = 100;
        maxHP = currentHp;
        dieMoney = 100;
        base.Start();
    }
}
