using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject enemyStatue;
    public EnemyTrigger enemyTrigger;
    public GameObject spawnEnemy;
    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemy.SetActive(false);
        enemyStatue.SetActive(true);
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTrigger.triggered == true && triggered == false)
        {
            spawnEnemy.SetActive(true);
            enemyStatue.SetActive(false);
            triggered = true;
        }
    }
}
