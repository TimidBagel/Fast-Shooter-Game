using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class BulletScript : MonoBehaviour
    {
        [Range(1, 100)]
        public float destroyAfter;
        public bool destroyOnImpact = false;
        public float minDestroyTime;
        public float maxDestroyTime;

        [Header("Impact Effect Prefabs")]
        public Transform[] metalImpactPrefabs;

        private void Start()
        {
            StartCoroutine(DestroyAfter());
        }

        public float fleshDamage;

        private void OnColliusionEnter(Collision collision)
        {
            if (!destroyOnImpact)
                StartCoroutine(DestroyTimer());
            else
                Destroy(gameObject);

            if (collision.transform.tag == "Metal")
            {
                Instantiate(metalImpactPrefabs[Random.Range(0, metalImpactPrefabs.Length)], transform.position,
                    Quaternion.LookRotation(collision.contacts[0].normal));
                Destroy(gameObject);
            }
            if (collision.gameObject.GetComponent<Target>() != null)
                collision.gameObject.GetComponent<Target>().TakeDamage(fleshDamage);

        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds
                (Random.Range(minDestroyTime, maxDestroyTime));
            Destroy(gameObject);
        }

        private IEnumerator DestroyAfter()
        {
            yield return new WaitForSeconds(destroyAfter);
            Destroy(gameObject);
        }
    }
}
