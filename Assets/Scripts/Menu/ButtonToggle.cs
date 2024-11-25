using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonToggle: MonoBehaviour {
  public Button musicButton;
  public Button soundButton;
  public Sprite musicOnSprite;
  public Sprite musicOffSprite;
  public Sprite soundOnSprite;
  public Sprite soundOffSprite;

  private bool isMusicOn = true;
  private bool isSoundOn = true;

  public void Start() {
    isMusicOn = PlayerPrefs.GetInt("MusicState", 1) == 1;
    isSoundOn = PlayerPrefs.GetInt("SoundState", 1) == 1;
    UpdateMusicButton();
    UpdateSoundButton();
    AudioManager.Instance.SetMusicState(isMusicOn);
    AudioManager.Instance.SetSoundState(isSoundOn);
    soundButton.onClick.AddListener(ToggleSound);
    musicButton.onClick.AddListener(ToggleMusic);

    Debug.Log("Start the music");
  }

  public void ToggleMusic() {
    Debug.Log("Music " + isMusicOn);
    isMusicOn = !isMusicOn;
    AudioManager.Instance.SetMusicState(isMusicOn);
    PlayerPrefs.SetInt("MusicState", isMusicOn ? 1 : 0);
    UpdateMusicButton();
  }

  public void ToggleSound() {
    isSoundOn = !isSoundOn;
    AudioManager.Instance.SetSoundState(isSoundOn);
    PlayerPrefs.SetInt("SoundState", isSoundOn ? 1 : 0);
    UpdateSoundButton();
  }

  private void UpdateMusicButton() {
    musicButton.GetComponent < Image > ().sprite = isMusicOn ? musicOnSprite : musicOffSprite;
  }

  public void UpdateSoundButton() {
    soundButton.GetComponent < Image > ().sprite = isSoundOn ? soundOnSprite : soundOffSprite;
  }

  public void PlayGame() {
    SceneManager.LoadScene("SampleScene");
  }

}