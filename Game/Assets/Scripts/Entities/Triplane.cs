using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triplane : MonoBehaviour
{
    [SerializeField]
    bool Trigger = false;
    
    [SerializeField]
    float Health = 100.0f;

    [SerializeField]
    ParticleSystem flash, smoke, lightSmoke, darkSmoke, fire;

    [SerializeField]
    GameObject[] disableObjects;

    [SerializeField]
    GameObject Explosion;
    
    public Killable ParentPlane;

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
        if (Trigger)
        {
            TriggerMuzzleFlash();
            Trigger = false;
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

    public void TriggerMuzzleFlash()
    {
        flash.Play();
        smoke.Play();
    }

    public void Hurt(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Kill();
        }

        if (Health <= 66)
        {
            lightSmoke.Play();
        }
        if (Health <= 33)
        {
            darkSmoke.Play();
        }
    }

    public void Kill()
    {
        AudioPlayer.main.PlaySound(GameEvent.BombExplodes);
        GameObject xpl = Instantiate(Explosion);
        xpl.SetActive(true);
        xpl.transform.position = transform.position;
        ParentPlane.Kill();
        fire.Play();

        foreach (GameObject obj in disableObjects)
        {
            obj.SetActive(false);
        }

        //Destroy(gameObject);
    }
}
