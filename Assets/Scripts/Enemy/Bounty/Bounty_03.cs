using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounty_03 : EnemyBase
{
    protected override void Start()
    {
        moveSpeed = 0.5f;
        waitTime = 0;
        currentHp = 500;
        maxHP = currentHp;
        dieMoney = 300;
        base.Start();
    }
}
