﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//체력이 존재하는 오브젝트들의 부모 클래스
//장애물, 플레이어 오브젝트 등
//체력이 있고 죽을 수 있는(파괴 될 수 있는) 오브젝트들에게 할당
public abstract class LivingEntity : MonoBehaviour
{
    public enum Status
    {
        Live,
        Die
    }
    public Status status { get; protected set; }

    [Header( "최대 체력을 조절할 수 있습니다. (정수만 가능)" )]
    //최대 체력
    public int maxLife;
    //현재 체력
    public int curLife;

    //죽을 때 실행되는 이벤트
    protected event Action onDie;

    virtual protected void Awake()
    {
        status = Status.Live;
    }

    //데미지를 받았을 때 실행되는 함수
    abstract public void OnDamage();


    //죽었을 때 실행되는 함수
    public void Die()
    {
        status = Status.Die;

        if (onDie != null)
            onDie();
    }
}

