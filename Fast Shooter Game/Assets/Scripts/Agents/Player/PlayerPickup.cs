using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
	[SerializeField] private float pickupRange = 2;
	[SerializeField] LayerMask pickupLayer;

	private Camera cam;
	private Inventory playerInventory;

	private void Start()
	{
		cam = Camera.main;
		playerInventory = GetComponent<Inventory>();
	}
	private void Update()
	{
		// something here with a crosshair indication only when able to pickup soemthing
		if (Input.GetKeyDown(KeyCode.F))
		{
			Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
			{
				Debug.Log($"Raycast hit '{hit.transform.name}'");
				// change this to support other item types later :)
				Item newItem = hit.transform.GetComponent<ItemObject>().itemSelf;
				if (newItem.type == "Weapon")
					playerInventory.AddItem(newItem as Weapon);
				Destroy(hit.transform.gameObject);
			}
		}
	}
}
