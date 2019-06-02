using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectInitializer : MonoBehaviour
{
    public static ScriptableObjectInitializer Instance { get; private set; }
    [SerializeField]
    private RuntimeInt LevelsFinished;
    [SerializeField]
    private int defaultLevelsFinished;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LevelsFinished.Count = defaultLevelsFinished;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
