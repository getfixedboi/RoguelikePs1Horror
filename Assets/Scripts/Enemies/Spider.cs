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

		agent.SetDestination(target.position);
        
        Vector3 _direction = target.position - transform.position;
        float angle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle + 180f, 0f);

        if(agent.remainingDistance >= agent.stoppingDistance && !agent.pathPending)
        {
            anim.Play("walk");
        }
        else if (!target)
        {
            anim.Play("idle");
        }
        

		Vector3 direction = target.position - gameObject.transform.position;

		RaycastHit hit;
		if (Physics.Raycast(gameObject.transform.position, direction, out hit, agent.stoppingDistance + 1f))
		{
			if (hit.collider.gameObject.GetComponent<PlayerStatictics>())
			{
				Attack();
				hit.collider.gameObject.GetComponent<PlayerStatictics>().TakeDamage(20);
			}
		}
    }
    private void Attack()=>StartCoroutine(C_Attack());
    private IEnumerator C_Attack()
    {
        isAttacking = true;
        anim.Play("attack");
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if(isDead)
        {
            anim.Play("death");
        }
        else
        {
            anim.Play("take damage");
        }
    }
}
