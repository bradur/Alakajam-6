// Date   : 01.06.2019 18:25
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {

    public static AudioPlayer main;

    void Awake() {
        main = this;
    }

    [SerializeField]
    private SoundMap soundMap;

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

    public void PlaySound(GameEvent gameEvent) {
        AudioClip clip = soundMap.GetAudioClip(gameEvent);
        soundSource.PlayOneShot(clip, 1f);
    }
}
