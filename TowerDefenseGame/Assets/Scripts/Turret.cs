﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCowntdown = 0f;


    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowAmount = 0.5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;


    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform PartToRotate;
    public float turnSpeed = 10f;

    
    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        } else
        {
            target = null;
        }

    }

    void Update()
    {
        if (target == null) 
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }
       

        LockOnTarger();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCowntdown <= 0f)
            {
                Shoot();
                fireCowntdown = 1f / fireRate;
            }

            fireCowntdown -= Time.deltaTime;
        }

        
    }

    private void Laser()
    {

        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);


        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.LookAt(firePoint);
        


    }

    private void LockOnTarger()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0);
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
