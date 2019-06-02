// Date   : 01.06.2019 18:25
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {

    public static AudioPlayer main;

    void Awake() {
        originalSoundVolume = soundSource.volume;
        soundsMuted.Toggle = false;
        main = this;
    }

    private float originalSoundVolume;

    [SerializeField]
    private SoundMap soundMap;

    [SerializeField]
    private RuntimeBool soundsMuted;

    [SerializeField]
    private AudioSource soundSource;
    [SerializeField]
    private AudioSource menuMusicSource;
    [SerializeField]
    private AudioSource gameMusicSource;

    public void FadeOutGameMusic() {

    }

    public void FadeInMenuMusic() {

    }

    public void FadeInGameMusic() {

    }

    public void FadeOutMenuMusic() {

    }

    public void StopMenuMusic(){
        menuMusicSource.Stop();
    }

    public void PlaySound(GameEvent gameEvent) {
        AudioClip clip = soundMap.GetAudioClip(gameEvent);
        if (clip != null) {
            soundSource.PlayOneShot(clip, 1f);
        }
    }

    void Update() {
        if (soundsMuted.Toggle) {
            soundSource.volume = 0f;
        } else if (soundSource.volume == 0f) {
            soundSource.volume = originalSoundVolume;
        }
    }
}
