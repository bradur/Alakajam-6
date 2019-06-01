// Date   : 01.06.2019 09:34
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class Bomb : PooledObject
{
    [SerializeField]
    private Rigidbody2D rb2D;

    private float lifeTimer = 0f;
    private float lifeTime = -1f;
    private bool alive = false;

    public void Drop(float lifeTime, Vector2 direction, float speed)
    {
        alive = true;
        this.lifeTime = lifeTime;
        lifeTimer = 0f;
        rb2D.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision2D) {
        Explode();
    }

    void Explode() {
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
