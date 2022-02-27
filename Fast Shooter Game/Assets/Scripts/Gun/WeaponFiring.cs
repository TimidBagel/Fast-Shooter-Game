using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFiring : MonoBehaviour
{
    Animator anim;
    EquipmentManager equipmentManager = EquipmentManager.equipmentManager;
    WeaponUI weaponUI = WeaponUI.weaponUIInstance;
    GunScript gunScript;

    public int magazineCapacity;
    public int magazineCarryingCapacity; // change this to be based on equipment
    public int currentLoadedMagazine;
    public int currentAmmo;
    public float reloadTime;
    public float reloadTimeMagEmpty;

    public int[] magazines; // change this to a magazine class eventually, though this might work
                            // if the WeaponFiring.cs script works individually with this

    private float timeTillFire;

    private bool isReloading = false;
    private bool canFire = true;

	private void Start()
	{
        gunScript = GetComponent<GunScript>();
        magazines = new int[magazineCarryingCapacity]; // you should be able to pick up magazines into inventory

        for (int i = 0; i < magazines.Length; i++)
            magazines[i] = magazineCapacity;

        UpdateInfo();

        anim = GetComponent<Animator>();
    }
	private void Update()
	{
        currentAmmo = magazines[currentLoadedMagazine];
		if (Input.GetMouseButtonDown(0) && equipmentManager.equippedWeapon.fireMode == "SINGLE FIRE" &&
            magazines[currentLoadedMagazine] > 0 && canFire)
		{
			FireWeapon();
		}
        if (Input.GetMouseButton(0) && equipmentManager.equippedWeapon.fireMode == "FULL AUTO" &&
            magazines[currentLoadedMagazine] > 0 && timeTillFire <= Time.time && canFire)
		{
            FireWeapon();
		}
        if (equipmentManager.equippedWeapon.fireMode == "SINGLE FIRE" && Input.GetKeyDown(KeyCode.B) && 
            equipmentManager.equippedWeapon.fullAutoCapable)
            equipmentManager.equippedWeapon.fireMode = "FULL AUTO";
        else if (equipmentManager.equippedWeapon.fireMode == "FULL AUTO" && Input.GetKeyDown(KeyCode.B))
            equipmentManager.equippedWeapon.fireMode = "SINGLE FIRE";

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
		{
            if (magazines.Length <= 1) return;
            StartCoroutine(Reload());
		}
    }
	private void FireWeapon()
	{
        Weapon weapon = equipmentManager.equippedWeapon;
        magazines[currentLoadedMagazine]--;

        UpdateInfo();

        timeTillFire = Time.time + 1f / weapon.rateOfFire;

        gunScript.shootAudioSource.clip = gunScript.soundClips.shootSound;
        gunScript.shootAudioSource.Play();

        gunScript.muzzleParticles.Emit(1);
        gunScript.sparkParticles.Emit(Random.Range(1, 6));

        StartCoroutine(gunScript.MuzzleFlashLight());

        Transform bullet = Instantiate(gunScript.prefabs.bulletPrefab,
            gunScript.spawnPoints.bulletSpawnPoint.transform.position,
            gunScript.spawnPoints.bulletSpawnPoint.transform.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * weapon.bulletForce;

        if (gunScript.slidePosition != null)
            gunScript.slidePosition.localPosition = gunScript.targetSlidePosition;

        Instantiate(gunScript.prefabs.casingPrefab,
            gunScript.spawnPoints.casingSpawnPoint.transform.position,
            gunScript.spawnPoints.casingSpawnPoint.transform.rotation);

        gunScript.cameraRecoilScript.RecoilFire();
        gunScript.gunRecoilScript.RecoilFire(gunScript.isAiming);
    }
    public void GetRidOfThis(GameObject objectToRemove)
	{
        Destroy(objectToRemove);
	}
    private void UpdateInfo()
	{
        weaponUI.UpdateAmmoOnly(magazines[currentLoadedMagazine], magazines.Length -1);
	}

    private IEnumerator Reload()
	{
        anim.Play($"{equipmentManager.equippedWeapon.name} reload");
        isReloading = true;
        canFire = false;
        currentAmmo = 0;
        yield return new WaitForSeconds(reloadTime);

        if (magazines[currentLoadedMagazine] <= 0)
		{
            int[] newMagazines = new int[magazines.Length - 1];
            int oldMagazineIndex = 0;
            int newMagazineIndex = 0;

			while (newMagazineIndex < newMagazines.Length)
			{
                if (magazines[oldMagazineIndex] == 0)
				{
                    oldMagazineIndex++;
                    continue;
				}

                newMagazines[newMagazineIndex++] = magazines[oldMagazineIndex++];
			}
            magazines = newMagazines;
        }
        currentLoadedMagazine++;

        if (currentLoadedMagazine >= magazines.Length)
            currentLoadedMagazine = 0;

        UpdateInfo();

        isReloading = false;
        canFire = true;
	}
}
