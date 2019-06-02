using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessStamp : MonoBehaviour
{
    [SerializeField]
    private Image stamp;
    [SerializeField]
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSuccess(bool value)
    {
        stamp.gameObject.SetActive(value);
    }

    public void EnableButton(bool enable)
    {
        button.enabled = enable;
    }
}
