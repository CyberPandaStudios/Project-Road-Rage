using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    private List<GameObject> monsters = new List<GameObject>();
    public int maxSpawnCount = 5;
    public float radius = 20f;
    public GameObject monster;
    public float spawnDelay = 25f;

    private WaitForSecondsRealtime waitTime;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = new WaitForSecondsRealtime(spawnDelay);
        StartCoroutine(SpawnMonster());
    }

     IEnumerator SpawnMonster(){
        while (monsters.Count <= maxSpawnCount){

            GameObject newMonster = Instantiate(monster, transform.position, transform.rotation);
            monsters.Add(newMonster);

            yield return waitTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
