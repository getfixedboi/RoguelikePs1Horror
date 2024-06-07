using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : BaseEnemy
{
    [SerializeField] private float attackCooldown;
    private bool isAttacking = false;

    public override void Awake()
    {
        base.Awake();

        agent.updateRotation = false;
    }
    public void Update()
    {
        if (isAttacking || isDead) { return; }

        float dist = Vector3.Distance(transform.position, target.position);

        if (dist>agent.stoppingDistance)
		{
			agent.SetDestination(target.position);
		}
        else
        {
            agent.SetDestination(transform.position);
        }

        Vector3 _direction = target.position - transform.position;
        float angle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle + 180f, 0f);

        if (agent.remainingDistance >= agent.stoppingDistance && !agent.pathPending)
        {
            anim.Play("walk");
        }
        else if (!target)
        {
            anim.Play("idle");
        }

        if (agent.remainingDistance == 0)
        {
            return;
        }

        if(isAttacking)
        {
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Attack();
            target.GetComponent<PlayerStatictics>().TakeDamage(dealDamage);

        }
    }
    private void Attack() => StartCoroutine(C_Attack());
    private IEnumerator C_Attack()
    {
        isAttacking = true;
        agent.SetDestination(transform.position);
        anim.Play("attack");
        yield return new WaitForSeconds(attackCooldown + 0.1f);
        isAttacking = false;
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (isDead)
        {
            anim.Play("death");
        }
        else
        {
            anim.Play("take damage");
        }
    }
}
