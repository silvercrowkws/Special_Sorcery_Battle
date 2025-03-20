using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounty_05 : EnemyBase
{
    protected override void Start()
    {
        moveSpeed = 0.5f;
        waitTime = 0;
        currentHp = 1000;
        maxHP = currentHp;
        dieMoney = 500;
        base.Start();
    }
}
