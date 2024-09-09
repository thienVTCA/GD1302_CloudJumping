using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    List<Transform> listPosTransform;
    [SerializeField]
    float timeToSpawnEnemy = 2;
    float timeSpawn;
    // Start is called before the first frame update
    void Start()
    {
        timeSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSpawn < timeToSpawnEnemy)
        {
            timeSpawn += Time.deltaTime;
        }
        else
        {
            timeSpawn = 0;
            int posIndex = Random.Range(0, listPosTransform.Count);
            Instantiate(enemyPrefab, listPosTransform[posIndex].position, enemyPrefab.transform.rotation);
        }
    }
}
