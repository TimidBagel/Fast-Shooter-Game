using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform gunPosition;
    public Transform otherGunPosition;

    GameManager gameManager;

    [Header("Weapon Sway")]
    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.06f;
    public float swaySmoothValue = 4.0f;
    private Vector3 initialSwayPosition;

    [Header("Weapon Settings")]
    public Transform WeaponContainer;
    public Vector3 pointPosition;
    private float timeTillFire;

    public float reloadTime;
    public float reloadTimeEmpty;

    private bool canReload = true;

    public float sliderBackTimer = 1.58f;
    private bool hasStartedSliderBack;

    private bool isReloading;

    public bool isAiming;

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
    public bool hasOptic;
    public Transform Lens;
    public Vector3 lensTargetPosition;
    public Vector3 opticAimPosition;
    public Vector3 redDotAimPosition;
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

    WeaponUI weaponUI = WeaponUI.weaponUIInstance;

    private void Start()
    {
        initialSwayPosition = transform.localPosition;

        WeaponContainer = GameObject.FindGameObjectWithTag("Weapon Container").transform;
        gunPosition = GameObject.FindGameObjectWithTag("Weapon Holder").transform;
        otherGunPosition = GameObject.FindGameObjectWithTag("Second Weapon Holder").transform;

        gunRecoilScript = GetComponent<GunRecoil>();
        cameraRecoilScript = GetComponentInParent<CameraRecoil>();

        shootAudioSource = GetComponent<AudioSource>();

        gameManager = GameManager.gameManagerInstance;

        prefabs.bulletPrefab = gameManager.BigBulletPrefab;
        prefabs.casingPrefab = gameManager.BigCasingPrefab;
    }

    private void LateUpdate()
    {
        float movementX = -Input.GetAxis("Mouse X") * swayAmount;
        float movementY = -Input.GetAxis("Mouse Y") * swayAmount;
        movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
        movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);
        Vector3 finalSwayPosition = new Vector3 (movementX, movementY, 0);
        otherGunPosition.localPosition = Vector3.Lerp(otherGunPosition.localPosition,
            finalSwayPosition + initialSwayPosition, Time.deltaTime * swaySmoothValue);
    }

    private void Update()
    {
        WeaponContainer.localPosition = pointPosition;

        if (Input.GetButton("Fire2") && !isReloading)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, aimFov, fovSpeed * Time.deltaTime);
            isAiming = true;

            if (!hasOptic)
            {
                if (gunPosition.localPosition != targetAimPosition)
                    gunPosition.localPosition = Vector3.Lerp(gunPosition.localPosition,
                      targetAimPosition, aimSpeed * Time.deltaTime);
            }
            else
			{
                gunPosition.localPosition = Vector3.Lerp(gunPosition.localPosition,
                    redDotAimPosition, aimSpeed * Time.deltaTime);
                if ( Lens != null && Lens.localPosition != lensTargetPosition)
                    Lens.localPosition = Vector3.Lerp(Lens.localPosition,
                        lensTargetPosition, aimSpeed * Time.deltaTime);
            }
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, defaultFov, fovSpeed * Time.deltaTime);
            gunPosition.localPosition = Vector3.Lerp(gunPosition.localPosition,
                Vector3.zero, aimReturnSpeed * Time.deltaTime);
            if (Lens != null)
                Lens.localPosition = Vector3.Lerp(Lens.localPosition,
                    Vector3.zero, aimSpeed * Time.deltaTime);

            isAiming = false;
        }

        if (slidePosition != null && slidePosition.localPosition != Vector3.zero)
            slidePosition.localPosition = Vector3.Lerp(slidePosition.localPosition,
                Vector3.zero, slideReturnSpeed * Time.deltaTime);

        //if (Input.GetKeyDown(KeyCode.R) && canReload)
        //    StartCoroutine(Reload(false));
    }
    public IEnumerator MuzzleFlashLight()
    {
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(lightDuration);
        muzzleFlashLight.enabled = false;
    }

  //  private IEnumerator Reload(bool empty)
  //  {
  //      Weapon weapon = EquipmentManager.equipmentManager.equippedWeapon;
  //      if (weapon.magazineCount <= 0)
		//{
  //          Debug.Log("No more magazines");
  //          yield return new WaitForSeconds(0);
		//}
		//else
		//{
  //          canFire = false;
  //          canReload = false;

  //          // some reload thing

  //          yield return new WaitForSeconds(reloadTime);
  //          weapon.currentAmmo = weapon.maxAmmo;
  //          weapon.magazineCount -= 1;
  //          weaponUI.UpdateAmmoOnly(weapon.currentAmmo, weapon.magazineCount);
  //          canReload = true;
  //          canFire = true;
  //      }
  //  }
}
