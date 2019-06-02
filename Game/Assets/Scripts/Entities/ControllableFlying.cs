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
    [SerializeField]
    private RuntimeVector3 entityPosition;

    public float throttle = 5f;
    float maxThrottle = 15f;
    float gravity = 0f;
    Quaternion startRot;
    float startTime;
    float dropAcc = -1;
    float damp = 0.99f;

    bool firstTick = true;

    bool aiAccelerate = false;
    bool aiRotate = false;
    float aiRotateTarget = 0f; //angle
    Vector3 aiRotateTargetV;
    AiMovementFinished aiCallback = null;

    Triplane triplane;

    [SerializeField]
    private float engineSoundPitch = 0.25f;
    private AudioSource engineSoundSource;
    private float originalEngineSoundvolume;


    [SerializeField]
    private RuntimeBool muteSounds;

    // Start is called before the first frame update
    void Start()
    {
        throttle = 12f;
        engineSoundSource = GetComponent<AudioSource>();
        if (engineSoundSource != null) {
            originalEngineSoundvolume = engineSoundSource.volume;
        }
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
        Debug.DrawLine(transform.position, transform.position + aiRotateTargetV, Color.blue);
        if (engineSoundSource != null)
        {
            if (muteSounds.Toggle) {
                engineSoundSource.volume = 0f;
            } else if (engineSoundSource.volume == 0) {
                engineSoundSource.volume = originalEngineSoundvolume;
            }
        }
    }

    private void FixedUpdate()
    {
        if (firstTick)
        {
            firstTick = false;
            //body.velocity = Vector3.right * throttle;
            body.velocity = new Vector2(30f, 0.6f);
        }

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

        if (entityPosition != null)
        {
            entityPosition.Value = transform.position;
        }
        if (engineSoundSource != null)
        {
            engineSoundSource.pitch = speed * engineSoundPitch;
        }
        runAIMovements();
        //Debug.Log(lift + ", " + speed + ", " + throttle + ", " + transform.up.normalized.y + ", " + speed);
    }

    private void runAIMovements()
    {
        if (aiRotate)
        {
            float angleDiff = Vector3.SignedAngle(aiRotateTargetV, body.velocity, Vector3.forward);

            Debug.Log(angleDiff + ", " + body.velocity + ", " + aiRotateTargetV + ", " + Vector3.Distance(getVec3(body.velocity), aiRotateTargetV));
            Debug.DrawLine(transform.position, transform.position + aiRotateTargetV);
            if (Vector3.Distance(getVec3(body.velocity), aiRotateTargetV) < 1f)
            {
                aiCallback();
                aiRotate = false;
            }
        }
    }

    public void Accelerate()
    {
        throttle += 10f * Time.fixedDeltaTime;
        throttle = Mathf.Min(throttle, maxThrottle);
    }

    public void Decelerate()
    {
        throttle -= 10f * Time.fixedDeltaTime;
        throttle = Mathf.Max(throttle, 0);
    }

    public void RotateCW()
    {
        body.velocity = Quaternion.AngleAxis(-rotateSpeed * Time.fixedDeltaTime, Vector3.forward) * body.velocity;
    }

    public void RotateCW(float angle, AiMovementFinished callback, bool accelerate)
    {
        aiRotate = true;
        aiRotateTarget = angle;
        aiAccelerate = accelerate;
        if (callback != null)
        {
            aiCallback = callback;
        }
    }
    public void RotateCW(Vector3 target, AiMovementFinished callback, bool accelerate)
    {
        aiRotate = true;
        aiRotateTargetV = target;
        aiAccelerate = accelerate;
        if (callback != null)
        {
            aiCallback = callback;
        }
    }

    public void RotateCCW()
    {
        body.velocity = Quaternion.AngleAxis(rotateSpeed * Time.fixedDeltaTime, Vector3.forward) * body.velocity;
    }

    public void Roll()
    {
        triplane.Roll();
    }

    public bool isUpsideDown()
    {
        return triplane.isUpsideDown();
    }

    public Vector3 transformDown()
    {
        return triplane.isUpsideDown() ? transform.up : -transform.up;
    }

    private Vector3 getVec3(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }
}

public delegate bool AiMovementFinished();
