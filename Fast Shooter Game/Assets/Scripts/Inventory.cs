using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
	[SerializeField] private GameObject[] weaponObjects;
	[SerializeField] public WeaponUI weaponUI;
	[SerializeField] private Transform weaponContainer;

	#region singleton
	public static Inventory inventory;
	private void Awake()
	{
		if (inventory != null)
		{
			Debug.LogWarning("MORE THAN ONE INSTANCE OF INVENTORY FOUND");
			return;
		}
		inventory = this;
	}
	#endregion

	private void Start()
	{
		InitVariables();
		weaponContainer = GameObject.FindGameObjectWithTag("Rotation Point").transform;
	}

	public void AddItem(Weapon newItem)
	{
		int newItemIndex = (int)newItem.weaponStyle;

		if (weapons[newItemIndex] != null)
		{
			RemoveItem(newItemIndex);
		}
		weapons[newItemIndex] = newItem;
		weaponObjects[newItemIndex] = Instantiate(newItem.prefab, weaponContainer);
		EquipmentManager.equipmentManager.weaponObjects[newItemIndex] = weaponObjects[newItemIndex];
	}

	public void RemoveItem(int index)
	{
		weaponObjects[index].GetComponent<WeaponFiring>().GetRidOfThis(weaponObjects[index]);
		weapons[index] = null;
	}

	public Weapon GetItem(int index)
	{
		return weapons[index];
	}

	private void InitVariables()
	{
		weapons = new Weapon[3]; // primary, secondary, melee
		weaponObjects = new GameObject[3];
	}
}
