using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class BulletScript : MonoBehaviour
    {
        [Range(1, 100)]
        public float destroyAfter;
        public float fleshDamage;
        public bool destroyOnImpact = false;

        public float trailEnabletimer;

        [Header("Impact Effect Prefabs")]
        public Transform[] metalImpactPrefabs;

        private void Start()
        {
            StartCoroutine(DestroyAfter());
            StartCoroutine(TrailEnableTimer());
        }

		private void OnCollisionEnter(Collision collision)
		{
            Debug.Log("Collided with " + collision.transform.name);
            Target target = collision.transform.GetComponent<Target>();
            if (target != null && collision.transform.tag == "Target")
			{
				target.TakeDamage(fleshDamage);
				//GameObject particles = Instantiate(target.bloodParticles, transform.position, Quaternion.identity);
				//Destroy(particles, target.bloodParticleSystem.main.duration); // for blood particles
			}
            else if (target != null && collision.transform.tag == "Standing Target")
                target.anim.SetTrigger("Target Hit");
            Destroy(gameObject);
		}

		private IEnumerator TrailEnableTimer()
		{
            yield return new WaitForSeconds(trailEnabletimer);
            GetComponent<TrailRenderer>().enabled = true;
		}

        private IEnumerator DestroyAfter()
        {
            yield return new WaitForSeconds(destroyAfter);
            Destroy(gameObject);
        }
    }
}
