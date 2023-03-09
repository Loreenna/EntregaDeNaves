using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 20;

    public List<GameObject> enemyPool = new List<GameObject>();

    public Transform[] Puntos;

    public int PuntoIndice = 0;

    public static EnemyPool Instance;

    private void Start()
    {
        // Crea un pool de enemigos y desactívalos
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }

        PuntoIndice = Random.Range(0, Puntos.Length);
    }

    public GameObject GetEnemy(int bulletType, Vector3 position, Quaternion rotation)
    {
        // Busca un enemigo disponible en el pool y lo activa
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        // Si no hay enemigos disponibles, devuelve null
        return null;
    }

    public void Randomize()
    {
        PuntoIndice = Random.Range(0, Puntos.Length);
    }
}