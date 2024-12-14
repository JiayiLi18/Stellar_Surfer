using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

    /// <summary>
    /// Preloads a pool for a specific prefab.
    /// </summary>
    public void PreloadPool(GameObject prefab, int initialSize)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            poolDictionary[prefab] = new Queue<GameObject>();

            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = CreateNewObject(prefab);
                obj.SetActive(false);
                poolDictionary[prefab].Enqueue(obj);
            }
        }
    }

    /// <summary>
    /// Gets an object from the pool for a specific prefab.
    /// </summary>
    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            poolDictionary[prefab] = new Queue<GameObject>();
        }

        GameObject obj;

        if (poolDictionary[prefab].Count > 0)
        {
            obj = poolDictionary[prefab].Dequeue();
            obj.SetActive(true);
        }
        else
        {
            obj = CreateNewObject(prefab);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        if (parent != null)
        {
            obj.transform.SetParent(parent);
        }

        return obj;
    }

    /// <summary>
    /// Returns an object to the pool for a specific prefab.
    /// </summary>
    public void ReturnObject(GameObject prefab, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[prefab].Enqueue(obj);
    }

    private GameObject CreateNewObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(transform); // Set under this pool manager

        PooledObject pooledObject = obj.AddComponent<PooledObject>();
        pooledObject.Initialize(this, prefab);
        return obj;
    }
}
