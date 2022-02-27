using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    #region singleton
    public static WeaponUI weaponUIInstance;

	private void Awake()
	{
		if (weaponUIInstance != null)
		{
			Debug.LogWarning("MORE THAN ONE INSTANCE OF WEAPONUI FOUND");
			return;
		}
		weaponUIInstance = this;
	}
	#endregion

	[SerializeField] private Image icon;
    [SerializeField] private Text magazineSizeText;
    [SerializeField] private Text magazineCountText;

	public void UpdateInfo(Sprite weaponIcon, int magazineSize, int magazineCount, Vector2 iconSize)
	{
        icon.sprite = weaponIcon;
        icon.rectTransform.sizeDelta = iconSize;
        magazineSizeText.text = magazineSize.ToString();
        magazineCountText.text = magazineCount.ToString();
	}
    public void UpdateAmmoOnly(int magazienSize, int magazineCount)
	{
        magazineSizeText.text = magazienSize.ToString();
        magazineCountText.text = magazineCount.ToString();
	}
}
