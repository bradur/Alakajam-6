using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePart : MonoBehaviour
{

    [SerializeField]
    Transform pivot;

    Rigidbody2D rb;

    bool active = false;
    float activateTime;
    Vector2 dist;
    float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dist = transform.position - pivot.position;
        activateTime = Time.time + 0.5f + dist.magnitude / 30;
        rotSpeed = dist.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (activateTime < Time.time)
        {
            active = true;
        }
        if (active)
        {
            transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (active)
        {
            rb.AddForce(Vector2.down * 10);
        }
    }
}
