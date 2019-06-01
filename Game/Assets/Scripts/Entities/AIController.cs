using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private AIStates state = AIStates.Start;
    private ControllableFlying flying;
    [SerializeField]
    private RuntimeVector3 playerPosition;
    private Rigidbody2D body;
    private AiMovementFinished rotationFinished;
    private bool rotationOngoing = false;

    // Start is called before the first frame update
    void Start()
    {
        flying = GetComponent<ControllableFlying>();
        body = GetComponent<Rigidbody2D>();
        rotationFinished = callBackFinished;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerPosition.Value, transform.position) <= 50f)
            Debug.DrawLine(transform.position, playerPosition.Value, Color.green);
        else
            Debug.DrawLine(transform.position, playerPosition.Value, Color.red);
        //Debug.Log(Vector3.Distance(playerPosition.Value, transform.position));
        //Debug.Log(body.velocity);

    }

    private void FixedUpdate()
    {
        if (rotationOngoing)
        {
            return;
        }

        if (state == AIStates.Start)
        {
            flying.Accelerate();
            if (Vector3.Distance(playerPosition.Value, transform.position) > 50f)
            {
                state = AIStates.PlayerNotInRange;
            }
            else if (playerPosition.Value.y + 1f <= transform.position.y) // player is at least 1 unit lower than this enemy
            {
                state = AIStates.PlayerBelow;
            }
            else
            {
                state = AIStates.PlayerInRange;
            }
        }
        else if (state == AIStates.PlayerNotInRange)
        {
            if (Vector3.Distance(playerPosition.Value, transform.position) <= 50f)
            {
                state = AIStates.PlayerInRange;
            }

            if (transform.position.x > playerPosition.Value.x)
            {
                if (body.velocity.x > 0)
                {
                    //flip
                    //flying.RotateCW(Vector3.Angle(transform.position, playerPosition.Value), callBackFinished, true);
                    flying.RotateCW(playerPosition.Value - transform.position, callBackFinished, true);
                    rotationOngoing = true;
                    Debug.Log("Rotate towards player");
                }
                else
                {
                    flying.Accelerate();
                }
            }
            else if (transform.position.x < playerPosition.Value.x)
            {
                if (body.velocity.x < 0)
                {
                    flying.RotateCW(playerPosition.Value - transform.position, callBackFinished, true);
                    rotationOngoing = true;
                    Debug.Log("Panic!");
                }
                else
                {
                    flying.Accelerate();
                }
            }
        }
        else if(state == AIStates.PlayerInRange)
        {
            if (Vector3.Distance(playerPosition.Value, transform.position) > 50f)
            {
                state = AIStates.PlayerNotInRange;
            }

        }

    }

    private bool callBackFinished()
    {
        Debug.Log("Rotated towards player!");
        rotationOngoing = false;
        return true;
    }

    enum AIStates
    {
        Start,
        PlayerNotInRange,
        PlayerBelow, //in range but below this enemy
        PlayerInRange,
        GetAway
    }
}
