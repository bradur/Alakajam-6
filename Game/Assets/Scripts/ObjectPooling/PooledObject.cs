// Date   : 01.06.2019 07:53
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class PooledObject : MonoBehaviour
{

    [System.NonSerialized]
    ObjectPool poolInstanceForPrefab;
    public ObjectPool Pool { get; set; }

    public void ReturnToPool()
    {
        if (Pool)
        {
            Pool.AddObject(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public T GetPooledInstance<T>() where T : PooledObject
    {
        if (!poolInstanceForPrefab)
        {
            poolInstanceForPrefab = ObjectPool.GetPool(this);
        }
        return (T)poolInstanceForPrefab.GetObject();
    }
}
