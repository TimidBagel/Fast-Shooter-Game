using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : Item
{
    public GameObject prefab;
    public float bulletForce;
    public WeaponType weaponType;
    public WeaponStyle weaponStyle;

    [Header("Weapon Settings")]
    public string fireMode = "SINGLE FIRE";
    public bool fullAutoCapable = false;
    public float rateOfFire;

    WeaponUI weaponUI = WeaponUI.weaponUIInstance;
	public override void Use()
	{
		base.Use();
	}
}

public enum WeaponType { Melee, Pistol, Assault_Rifle, Shotgun, Sniper }
public enum WeaponStyle { Primary, Secondary, Melee }
