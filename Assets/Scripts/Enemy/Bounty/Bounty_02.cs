using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounty_02 : EnemyBase
{
    protected override void Start()
    {
        moveSpeed = 0.5f;
        waitTime = 0;
        currentHp = 250;
        maxHP = currentHp;
        dieMoney = 20;
        dieSoul = 1;
        base.Start();
    }
}
