using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Target
{
	NavMeshAgent nm;
	public Transform target;
	bool alive = true;

	private void Start()
	{
		nm = GetComponent<NavMeshAgent>();
		nm.SetDestination(target.position);
	}

	private void Update()
	{
		if (alive)
			nm.SetDestination(target.position);
	}

	public override void Die()
	{
		base.Die();
		alive = false;
		Rigidbody rb = gameObject.AddComponent<Rigidbody>();
		nm.enabled = false;
		rb.AddTorque(30f, 0, 0);
	}
}
