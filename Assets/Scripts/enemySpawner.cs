using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab; // Asks for gameobject
    public float Maxenemies = 25;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Maxenemies ; i++) // For loop, loops 25 times
        {
            GameObject Enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity); // ?
        }
    }
}
