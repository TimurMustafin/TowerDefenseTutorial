﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float health = 100;
    public int worth = 50;

    public GameObject deathEffect;

    void Start()
    {
        speed = startSpeed;
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <=0 )
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerStats.Money += worth;
        Destroy(gameObject);
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1 - pct);
    }
}

