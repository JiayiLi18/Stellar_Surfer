using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public float skyboxSpeed;
    /*[SerializeField] GameObject rings;
    [SerializeField] ObjectPool objectPool;
    [SerializeField] Transform parentObject, spawnPosition;
    [SerializeField] float spawnInterval, returnInterval;
    private GameObject obj1, obj2;

    void Start()
    {
        objectPool.PreloadPool(rings, 2);
        StartCoroutine(SpawnObjects());
    }*/
    void Update()
    {
        //Skybox rotate slowly
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxSpeed);
    }

    /*private IEnumerator SpawnObjects()
    {
        while (true)
        {
            obj1 = objectPool.GetObject(rings, spawnPosition.position, Quaternion.identity, parentObject);
            StartCoroutine(ReturnObjects(obj1));
            yield return new WaitForSeconds(spawnInterval);
            obj2 = objectPool.GetObject(rings, spawnPosition.position, Quaternion.identity, parentObject);
            StartCoroutine(ReturnObjects(obj2));
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator ReturnObjects(GameObject obj)
    {
        yield return new WaitForSeconds(returnInterval);
        if (obj != null)
        {
            PooledObject pooledObject = obj.GetComponent<PooledObject>();
            if (pooledObject != null)
                pooledObject.ReturnObject(); // Return this object to the pool
        }
    }*/
}
