using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritesManager : MonoBehaviour
{
    [SerializeField] GameObject[] Meteorites;
    [SerializeField] ObjectPool objectPool;
    [SerializeField] Transform parentObject, spawnPosition;
    [SerializeField] float positionRangeX, positionRangeY, minSpawnInterval, maxSpawnInterval;

    void Start()
    {
        if (Meteorites != null)
        {
            for (int i = 0; i < Meteorites.Length; i++)
            {
                objectPool.PreloadPool(Meteorites[i], 8);
            }
        }
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (Meteorites != null)
            {
                int i = Random.Range(0, Meteorites.Length);
                Vector3 position = new Vector3
                (spawnPosition.position.x + Random.Range(-positionRangeX, positionRangeX),
                spawnPosition.position.y + Random.Range(-positionRangeY, positionRangeY),
                spawnPosition.position.z
                );
                objectPool.GetObject(Meteorites[i], position, Quaternion.identity, parentObject);
                float spawnInterval = Random.Range(minSpawnInterval,maxSpawnInterval);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
