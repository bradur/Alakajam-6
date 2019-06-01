using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triplane : MonoBehaviour
{
    [SerializeField]
    bool TriggerRoll = false;

    bool rolled = false;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TriggerRoll)
        {
            Roll();
            TriggerRoll = false;
        }
    }

    public void Roll()
    {
        if (rolled)
        {
            anim.SetBool("rolled", false);
        }
        else
        {
            anim.SetBool("rolled", true);
        }
        rolled = !rolled;
    }

    public bool isUpsideDown()
    {
        return rolled;
    }
}
