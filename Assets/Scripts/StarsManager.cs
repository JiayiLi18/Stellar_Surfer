using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsManager : MonoBehaviour
{
    [SerializeField] GameObject[] Stars;
    [SerializeField] ObjectPool objectPool;
    [SerializeField] Transform parentObject, spawnPosition;
    [SerializeField] float positionRangeX, positionRangeY, minSpawnInterval, maxSpawnInterval, constantInterval;

    void Start()
    {
        if (Stars != null)
        {
            for (int i = 0; i < Stars.Length; i++)
            {
                objectPool.PreloadPool(Stars[i], 8);
            }
        }
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (Stars != null)
            {
                int i = Random.Range(0, Stars.Length);
                Vector3 position = new Vector3
                (spawnPosition.position.x + Random.Range(-positionRangeX, positionRangeX),
                spawnPosition.position.y + Random.Range(-positionRangeY, positionRangeY),
                spawnPosition.position.z
                );
                objectPool.GetObject(Stars[i], position, Quaternion.identity, parentObject);
                // 可能可以连成一条星链
                int isConstant = Random.Range(0, 2);
                if(isConstant == 0){
                   yield return new WaitForSeconds(constantInterval);
                   objectPool.GetObject(Stars[i], position, Quaternion.identity, parentObject);
                   yield return new WaitForSeconds(constantInterval);
                }
                else{
                float spawnInterval = Random.Range(minSpawnInterval,maxSpawnInterval);
                yield return new WaitForSeconds(spawnInterval);}
            }
        }
    }
}
