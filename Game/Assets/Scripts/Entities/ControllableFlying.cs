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
    float throttle = 5f;
    float maxThrottle = 15f;
    float gravity = 0f;
    Quaternion startRot;
    float startTime;
    float dropAcc = -1;
    float damp = 0.99f;

    bool firstTick = true;

    Triplane triplane;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        triplane = GetComponentInChildren<Triplane>();
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
            body.velocity = Vector3.right * throttle;
        }

        /*
        var speed = body.velocity.magnitude;

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
        
        body.velocity = body.velocity.normalized * throttle;
        body.velocity += Vector2.down * gravity;
        */


        if (body.velocity.magnitude > 80)
        {
            body.velocity = body.velocity.normalized * 80;
        }

        var speed = body.velocity.magnitude;

        body.AddForce(transform.right.normalized * throttle);
        body.AddForce(Vector2.down * 20);
        var upsideDownFactor = triplane.isUpsideDown() ? -1.0f : 1.0f;
        float lift = Mathf.Max(0, Mathf.Min(20, transform.up.normalized.y * speed * upsideDownFactor));
        body.AddForce(Vector2.up * lift);

        Debug.Log(lift + ", " + speed + ", " + throttle + ", " + transform.up.normalized.y + ", " + speed);
    }

    public void Accelerate()
    {
        Debug.Log("Accelerate");
        throttle += 4f * Time.fixedDeltaTime;
        throttle = Mathf.Min(throttle, maxThrottle);
    }

    public void Decelerate()
    {
        throttle -= 4f * Time.fixedDeltaTime;
        throttle = Mathf.Max(throttle, 0);
    }

    public void RotateCW()
    {
        body.velocity = Quaternion.AngleAxis(-rotateSpeed * Time.fixedDeltaTime, Vector3.forward) * body.velocity;
    }

    public void RotateCCW()
    {
        body.velocity = Quaternion.AngleAxis(rotateSpeed * Time.fixedDeltaTime, Vector3.forward) * body.velocity;
    }

    public void Roll()
    {
        triplane.Roll();
    }
}
