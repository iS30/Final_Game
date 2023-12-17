using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour {

    public GameObject[] obstacles;
    public GameObject[] collectables;

    private Vector3 spawnPos;
    private PlayerControl playerControllerScript;

    private float startDelay = 2;
    private float repeatDelay = 1;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("SpawnObstacle", startDelay, repeatDelay);
        InvokeRepeating("SpawnCollectable", startDelay, repeatDelay);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    //to spawn obstacles
    public IEnumerator SpawnObstacle() {
        while (gameManager.isGameActive) {
            yield return new WaitForSeconds(gameManager.spawnRate);
            spawnPos = new Vector3(Random.Range(-80, -40), 0, Random.Range(-8, 8));
            int randomIndex = Random.Range(0, obstacles.Length);
            Instantiate(obstacles[randomIndex], spawnPos, obstacles[randomIndex].transform.rotation);
        }
    }

    //to spawn collectables
    public IEnumerator SpawnCollectable() {
        while (gameManager.isGameActive) {
            yield return new WaitForSeconds(gameManager.spawnRate);
            spawnPos = new Vector3(Random.Range(-80, -40), 1, Random.Range(-8, 8));
            int randomIndex = Random.Range(0, collectables.Length);
            Instantiate(collectables[randomIndex], spawnPos, collectables[randomIndex].transform.rotation);
        }
    }
}
