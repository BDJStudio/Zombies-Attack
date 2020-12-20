using System.Collections;
using UnityEngine;

public class EnemyHP : MyHP
{
    public float originalHP = 10;

    public float currentHP;

    void Start()
    {
        currentHP = originalHP;
    }

    public override bool SetDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP >= 0)
        {
            return true;
        }
        else
        {
            Destroy(gameObject,1.5f);
            return false;
        }
    }
}
