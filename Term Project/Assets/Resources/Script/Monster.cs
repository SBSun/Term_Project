using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LivingEntity
{
    public enum MonsterType
    {
        Move, //파괴 불가능
        RandomMove, //파괴 가능
        Fix, //파괴 불가능
        Follow //파괴 가능
    }
    public MonsterType monsterType;

    protected Rigidbody2D       monsterRb;
    protected SpriteRenderer    spriteRenderer;
    protected Animator          monsterAnimator;

    public override void OnDamage()
    {
        curLife--;

        if(curLife <= 0)
        {
            Die();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        curLife = maxLife;
        monsterRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterAnimator = GetComponent<Animator>();
    }
}
