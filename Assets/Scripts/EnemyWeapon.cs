using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private DataBase db;
    private EnemyMotor enemyMotor;

    private void Start()
    {
        db = GameObject.Find("DataBase").GetComponent<DataBase>();
        enemyMotor = GetComponentInParent<EnemyMotor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Damage collisionDamage = other.gameObject.GetComponent<Damage>();

            if (collisionDamage)
                collisionDamage.SetDamage(db.mobs[enemyMotor.mobIdCount].countDam);
        }
    }
}
