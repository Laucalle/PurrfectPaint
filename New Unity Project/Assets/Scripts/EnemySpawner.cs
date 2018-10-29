using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour {
    public GameObject[] enemies;
    public GameObject[] corpses;
    public GameObject canvas;

    public TextMeshProUGUI text;

    public Transform[] targets;
    public Vector2 spawnValues;
    
    public float maxSpawnWait;
    public float minSpawnWait;
    public float startWait;
    public bool stop;

    private float spawnWait;
    private int score = 0;

    int randEnemy;
	// Use this for initialization
	void Start () {
        StartCoroutine(WaitSpawner());
	}
	
	// Update is called once per frame
	void Update () {
        spawnWait = Random.Range(minSpawnWait, maxSpawnWait);
	}

    public void IncreaseScore()
    {
        score++;
    }

    public void GameOver()
    {
        text.text= score.ToString();
        canvas.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator WaitSpawner() {
        yield return new WaitForSeconds(startWait);

        while (!stop) {
            randEnemy = Random.Range(0, enemies.Length);
            Vector3 SpawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x),
                Random.Range(-spawnValues.y, spawnValues.y), 0);
            GameObject enemy = Instantiate(enemies[randEnemy], SpawnPosition + transform.position, gameObject.transform.rotation, this.transform) as GameObject;
            enemy.GetComponent<EnemyBehaviour>().target = targets[Random.Range(0, targets.Length)];
            enemy.GetComponent<EnemyBehaviour>().corpse = corpses[randEnemy];
            yield return new WaitForSeconds(spawnWait);
        }
    }

}
