using UnityEngine;

public class AudioManager: MonoBehaviour {
  public static AudioManager Instance;

  public AudioSource backgroundMusic;
  public AudioSource soundEffectsSource;


  public AudioClip[] soundEffectsClips; 
  void Awake() {
    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
  }

  public void SetMusicState(bool state) {
    if (backgroundMusic.clip != null && !backgroundMusic.isPlaying) {
      backgroundMusic.loop = true; 
      backgroundMusic.Play();
    }
    if (backgroundMusic != null) {
      backgroundMusic.mute = !state;
    }
  }

  public void SetSoundState(bool state) {
    if (soundEffectsSource != null) {
      soundEffectsSource.mute = !state;
    }
  }

  public void PlaySoundEffect(int index) {
    bool isSoundOn = PlayerPrefs.GetInt("SoundState", 1) == 1;
    if (isSoundOn && soundEffectsClips.Length > index) {
      soundEffectsSource.PlayOneShot(soundEffectsClips[index]);
    }
  }
}