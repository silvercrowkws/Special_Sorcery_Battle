using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounty_04 : EnemyBase
{
    protected override void Start()
    {
        moveSpeed = 0.5f;
        waitTime = 0;
        currentHp = 750;
        maxHP = currentHp;
        dieMoney = 20;
        dieSoul = 5;
        base.Start();
    }
}
