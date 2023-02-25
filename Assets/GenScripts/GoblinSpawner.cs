using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSpawner : MonoBehaviour
{
    public GameObject goblinSpawnPoint;
    public int numGoblinsPerWave = 4;
    
    [SerializeField]
    private GameObject goblinPrefab;

    [SerializeField]
    private float waveInterval = 3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(waveInterval, goblinPrefab)); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {   
        yield return new WaitForSeconds(interval);
        for (int i = 0; i < numGoblinsPerWave; i++)
        {
            if(i < numGoblinsPerWave)
            {
                GameObject newEnemy = Instantiate(enemy, goblinSpawnPoint.transform.position, Quaternion.identity);   
            }
        }
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
