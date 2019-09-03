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

    protected override void Awake()
    {
        base.Awake();
        monsterRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterAnimator = GetComponent<Animator>();
    }
}
