// Date   : 01.06.2019 08:06
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class Projectile : PooledObject
{
    [SerializeField]
    private float Damage = 5.0f;

    [SerializeField]
    private Rigidbody2D rb2D;

    [SerializeField]
    private ParticleSystem trail;

    [SerializeField]
    private GameObject hitEffect;


    private float lifeTimer = 0f;
    private float lifeTime = -1f;
    private bool alive = false;

    public void Shoot(float lifeTime, Vector2 direction, float speed)
    {
        alive = true;
        this.lifeTime = lifeTime;
        lifeTimer = 0f;
        rb2D.velocity = direction * speed;
        trail.Play();
    }

    void Update()
    {
        if (alive)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer > lifeTime)
            {
                Kill();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject effect = Instantiate(hitEffect);
        effect.transform.position = transform.position;

        GameObject other = collider.gameObject;
        if (other.tag == "Bomb")
        {
            Bomb bomb = other.GetComponent<Bomb>();
            bomb.Explode();
        }
        else if (other.tag == "Plane")
        {
            Triplane plane = other.GetComponent<Triplane>();
            plane.Hurt(Damage);
            GooseAI ai = other.GetComponent<GooseAI>();
            if (ai != null)
            {
                ai.Hurt();
            }
        }

        Kill();
    }

    public void Kill()
    {
        rb2D.velocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
        alive = false;
        lifeTime = -1f;
        lifeTimer = 0f;
        ReturnToPool();
    }
}
