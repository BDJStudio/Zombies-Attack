using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeLive = 2;

    private float timeDeath;

    private void Start()
    {
        timeDeath = Time.time + timeLive;
    }

    private void FixedUpdate()
    {
        if (Time.time > timeDeath)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Damage collisionDamage = other.gameObject.GetComponent<Damage>();
        
        if (collisionDamage)
            collisionDamage.SetDamage(1);

        Destroy(gameObject);
    }
}
