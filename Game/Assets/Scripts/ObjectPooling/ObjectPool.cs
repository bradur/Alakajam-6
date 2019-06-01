// Date   : 01.06.2019 07:54
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{

    static string poolName = " Pool";

    List<PooledObject> availableObjects = new List<PooledObject>();

    private PooledObject prefab;

    public PooledObject GetObject()
    {
        PooledObject newObject;

        int lastAvailableIndex = availableObjects.Count - 1;
        if (lastAvailableIndex >= 0)
        {
            newObject = availableObjects[lastAvailableIndex];
            availableObjects.RemoveAt(lastAvailableIndex);
            newObject.gameObject.SetActive(true);
        }
        else
        {
            newObject = Instantiate<PooledObject>(prefab);
            newObject.transform.SetParent(transform, false);
            newObject.Pool = this;
        }
        return newObject;
    }

    public void AddObject(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        availableObjects.Add(pooledObject);
        //Object.Destroy(pooledObject.gameObject);
    }

    public static ObjectPool GetPool(PooledObject prefab)
    {
        GameObject newObject;
        ObjectPool pool;
        if (Application.isEditor)
        {
            newObject = GameObject.Find(prefab.name + poolName);
            if (newObject)
            {
                pool = newObject.GetComponent<ObjectPool>();
                if (pool)
                {
                    return pool;
                }
            }
        }

        newObject = new GameObject(prefab.name + poolName);
        pool = newObject.AddComponent<ObjectPool>();
        pool.prefab = prefab;
        return pool;
    }
}
