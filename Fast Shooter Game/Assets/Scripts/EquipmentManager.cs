using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	#region singleton
	public static EquipmentManager equipmentManager;
	private void Awake()
	{
		if (equipmentManager != null)
		{
			Debug.LogWarning("MORE THAN ONE INSTANCE OF EQUIPMENT MANAGER DETECTED");
			return;
		}
		equipmentManager = this;
	}
	#endregion
	private Inventory inventory;
	public GameObject[] weaponObjects;
	public GameObject equippedInSlotObject;
	public Weapon equippedWeapon;

	private void Start()
	{
		inventory = GetComponent<Inventory>();
		weaponObjects = new GameObject[3];
	}
	private void Update()
	{
		#region an idiotic mess
		if (Input.GetKeyDown(KeyCode.Alpha1) && weaponObjects[0] != null)
		{
			UnequipWeapon();
			EquipWeapon(inventory.GetItem(0));
		}
		//else if (Input.GetKeyDown(KeyCode.Alpha1))
		//{
		//	if (weaponObjects[0] == null || weaponObjects[0] != inventory.GetItem(0).prefab)
		//		{
		//		InstantiateWeapon(inventory.GetItem(0).prefab, 0);
		//		if (equippedInSlotObject != null)
		//			Destroy(equippedInSlotObject);
		//		EquipWeapon(inventory.GetItem(0));
		//	}		
		//}
		if (Input.GetKeyDown(KeyCode.Alpha2) && weaponObjects[1] != null)
		{
			UnequipWeapon();
			EquipWeapon(inventory.GetItem(1));
		}
		//else if (Input.GetKeyDown(KeyCode.Alpha2))
		//{
		//	if (weaponObjects[1] == null || weaponObjects[1] != inventory.GetItem(0).prefab)
		//	{
		//		InstantiateWeapon(inventory.GetItem(1).prefab, 1);
		//		if (equippedInSlotObject != null)
		//			Destroy(equippedInSlotObject);
		//		EquipWeapon(inventory.GetItem(1));
		//	}
		//}
		if (Input.GetKeyDown(KeyCode.Alpha3) && weaponObjects[2] != null)
		{
			UnequipWeapon();
			EquipWeapon(inventory.GetItem(2));
		}
		//else if (Input.GetKeyDown(KeyCode.Alpha3))
		//{
		//	if (weaponObjects[2] == null || weaponObjects[2] != inventory.GetItem(0).prefab)
		//	{
		//		InstantiateWeapon(inventory.GetItem(2).prefab, 2);
		//		if (equippedInSlotObject != null)
		//			Destroy(equippedInSlotObject);
		//		EquipWeapon(inventory.GetItem(2));
		//	}
		//}
		#endregion
	}

	//private void InstantiateWeapon(GameObject weaponObject, int weaponStyle)
	//{
	//	equippedWeapon = inventory.GetItem(weaponStyle);
	//	if (previousWeapon != equippedWeapon && equippedWeapon != null)
	//	{
	//		weaponObjects[weaponStyle] = Instantiate(weaponObject, weaponContainer);
	//		Debug.Log("Instantied a weapon");
	//	}
	//	previousWeapon = inventory.GetItem(weaponStyle);

	//}
	private void EquipWeapon(Weapon weapon)
	{
		weaponObjects[(int)weapon.weaponStyle].SetActive(true);
		WeaponFiring thisWeapon = weaponObjects[(int)weapon.weaponStyle].GetComponent<WeaponFiring>();
		inventory.weaponUI.UpdateInfo(weapon.icon, thisWeapon.currentAmmo, 
			thisWeapon.magazines.Length - 1, weapon.iconSize);

		equippedWeapon = inventory.GetItem((int)weapon.weaponStyle);
		equippedInSlotObject = weaponObjects[(int)weapon.weaponStyle];
	}
	private void UnequipWeapon()
	{
		if (equippedInSlotObject != null)
			equippedInSlotObject.SetActive(false);
	}
	// inventory.weaponUI.UpdateInfo(weapon.icon, weapon.magazineSize, weapon.magazineCount, weapon.iconSize);
}
