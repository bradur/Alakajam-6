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

    private float width;
    private float height;
    private float buffer = 5f;

    private bool bossHasBeenSeen = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        width = rectTransform.sizeDelta.x;
        height = rectTransform.sizeDelta.y;
    }

    void Update()
    {
        if (bossHasBeenSeen) {
            if (!bossVisible.Toggle)
            {
                img.enabled = true;
                Vector3 pos = Camera.main.WorldToScreenPoint(bossPosition.Value);
                pos.x = Mathf.Clamp(pos.x, 0 + width + buffer, Screen.width - width - buffer);
                pos.y = Mathf.Clamp(pos.y, 0 + height + buffer, Screen.height - height - buffer);
                rectTransform.anchoredPosition = pos;
                Vector3 lookPos = transform.position - bossPosition.Value;
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
