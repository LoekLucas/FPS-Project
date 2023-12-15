using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    private enemy enemy;
    private Transform player;
    public float attackRange = 10f;
    bool foundPlayer = false;


    public Material defaultMaterial;
    public Material attackMaterial;
    private Renderer rend;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<enemy>();
        rend = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            rend.sharedMaterial = attackMaterial;
            enemy.agent.SetDestination(player.position);
            foundPlayer = true;
        }

        else if (foundPlayer == true)
        {
            rend.sharedMaterial = defaultMaterial;
            enemy.newLocation();
            foundPlayer = false;
        }
    }
}
