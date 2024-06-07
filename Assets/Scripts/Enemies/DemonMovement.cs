using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Unity.VisualScripting;
using System;

public class DemonMovement : BaseEnemy
{
	int hIdles;
	int hAttack;
	private bool isAttacking = false;
	// Use this for initialization
	public override void Awake()
	{
		base.Awake();
	}
	void Start()
	{
		hIdles = Animator.StringToHash("Idles");
		hAttack = Animator.StringToHash("Attack");
	}

	// Update is called once per frame
	public void Update()
	{
		if (isAttacking) { return; }

		float dist = Vector3.Distance(transform.position, target.position);

        if (dist>agent.stoppingDistance)
		{
			agent.SetDestination(target.position);
		}
        else
        {
            agent.SetDestination(transform.position);
        }

		Vector3 direction = target.position - gameObject.transform.position;

		RaycastHit hit;
		if (Physics.Raycast(gameObject.transform.position, direction, out hit, agent.stoppingDistance + 1f))
		{
			if (hit.collider.gameObject.GetComponent<PlayerStatictics>())
			{
				Attack();
				hit.collider.gameObject.GetComponent<PlayerStatictics>().TakeDamage(dealDamage);
			}
		}
	}


	public void Attack()
	{
		StartCoroutine(C_Attack());
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idles"))
		{
			anim.SetBool(hIdles, false);
			anim.SetBool(hAttack, true);
		}
	}
	public IEnumerator C_Attack()
	{
		isAttacking = true;
		agent.SetDestination(transform.position);
		yield return new WaitForSeconds(2f);
		anim.SetBool(hIdles, true);
		anim.SetBool(hAttack, false);
		yield return new WaitForSeconds(1f);
		isAttacking = false;
	}
}