// Date   : 02.06.2019 17:07
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Radar : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;
    [SerializeField]
    Image img;

    [SerializeField]
    private RuntimeVector3 bossPosition;

    [SerializeField]
    private RuntimeBool bossVisible;

    private float buffer = 100f;

    private bool bossHasBeenSeen = false;
    Vector2 center;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (bossHasBeenSeen) {
            if (!bossVisible.Toggle)
            {
                img.enabled = true;

                transform.up = (Vector2)Camera.main.WorldToScreenPoint((Vector2)bossPosition.Value) - (Vector2)transform.position;
                /*
                center = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
                Vector2 pos = Camera.main.WorldToViewportPoint(bossPosition.Value);
                Debug.Log(pos);
                Debug.Log(center);
                pos.x *= buffer;
                pos.y *= buffer;

                rectTransform.anchoredPosition = pos;*/
                /*
                Vector2 lookPos = (Vector2) bossPosition.Value - center;
                float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                rectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
            } else {
                img.enabled = false;
            }
        } else if (bossVisible.Toggle) {
            bossHasBeenSeen = true;
        }
    }

}
