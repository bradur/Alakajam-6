using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectInitializer : MonoBehaviour
{
    [SerializeField]
    private RuntimeInt LevelsFinished;
    [SerializeField]
    private int defaultLevelsFinished;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LevelsFinished.Count = defaultLevelsFinished;
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
