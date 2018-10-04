using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject[] enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public List<Transform> enemies = new List<Transform>();

    void Start ()
    {
        InvokeRepeating ("Spawn", 5f, spawnTime);
    }

	void Update()
	{
        if(spawnTime >= 1f){
            if (ScoreManager.score > 75)
            {
                spawnTime -= 1;
            }  
            else if (ScoreManager.score > 150)
            {
                spawnTime -= 1;
            }  
            if (ScoreManager.score > 225)
            {
                spawnTime -= 2;
            }  
            if (ScoreManager.score > 300)
            {
                spawnTime = 1;
            }  
        }

	}

	void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemy.Length);

        GameObject enemyTransform = Instantiate(enemy[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
        enemies.Add(enemyTransform.transform);
    }

    public int getNumberOfEnemies(){
        return enemies.Count;
    }
}
