// Date   : 01.06.2019 07:31
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameAction
{
    None,
    Shoot,
    DropBomb,
    SkipCutScene
}

[CreateAssetMenu(fileName = "HotkeyMap ", menuName = "ScriptableObjects/New HotkeyMap")]
public class HotkeyMap : ScriptableObject
{

    [SerializeField]
    private string configName;
    public string Name { get { return configName; } }


    [SerializeField]
    private List<GameKey> gameKeys = new List<GameKey>();

    public bool GetKeyDown(GameAction action)
    {
        foreach (KeyCode keyCode in GetKeyCodes(action))
        {
            if (Input.GetKeyDown(keyCode))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetKeyUp(GameAction action)
    {
        foreach (KeyCode keyCode in GetKeyCodes(action))
        {
            if (Input.GetKeyUp(keyCode))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetKey(GameAction action)
    {
        foreach (KeyCode keyCode in GetKeyCodes(action))
        {
            if (Input.GetKey(keyCode))
            {
                return true;
            }
        }
        return false;
    }

    public List<KeyCode> GetKeyCodes(GameAction action)
    {
        List<KeyCode> keyCodes = new List<KeyCode>();
        foreach (GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                keyCodes.Add(gameKey.key);
            }
        }
        return keyCodes;
    }
}

[System.Serializable]
public class GameKey : System.Object
{
    public KeyCode key;
    public GameAction action;
}
