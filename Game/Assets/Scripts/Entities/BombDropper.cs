// Date   : 01.06.2019 09:35
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class BombDropper : MonoBehaviour
{

    [SerializeField]
    private BombConfig config;
    
    [SerializeField]
    private Triplane triplane;

    [SerializeField]
    private GameObject bombOrigin;

    private float dropTimer = 0f;
    private bool canDrop = true;

    public Bomb Drop(Vector2 origin, Vector2 direction)
    {
        if (canDrop) {
            Bomb newBomb = config.Prefab.GetPooledInstance<Bomb>();
            newBomb.transform.position = origin;
            var startVelocity = (Vector2)triplane.transform.up * 10.0f * (triplane.isUpsideDown() ? 1.0f : -1.0f);
            newBomb.Drop(config.LifeTime, direction + startVelocity, config.Speed);
            canDrop = false;
            return newBomb;
        }
        return null;
    }

    void Update()
    {
        if (!canDrop) {
            dropTimer += Time.deltaTime;
            if (dropTimer > config.DropInterval) {
                dropTimer = 0f;
                canDrop = true;
            }
        }
    }
}
