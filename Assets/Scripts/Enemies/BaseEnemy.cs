using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseEnemy : MonoBehaviour
{
    public int maxHP;
    public int currentHp;
    public Transform target;
    public NavMeshAgent agent;
    public Animator anim;

    public virtual void Awake()
    {
        currentHp = maxHP;
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    public virtual void TakeDamage(int damage)
    {
        currentHp-=damage;
        if(currentHp<=0)
        {
            Destroy(gameObject,2f);
        }
    }
}
