using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private int randomObstacle;

    //Variables para determinar el casteo y enfriamiento del Invoke.
    private float startDelay = 2;
    private float repeatRate = 2;

    //Variable de referencia de scripts externos
    private PlayerController playerCScript;

    // Start is called before the first frame update
    void Start()
    {
        playerCScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if (playerCScript.gameOver == false)
        {
            randomObstacle = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[randomObstacle], spawnPos, obstaclePrefab[randomObstacle].transform.rotation);
        }
    }
}
