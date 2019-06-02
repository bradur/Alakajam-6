using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaneControls : MonoBehaviour, Killable
{

    ControllableFlying flying;
    bool rotateClockwise = false;
    bool rotateCounterClockwise = false;
    bool accelerate = false;
    bool decelerate = false;

    [SerializeField]
    private RuntimeBool playerControlsEnabled;

    [SerializeField]
    private RuntimeBool playerDied;

    // Start is called before the first frame update
    void Start()
    {
        flying = GetComponent<ControllableFlying>();
        GetComponentInChildren<Triplane>().ParentPlane = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlsEnabled.Toggle) {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rotateClockwise = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotateCounterClockwise = true;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                accelerate = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                decelerate = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                flying.Roll();
            }
        }
    }

    private void FixedUpdate()
    {
        if (playerControlsEnabled.Toggle) {
            if (rotateClockwise)
            {
                flying.RotateCW();
                rotateClockwise = false;
            }
            else if (rotateCounterClockwise)
            {
                flying.RotateCCW();
                rotateCounterClockwise = false;
            }

            if (accelerate)
            {
                flying.Accelerate();
                accelerate = false;
            }
            else if (decelerate)
            {
                flying.Decelerate();
                decelerate = false;
            }
        }
    }

    public void Kill()
    {
        playerDied.Toggle = true;
        flying.Kill();
        //Destroy(gameObject);
    }
}
