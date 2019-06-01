using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableFlying : MonoBehaviour
{
    [SerializeField]
    GameObject childSprite;
    [SerializeField]
    Rigidbody2D body;
    Vector2 velocity;
    float direction;
    [SerializeField]
    float lift = 1f;
    [SerializeField]
    float rotateSpeed = 1f;
    [SerializeField]
    float maxVel;
    float speed = 0f;
    float maxSpeed = 20f;
    float gravity = 0f;
    Quaternion startRot;
    float startTime;
    float dropAcc = -1;
    float damp = 0.99f;
    bool accelerating;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (body.velocity.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(body.velocity, Vector3.up);
        }
        Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.red);
        Debug.DrawLine(transform.position, transform.position + new Vector3(body.velocity.x, body.velocity.y, 0));
    }

    private void FixedUpdate()
    {
        float gravityAcc = 9.81f / 2 * Time.fixedDeltaTime;
        if (speed > 5f)
        {
            gravity = 0;
        }
        else
        {
            gravity += gravityAcc;
        }

        gravity = gravity * Mathf.Max(0, (10 - speed) / 10);

        Vector2 v2 = body.velocity;
        if(v2.magnitude < 0.1f)
        {
            v2 = transform.forward.normalized;
        }

        velocity = v2.normalized * speed;
        body.velocity = velocity;
        body.velocity += Vector2.down * gravity;
        if (!accelerating)
        {
            //dampening
            //speed = speed - (speed * 1.5f * Time.fixedDeltaTime);
        }
        Debug.Log(speed + ", " + body.velocity);
        accelerating = false;
    }

    public void Accelerate()
    {
        Debug.Log("Accelerate");
        speed += 4f * Time.fixedDeltaTime;
        speed = Mathf.Min(speed, maxSpeed);
        accelerating = true;
    }

    public void Decelerate()
    {
        speed -= 4f * Time.fixedDeltaTime;
        speed = Mathf.Max(speed, 0);
    }

    public void RotateCW()
    {
        body.velocity = Quaternion.AngleAxis(-rotateSpeed * Time.fixedDeltaTime, Vector3.forward) * body.velocity;
    }

    public void RotateCCW()
    {
        body.velocity = Quaternion.AngleAxis(rotateSpeed * Time.fixedDeltaTime, Vector3.forward) * body.velocity;
    }
}
