using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseAI : MonoBehaviour, Killable
{
    enum State
    {
        START,
        ENGAGE,
        ASCEND,
        BOMB,
        RUN
    }

    private State state = State.START;


    [SerializeField]
    private RuntimeVector3 playerPosition;

    private ControllableFlying plane;
    private ProjectileShooter shooter;
    private BombDropper bomber;

    private Vector2 targetDir;
    private float targetThrottle;
    private bool shoot;
    private bool bomb;

    private float lastRun = 0;
    private float updateStateTimer = -1.0f;

    private Rigidbody2D rb2D;

    private int GROUND;

    private float maxThrottle = 20,
        minThrottle = 7;

    Vector3 playerDir;
    float playerDist;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        plane = GetComponent<ControllableFlying>();
        shooter = GetComponentInChildren<ProjectileShooter>();
        bomber = GetComponentInChildren<BombDropper>();
        GetComponentInChildren<Triplane>().ParentPlane = this;
        GROUND = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {

        if (runAI())
        {
            Vector3 playerPos = playerPosition.Value;
            Vector3 curPos = transform.position;
            playerDir = playerPos - curPos;
            playerDist = playerDir.magnitude;

            updateState();
            commonRoutine();
            switch (state)
            {
                case State.START:
                    startRoutine();
                    break;
                case State.ENGAGE:
                    engageRoutine();
                    break;
                case State.ASCEND:
                    ascendRoutine();
                    break;
                case State.BOMB:
                    bombRoutine();
                    break;
                case State.RUN:
                    runRoutine();
                    break;
            }
            avoidanceRoutine();

            Debug.Log(state);
        }
    }

    private void FixedUpdate()
    {
        var angleDiff = Vector3.SignedAngle(transform.right, targetDir, Vector3.forward);

        if (angleDiff < -5.0f)
        {
            plane.RotateCW();
        }
        if (angleDiff > 5.0f)
        {
            plane.RotateCCW();
        }

        if (transform.right.x < 0 && !plane.isUpsideDown())
        {
            plane.Roll();
        }

        if (transform.right.x > 0 && plane.isUpsideDown())
        {
            plane.Roll();
        }
        
        if (plane.throttle < targetThrottle - 0.1f)
        {
            plane.Accelerate();
        }
        if (plane.throttle > targetThrottle + 0.1f)
        {
            plane.Decelerate();
        }
        
        if (shoot)
        {
            Projectile projectile = shooter.Shoot(transform.right);
            if (projectile != null)
            {
                AudioPlayer.main.PlaySound(GameEvent.EnemyGunFires);
            }
        }

        if (bomb)
        {
            bomber.Drop((Vector2)bomber.transform.position, rb2D.velocity);
            Projectile projectile = shooter.Shoot(transform.right);
            if (projectile != null)
            {
                AudioPlayer.main.PlaySound(GameEvent.EnemyGunFires);
            }
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private bool runAI()
    {
        if (lastRun + 0.1f < Time.time)
        {
            lastRun = Time.time;
            return true;
        }
        return false;
    }

    private void updateState()
    {
        if (playerDist < 5.0f)
        {
            state = State.RUN;
            updateStateTimer = Time.time + 1f;
        }

        if (updateStateTimer < 0)
        {
            updateStateTimer = Time.time + 5f;
        }
        if (updateStateTimer > Time.time)
        {
            return;
        }

        if (state == State.ASCEND && playerDir.y + 5 < 0)
        {
            state = State.BOMB;
            targetDir = new Vector2(playerDir.x, 0);
            updateStateTimer = Time.time + 4f;
            return;
        }

        if (playerDir.y > 0)
        {
            state = State.ASCEND;
            updateStateTimer = Time.time + 3f;
            return;
        }
        

        updateStateTimer = Time.time + 2f;
        state = State.ENGAGE;
    }

    private void commonRoutine()
    {
        var angleDiff = Vector3.Angle(transform.right, playerDir);
        if (angleDiff < 5.0f && playerDist < 40f)
        {
            shoot = true;
        }
        else
        {
            shoot = false;
        }

        var bombDir = plane.transformDown().normalized + transform.right.normalized;
        var angleDiffBomb = Vector3.Angle(bombDir, playerDir);
        if (state == State.BOMB && angleDiffBomb < 20.0f)
        {
            bomb = true;
        }
        else
        {
            bomb = false;
        }

        if (targetDir.y > 0)
        {
            targetThrottle = maxThrottle;
        }
        else
        {
            targetThrottle = 10f;
        }

        if (targetThrottle > maxThrottle)
        {
            targetThrottle = maxThrottle;
        }
        if (targetThrottle < minThrottle)
        {
            targetThrottle = minThrottle;
        }
    }

    private void startRoutine()
    {
        targetDir = Vector2.left;
        targetThrottle = 10.0f;
    }

    private void engageRoutine()
    {
        targetDir = playerDir;
    }

    private void ascendRoutine()
    {
        targetDir = new Vector2(targetDir.x, 0).normalized + Vector2.up;
    }

    private void bombRoutine()
    {
        targetDir = new Vector2(targetDir.x, 0);
    }

    private void runRoutine()
    {
        targetDir = -playerDir.normalized - Vector3.up;
    }

    private void avoidanceRoutine()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10, GROUND);
        if (hit.collider != null)
        {
            float x = transform.right.x < 0 ? -1 : 1;
            targetDir = new Vector3(x, 1.0f);
        }

        RaycastHit2D hitForward = Physics2D.Raycast(transform.position, targetDir, 20, GROUND);
        if (hitForward.collider != null)
        {
            targetDir = targetDir + Vector2.up;
        }
    }
}
