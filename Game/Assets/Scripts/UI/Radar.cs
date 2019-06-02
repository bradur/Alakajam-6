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

    private float buffer = 250f;

    private bool bossHasBeenSeen = false;
    Vector3 center;

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

                center = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
                Vector2 pos = Camera.main.WorldToViewportPoint(bossPosition.Value);
                pos.x *= buffer;
                pos.y *= buffer;

                rectTransform.anchoredPosition = pos;
                Vector3 lookPos = bossPosition.Value - center;
                float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                rectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            } else {
                img.enabled = false;
            }
        } else if (bossVisible.Toggle) {
            bossHasBeenSeen = true;
        }
    }

}
