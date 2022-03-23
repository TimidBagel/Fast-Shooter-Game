using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region singleton
	public static GameManager gameManagerInstance;
	private void Awake()
	{
		if (gameManagerInstance != null)
		{
			Debug.LogWarning("MORE THAN ONE INSTANCE OF GAME MANAGER FOUND");
			return;
		}
		else
			gameManagerInstance = this;
	}
	#endregion

	// do some automation stuff for easier gun setup here. that would be nice :)
	public Transform BigBulletPrefab;
	public Transform BigCasingPrefab;
	public AudioClip shootAudioClip; // make support for more sound effects later please :)

	[System.Serializable]
	public class AudioClips
	{
		public AudioManager.Sound sound;
		public AudioClip audioClip;
		public float volume;
	}
	public AudioClips audioClips;
	public AudioClips[] sounds;
}
