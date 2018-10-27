using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject[] enemies;
    public Transform[] targets;
    public Vector2 spawnValues;
    float spawnWait;
    public float maxSpawnWait;
    public float minSpawnWait;
    public int startWait;
    public bool stop;

    int randEnemy;
	// Use this for initialization
	void Start () {
        StartCoroutine(WaitSpawner());
	}
	
	// Update is called once per frame
	void Update () {
        spawnWait = Random.Range(minSpawnWait, maxSpawnWait);
	}
    IEnumerator WaitSpawner() {
        yield return new WaitForSeconds(startWait);

        while (!stop) {
            randEnemy = Random.Range(0, enemies.Length);
            Vector3 SpawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x),
                Random.Range(-spawnValues.y, spawnValues.y), 0);
            GameObject enemy = Instantiate(enemies[randEnemy], SpawnPosition + transform.position, gameObject.transform.rotation) as GameObject;
            enemy.GetComponent<EnemyBehaviour>().target = targets[Random.Range(0, targets.Length)];
            yield return new WaitForSeconds(spawnWait);
        }
    }

}
