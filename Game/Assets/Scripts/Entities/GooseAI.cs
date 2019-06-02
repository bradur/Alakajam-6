using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseAI : MonoBehaviour
{
    [SerializeField]
    private RuntimeVector3 playerPosition;

    private ControllableFlying plane;
    private ProjectileShooter shooter;

    private Vector2 targetDir;

    // Start is called before the first frame update
    void Start()
    {
        plane = GetComponent<ControllableFlying>();
        shooter = GetComponentInChildren<ProjectileShooter>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector3 playerPos = playerPosition.Value;
        Vector3 curPos = transform.position;
        targetDir = playerPos - curPos;

        var angleDiff = Vector3.SignedAngle(transform.right, targetDir, Vector3.forward);
        if (angleDiff < 5.0f && angleDiff > -5.0f)
        {
            Projectile projectile = shooter.Shoot(transform.right);
            if (projectile != null) {
                AudioPlayer.main.PlaySound(GameEvent.EnemyGunFires);
            }
        }
        else
        {
            if (angleDiff < 0)
            {
                plane.RotateCW();
            }
            else
            {
                plane.RotateCCW();
            }
        }

        if (transform.right.x < 0 && !plane.isUpsideDown())
        {
            plane.Roll();
        }

        if (transform.right.x > 0 && plane.isUpsideDown())
        {
            plane.Roll();
        }

        plane.Accelerate();
    }
}
