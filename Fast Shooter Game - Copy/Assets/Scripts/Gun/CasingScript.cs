using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class CasingScript : MonoBehaviour
    {
        [Header("Force X")]
        public float minimumXForce;
        public float maximumXForce;

        [Header("Foce Y")]
        public float minimumYForce;
        public float maximumYForce;

        [Header("Foce Z")]
        public float minimumZForce;
        public float maximumZForce;

        [Header("Rotation Force")]
        public float minimumRotation;
        public float maximumRotation;

        [Header("Despawn Timer")]
        public float despawnTime;

        [Header("Audio")]
        public AudioClip[] casingSounds;
        public AudioSource audioSource;

        [Header("Spin Settings")]
        public float speed = 2500.0f;

        private void Awake()
        {
            GetComponent<Rigidbody>().AddRelativeTorque(
                Random.Range(minimumRotation, maximumRotation),
                Random.Range(minimumRotation, maximumRotation),
                Random.Range(minimumRotation, maximumRotation)
                * Time.deltaTime);

            GetComponent<Rigidbody>().AddRelativeForce(
                Random.Range(minimumXForce, maximumXForce),
                Random.Range(minimumYForce, maximumYForce),
                Random.Range(minimumZForce, maximumZForce));
        }
        private void Start()
        {
            StartCoroutine(RemoveCasing());
            transform.rotation = Random.rotation;
            StartCoroutine(PlaySound());
        }

        private void FixedUpdate()
        {
            transform.Rotate(Vector3.right, speed * Time.deltaTime);
            transform.Rotate(Vector3.down, speed * Time.deltaTime);
        }

        private IEnumerator PlaySound()
        {
            yield return new WaitForSeconds(Random.Range(0.25f, 0.85f));
            audioSource.clip = casingSounds[Random.Range(0, casingSounds.Length)];
            audioSource.Play();
        }

        private IEnumerator RemoveCasing()
        {
            yield return new WaitForSeconds(despawnTime);
            Destroy(gameObject);
        }
    }
}
