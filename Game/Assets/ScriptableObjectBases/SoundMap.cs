// Date   : 01.06.2019 18:15
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameEvent
{
    None,
    BuildingExplode,
    BombDrops,
    BombExplodes,
    PlayerGunFires,
    EnemyGunFires
}

[CreateAssetMenu(fileName = "SoundMap ", menuName = "ScriptableObjects/New SoundMap")]
public class SoundMap : ScriptableObject
{

    [SerializeField]
    private string configName;
    public string Name { get { return configName; } }

    [SerializeField]
    private List<GameSound> sounds;

    public AudioClip GetAudioClip(GameEvent gameEvent)
    {
        foreach (GameSound gameSound in sounds)
        {
            if (gameSound.GameEvent == gameEvent)
            {
                if (gameSound.AudioClip != null)
                {

                    return gameSound.AudioClip;
                }
                else if (gameSound.AudioClips.Count > 0)
                {
                    return gameSound.AudioClips[Random.Range(0, gameSound.AudioClips.Count - 1)];
                }
            }
        }
        return null;
    }

}

[System.Serializable]
public class GameSound : System.Object
{
    public GameEvent GameEvent;
    public AudioClip AudioClip;
    public List<AudioClip> AudioClips;
}
