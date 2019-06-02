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

    bool alive = true;

    int GROUND;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GROUND = LayerMask.NameToLayer("Ground");
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
        if (alive)
        {
            AudioPlayer.main.PlaySound(GameEvent.BombExplodes);
            GameObject xpl = Instantiate(Explosion);
            xpl.SetActive(true);
            xpl.transform.position = transform.position;
            ParentPlane.Kill();
            lightSmoke.Play();
            darkSmoke.Play();
            fire.Play();

            foreach (GameObject obj in disableObjects)
            {
                obj.SetActive(false);
            }

            alive = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == GROUND)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                ContactPoint2D point = collision.GetContact(i);
                float contactAngle = Vector2.Angle(point.normal, transform.right);
                Debug.Log(contactAngle);
                if (contactAngle < 75 || contactAngle > 105)
                {
                    Kill();
                }
            }
        }
        else if (collision.gameObject.tag == "Plane")
        {
            Kill();
        }
    }
}
