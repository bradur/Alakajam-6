// Date   : 01.06.2019 09:34
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class Bomb : PooledObject
{
    [SerializeField]
    private Rigidbody2D rb2D;

    [SerializeField]
    private Collider2D collider;

    [SerializeField]
    private GameObject explosion;

    private float lifeTimer = 0f;
    private float lifeTime = -1f;
    private bool alive = false;
    [SerializeField]
    private AudioSource bombSoundSource;

    private float enableAt = 0;

    public void Drop(float lifeTime, Vector2 direction, float speed)
    {
        if (bombSoundSource == null)
        {
            bombSoundSource = GetComponent<AudioSource>();
        }
        bombSoundSource.Play();
        alive = true;
        this.lifeTime = lifeTime;
        lifeTimer = 0f;
        rb2D.velocity = direction * speed;

        collider.enabled = false;
        enableAt = Time.time + 0.3f;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Explode();
    }

    public void Explode()
    {
        AudioPlayer.main.PlaySound(GameEvent.BombExplodes);
        GameObject xpl = Instantiate(explosion);
        xpl.transform.position = transform.position;
        Kill();
    }

    void Update()
    {
        if (alive && lifeTime > -1f)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer > lifeTime)
            {
                Kill();
            }
        }
        
        if (!collider.enabled && enableAt < Time.time)
        {
            collider.enabled = true;
        }

        if (rb2D.velocity.magnitude > 0.01f)
        {
            float angleDiff = Vector3.SignedAngle(transform.right, rb2D.velocity, Vector3.forward);
            transform.Rotate(Vector3.forward, angleDiff);
        }
    }

    public void Kill()
    {
        bombSoundSource.Stop();
        rb2D.velocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
        alive = false;
        lifeTime = -1f;
        lifeTimer = 0f;
        ReturnToPool();
    }
}