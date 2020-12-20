using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMotor : Damage
{
    public int mobIdCount;
    public float stopDistance;

    private bool isLive = true;

    private MyHP thisHP;
    private GameObject player;
    private Transform thisTransform;
    private Animation anim;
    private DataBase dataBase;
    private NavMeshAgent agent;

    void Start()
    {
        thisHP = GetComponent<MyHP>();
        anim = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player");
        dataBase = GameObject.Find("DataBase").GetComponent<DataBase>();

        thisTransform = transform;
        agent.speed = dataBase.mobs[mobIdCount].speed;
    }

    public override void SetDamage(float damage)
    {
        if (isLive)
        {
            if (thisHP.SetDamage(damage))
            {
                
            }
            else
            {
                isLive = false;
                player.GetComponent<PlayerShoot>().countPoints += dataBase.mobs[mobIdCount].deadPoints;
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }

    public void Update()
    {
        if (Vector3.Distance(thisTransform.position, player.transform.position) >= stopDistance && isLive)
        {
            anim.Play("walkzombie");
            agent.SetDestination(player.transform.position);
        }
        else
        {
            if (isLive)
            {
                anim.Play("attack01");
                agent.isStopped = true;
            }
            else
            {
                anim.Play("die01");
                agent.isStopped = true;
            }
        }
    }
}
