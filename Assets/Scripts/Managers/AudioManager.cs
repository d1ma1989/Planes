using UnityEngine;

using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioSource _ambientMusicSource;
	[SerializeField] private AudioSource[] _soundEffectsSources;

	private readonly Dictionary<string, AudioClip> _loadedClips = new Dictionary<string, AudioClip>(); 

	public void PlayAmbientMusic(string clipName, bool loop = false)
	{
		_ambientMusicSource.loop = loop;
		_ambientMusicSource.clip = GetClip(clipName);
		_ambientMusicSource.Play();
	}

	public void PlaySoundEffect(string clipName)
	{
		AudioClip clip = GetClip(clipName);

		foreach (AudioSource source in _soundEffectsSources)
		{
			if (!source.isPlaying)
			{
				source.PlayOneShot(clip);
				return;
			}

			_soundEffectsSources[0].PlayOneShot(clip);
		}
	}

	private AudioClip GetClip(string clipName)
	{
		if (_loadedClips.ContainsKey(clipName))
		{
			return _loadedClips[clipName];
		}

		AudioClip clip = Resources.Load<AudioClip>("Sounds/" + clipName);
		_loadedClips[clipName] = clip;
		return clip;
	}
}

public class AudioClipsNames
{
	public const string GameOverMusic = "GameOverMusic";
	public const string Shot = "Shot";
	public const string Explosion = "Explosion";
	public const string DifficultyRaised = "DifficultyRaised";
	public const string MainTheme = "MainTheme";
	public const string StartMusic = "StartMusic";
}
