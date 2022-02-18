using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform gunPosition;
    public Transform otherGunPosition;

    [Header("Weapon Sway")]
    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.06f;
    public float swaySmoothValue = 4.0f;
    private Vector3 initialSwayPosition;

    [Header("Weapon Settings")]
    public float rateOfFire;
    private float timeTillFire;

    public float reloadTime;
    public float reloadTimeEmpty;

    private bool canReload = true;

    public float sliderBackTimer = 1.58f;
    private bool hasStartedSliderBack;

    private bool isReloading;

    private bool isAiming;

    public int currentAmmo;
    public int maxAmmo;
    private bool outOfAmmo;

    [Header("Bullet Settings")]
    public float bulletForce = 400f;

    [Header("Muzzle Flash Settings")]
    public bool randomMuzzleFlash = false;
    private int minRandomValue;

    [Range(2, 25)]
    public int maxRandomValue = 5;

    private int randomMuzzleFlashValue;

    public ParticleSystem muzzleParticles;
    public ParticleSystem sparkParticles;
    public int minSparkEmission = 1;
    public int maxSparkEmission = 7;

    [Header("Muzzle Flash Light Settings")]
    public Light muzzleFlashLight;
    public float lightDuration = 0.02f;

    [Header("Audio Source")]
    public AudioSource mainAudioSource;
    public AudioSource shootAudioSource;

    public Transform slidePosition;

    [Header("Slide Settings")]
    public Vector3 targetSlidePosition;
    public float slideTravelSpeed;
    public float slideReturnSpeed;

    [Header("Aiming Settings")]
    public Vector3 targetAimPosition;
    public float aimSpeed;
    public float aimReturnSpeed;
    public float aimFov;
    public float defaultFov;
    public float fovSpeed;

    [System.Serializable]
    public class Prefabs
    {
        [Header("Prefabs")]
        public Transform bulletPrefab;
        public Transform casingPrefab;
    }
    public Prefabs prefabs;

    [System.Serializable]
    public class SpawnPoints
    {
        [Header("Spawn Points")]
        public Transform bulletSpawnPoint;
        public Transform casingSpawnPoint;
    }
    public SpawnPoints spawnPoints;

    [System.Serializable]
    public class SoundClips
    {
        public AudioClip shootSound;
    }
    public SoundClips soundClips;

    public CameraRecoil cameraRecoilScript;
    public GunRecoil gunRecoilScript;

    public bool canFire = true;

    private void Awake()
    {
        currentAmmo = maxAmmo;

        muzzleFlashLight.enabled = false;
    }

    private void Start()
    {
        initialSwayPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        float movementX = -Input.GetAxis("Mouse X") * swayAmount;
        float movementY = -Input.GetAxis("Mouse Y") * swayAmount;
        movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
        movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);
        Vector3 finalSwayPosition = new Vector3(movementX, movementY, 0);
        otherGunPosition.localPosition = Vector3.Lerp(otherGunPosition.localPosition, 
            finalSwayPosition + initialSwayPosition,  Time.deltaTime * swaySmoothValue);
    }

    private void Update()
    {
        if (Input.GetButton("Fire2") && !isReloading)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, aimFov, fovSpeed * Time.deltaTime);
            isAiming = true;

            if (gunPosition.localPosition != targetAimPosition)
                gunPosition.localPosition = Vector3.Lerp(gunPosition.localPosition, 
                    targetAimPosition, aimSpeed * Time.deltaTime);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, defaultFov, fovSpeed * Time.deltaTime);
            gunPosition.localPosition = Vector3.Lerp(gunPosition.localPosition, 
                Vector3.zero, aimReturnSpeed * Time.deltaTime);

            isAiming = false;
        }

        if (currentAmmo == 0)
            outOfAmmo = true;
        else
            outOfAmmo = false;

        if (Input.GetButton("Fire1") && !outOfAmmo && Time.time >= timeTillFire && canFire)
        {
            currentAmmo -= 1;

            Debug.Log("Got here");

            timeTillFire = Time.time + 1f / rateOfFire;

            shootAudioSource.clip = soundClips.shootSound;
            shootAudioSource.Play();

            muzzleParticles.Emit(1);
            sparkParticles.Emit(Random.Range(1, 6));

            StartCoroutine(MuzzleFlashLight());

            var bullet = (Transform)Instantiate(prefabs.bulletPrefab,
                spawnPoints.bulletSpawnPoint.transform.position,
                spawnPoints.bulletSpawnPoint.transform.rotation);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletForce;

            Instantiate(prefabs.casingPrefab, 
                spawnPoints.casingSpawnPoint.transform.position,
                spawnPoints.casingSpawnPoint.transform.rotation);

            cameraRecoilScript.RecoilFire();
            gunRecoilScript.RecoilFire(isAiming);
        }

        if (Input.GetKeyDown(KeyCode.R) && canReload)
            StartCoroutine(Reload(false));
    }
    private IEnumerator MuzzleFlashLight()
    {
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(lightDuration);
        muzzleFlashLight.enabled = false;
    }

    private IEnumerator Reload(bool empty)
    {
        canFire = false;
        canReload = false;

        // some reload thing

        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        canReload = true;
        canFire = true;
    }
}
