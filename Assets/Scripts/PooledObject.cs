using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool objectPool; // Reference to the object pool
    private GameObject prefab; // Reference to the prefab

    public void Initialize(ObjectPool pool, GameObject prefab)
    {
        this.objectPool = pool;
        this.prefab = prefab;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ReturnObjects"))
        {
            ReturnObject();
        }
    }

    public void ReturnObject()
    {
        objectPool.ReturnObject(prefab, gameObject); // Return this object to the pool
    }
}
