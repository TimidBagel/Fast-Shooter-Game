using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
	public static GameManager gameManager = GameManager.gameManagerInstance;

	public enum Sound
	{
		rifle_shot,
		rifle_shot_supressed,
		pistol_shot,
		pistol_shot_supressed,
		shotgun_shot,
		walk,
		walking,
		run,
		running,
	}
	public static void PlaySound(Sound sound, bool pitchChange)
	{
		GameObject soundGameObject = new GameObject("Sound");
		AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
		if (pitchChange) { audioSource.pitch = Random.Range(0.5f, 1); }
		audioSource.clip = GetAudioClip(sound);
		audioSource.volume = GetAudioVolume(sound);
		audioSource.PlayOneShot(audioSource.clip);
		Object.Destroy(soundGameObject, audioSource.clip.length);
	}

	private static AudioClip GetAudioClip(Sound sound)
	{
		foreach (GameManager.AudioClips audioClip in gameManager.sounds)
		{
			if (audioClip.sound == sound)
				return audioClip.audioClip;
		}
		Debug.LogError($"Audio Clip '{sound}' not found!");
		return null;
	}

	private static float GetAudioVolume(Sound sound)
	{
		foreach (GameManager.AudioClips audioClip in gameManager.sounds)
		{
			if (audioClip.sound == sound)
				return audioClip.volume;
		}
		return 0.0f;
	}
}
