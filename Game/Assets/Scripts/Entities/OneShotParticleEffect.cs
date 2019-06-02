using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotParticleEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particleSystem;

    float checkTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkTime < Time.time)
        {
            if (!particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
            checkTime = Time.time + 0.5f;
        }
    }
}
