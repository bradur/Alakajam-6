using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AACannon : MonoBehaviour
{
    [SerializeField]
    GameObject barrel;

    [SerializeField]
    ParticleSystem muzzle;

    [SerializeField]
    private RuntimeVector3 playerPosition;

    ProjectileShooter shooter;

    bool dead = false;


    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponentInChildren<ProjectileShooter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Vector3 playerPos = playerPosition.Value;
            Vector3 curPos = barrel.transform.position;
            var playerDir = playerPos - curPos;
            var playerDist = playerDir.magnitude;
        
            var angleDiff = Vector3.SignedAngle(-barrel.transform.right, playerDir, Vector3.forward);

            Debug.Log(angleDiff + ", " + playerDist);

            if (angleDiff > -1.0f && angleDiff < 1.0f)
            {
                if (playerDist < 60)
                {
                    shooter.Shoot(barrel.transform.right * -1);
                }
            }

            float rotateDir = angleDiff < 0 ? -1 : 1;
            float rotateAmount = Mathf.Min(Mathf.Abs(angleDiff), Time.deltaTime * 90);
        
            barrel.transform.Rotate(Vector3.forward, rotateAmount * rotateDir);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Bomb")
        {
            Destroy(barrel);
            dead = true;
        }
    }
}
