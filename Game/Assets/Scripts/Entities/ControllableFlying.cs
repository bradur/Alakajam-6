using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableFlying : MonoBehaviour
{
    [SerializeField]
    GameObject childSprite;
    [SerializeField]
    Rigidbody2D body;
    float direction;
    [SerializeField]
    float lift = 1f;
    [SerializeField]
    float rotateSpeed = 1f;
    [SerializeField]
    float maxVel;
    float speed = 5f;
    float maxSpeed = 20f;
    float gravity = 0f;
    Quaternion startRot;
    float startTime;
    float dropAcc = -1;
    float damp = 0.99f;

    bool firstTick = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (body.velocity.magnitude > 0.01f)
        {
            float angleDiff = Vector3.SignedAngle(transform.right, body.velocity, Vector3.forward);
            transform.Rotate(Vector3.forward, angleDiff);
        }
        Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.red);
        Debug.DrawLine(transform.position, transform.position + new Vector3(body.velocity.x, body.velocity.y, 0));
    }

    private void FixedUpdate()
    {
        if (firstTick)
        {
            firstTick = false;
            body.velocity = Vector3.right * speed;
        }

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
        
        body.velocity = body.velocity.normalized * speed;
        body.velocity += Vector2.down * gravity;


    }

    public void Accelerate()
    {
        Debug.Log("Accelerate");
        speed += 4f * Time.fixedDeltaTime;
        speed = Mathf.Min(speed, maxSpeed);
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
