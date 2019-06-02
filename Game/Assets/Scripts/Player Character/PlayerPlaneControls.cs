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
    private RuntimeBool isNormalControls;

    [SerializeField]
    private RuntimeBool isInvertedControls;

    [SerializeField]
    private RuntimeBool playerControlsEnabled;

    [SerializeField]
    private RuntimeBool playerDied;

    KeyCode throttle;
    KeyCode dethrottle;
    KeyCode turnCW;
    KeyCode turnCCW;

    // Start is called before the first frame update
    void Start()
    {
        flying = GetComponent<ControllableFlying>();
        GetComponentInChildren<Triplane>().ParentPlane = this;

        if(isNormalControls.Toggle)
        {
            throttle = KeyCode.UpArrow;
            dethrottle = KeyCode.DownArrow;
            if (isInvertedControls.Toggle)
            {
                turnCW = KeyCode.RightArrow;
                turnCCW = KeyCode.LeftArrow;
            }
            else
            {
                turnCW = KeyCode.LeftArrow;
                turnCCW = KeyCode.RightArrow;
            }
        }
        else
        {
            throttle = KeyCode.RightArrow;
            dethrottle = KeyCode.LeftArrow;
            if (isInvertedControls.Toggle)
            {
                turnCW = KeyCode.UpArrow;
                turnCCW = KeyCode.DownArrow;
            }
            else
            {
                turnCW = KeyCode.DownArrow;
                turnCCW = KeyCode.UpArrow;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (playerControlsEnabled.Toggle) {
            if (Input.GetKey(turnCW))
            {
                rotateClockwise = true;
            }
            else if (Input.GetKey(turnCCW))
            {
                rotateCounterClockwise = true;
            }

            if (Input.GetKey(throttle))
            {
                accelerate = true;
            }
            else if (Input.GetKey(dethrottle))
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
