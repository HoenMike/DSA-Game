/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoint; // Spawns's location
    public SpawnData[] spawnData; // Enemy's data
    private List<SpawnData> spawnList; // List of Enemies to spawn
    private int lastLevel = -1; // Last level of Enemy spawned
    public float levelTime; // Spawn Time Scale

    int level;
    float timer;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        spawnList = new List<SpawnData>();
        levelTime = GameManager.instance.maxGameTime / spawnData.Length; //spawn time Scale with maxGameTime
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isAlive) // if player is dead do nothing
            return;

        // Rotate the spawner for more dynamic spawn mechanic
        int rotationSpeed = 100;

        // level is increase when time go on
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1);

        // when level reach the next level, add Stronger Enemy to spawnList
        if (level != lastLevel)
        {
            spawnList.Add(spawnData[level]);
            lastLevel = level;
        }

        // Spawn Enemy
        if (timer > spawnData[level].spawnTime)
        {
            timer = 0f;
            Spawn();
        }

        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
    }

    void Spawn()
    {
        // For each Enemy in spawnList
        for (int i = 0; i < spawnList.Count; i++)
        {
            GameObject enemy = GameManager.instance.pool.Get(0); // 0 mean Enemy in Pool
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // Random spawn location
            enemy.GetComponent<Enemy>().Init(spawnList[i]); // Init Enemy with spawnData
        }
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
