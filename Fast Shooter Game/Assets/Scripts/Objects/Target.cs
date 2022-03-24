using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Animator anim;
	public float healthPoints;
	public ParticleSystem bloodParticleSystem;
	public GameObject bloodParticles;
	bool dead = false;

	private void Start()
	{
        anim = GetComponentInParent<Animator>();
	}

	public void TakeDamage(float damage)
    {
		if (dead)
			return;
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
			Die();
        }
    }

	public virtual void Die()
	{
		// behaviour for death in here, can be overriden
	}
}
