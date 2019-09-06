using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float bulletSpeed = 70f;
    public int damage = 50;
    public float explostionRadius = 0f;
    public GameObject impactEffect;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame) 
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);


    }

    private void HitTarget()
    {

        
        GameObject effetcInstance =  Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effetcInstance, 5f);

        if (explostionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        
        Destroy(gameObject);


    }

    void Explode()
    {
        Collider[] colliders =  Physics.OverlapSphere(transform.position, explostionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explostionRadius);

    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

   
}
