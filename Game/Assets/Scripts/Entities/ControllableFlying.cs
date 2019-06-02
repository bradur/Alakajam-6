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
    [SerializeField]
    GameObject Explosion;

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

    bool dead = false;

    private Vector2 worldMin, worldMax;

    private bool disableRotation = false;
    int GROUND;

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

        GameObject world = GameObject.FindWithTag("WorldConfines");
        BoxCollider collider = world.GetComponent<BoxCollider>();
        float width = world.transform.localScale.x * collider.size.x;
        float height = world.transform.localScale.y * collider.size.y;
        float centerx = world.transform.localScale.x * collider.center.x;
        float centery = world.transform.localScale.y * collider.center.y;
        worldMin = new Vector2(world.transform.position.x + centerx - width / 2, world.transform.position.y + centery - height / 2);
        worldMax = new Vector2(world.transform.position.x + centerx + width / 2, world.transform.position.y + centery + height / 2);

        GROUND = LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (body.velocity.magnitude > 1.0f && !disableRotation)
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

        if (!dead)
        {
            var speed = body.velocity.magnitude;

            body.AddForce(transform.right.normalized * throttle);
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
        } else {
            if (engineSoundSource != null && engineSoundSource.isPlaying)
            {
                engineSoundSource.Stop();
            }
        }

        body.AddForce(Vector2.down * 20);

        var x = transform.position.x;
        var y = transform.position.y;
        if (x < worldMin.x - 5)
        {
            body.velocity = new Vector2(30f, 0.6f);
            if (triplane.isUpsideDown())
            {
                triplane.Roll();
            }
        }
        if (x > worldMax.x + 5)
        {
            body.velocity = new Vector2(-30f, 0.6f);
            if (!triplane.isUpsideDown())
            {
                triplane.Roll();
            }
        }
        
        if (y > worldMax.y - 2)
        {
            throttle = 0;
        }
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
        if (!dead && withinConfines())
        {
            throttle += 10f * Time.fixedDeltaTime;
            throttle = Mathf.Min(throttle, maxThrottle);
        }
    }

    public void Decelerate()
    {
        if (!dead && withinConfines())
        {
            throttle -= 10f * Time.fixedDeltaTime;
            throttle = Mathf.Max(throttle, 0);
        }
    }

    public void RotateCW()
    {
        if (!dead && withinConfines())
        {
            body.velocity = Quaternion.AngleAxis(-rotateSpeed * Time.fixedDeltaTime, Vector3.forward) * body.velocity;
        }
    }

    public void RotateCW(float angle, AiMovementFinished callback, bool accelerate)
    {
        if (!dead && withinConfines())
        {
            aiRotate = true;
            aiRotateTarget = angle;
            aiAccelerate = accelerate;
            if (callback != null)
            {
                aiCallback = callback;
            }
        }
    }
    public void RotateCW(Vector3 target, AiMovementFinished callback, bool accelerate)
    {
        if (!dead && withinConfines())
        {
            aiRotate = true;
            aiRotateTargetV = target;
            aiAccelerate = accelerate;
            if (callback != null)
            {
                aiCallback = callback;
            }
        }
    }

    public void RotateCCW()
    {
        if (!dead && withinConfines())
        {
            body.velocity = Quaternion.AngleAxis(rotateSpeed * Time.fixedDeltaTime, Vector3.forward) * body.velocity;
        }
    }

    public void Roll()
    {
        if (!dead && withinConfines())
        {
            triplane.Roll();
        }
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

    public void Kill()
    {
        dead = true;
    }

    private bool withinConfines()
    {
        var x = transform.position.x;
        var y = transform.position.y;
        return x >= worldMin.x && x <= worldMax.x && y >= worldMin.y && y <= worldMax.y;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == GROUND)
        {
            if (dead && !disableRotation)
            {
                disableRotation = true;
                body.velocity = Vector2.zero;
                AudioPlayer.main.PlaySound(GameEvent.BombExplodes);
                GameObject xpl = Instantiate(Explosion);
                xpl.SetActive(true);
                xpl.transform.position = transform.position - Vector3.forward;
            }
        }
    }
}

public delegate bool AiMovementFinished();
