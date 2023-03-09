using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool Afuera = false;
    private int TipoDeEnemigo;


    void Update()
    {
        transform.position += transform.forward * 40 * Time.deltaTime;

        if (Afuera == true)
        {
            this.gameObject.SetActive(false);
            Afuera = false;
            Respawn();
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        Afuera = true;
    }
    
    void OnBecameInvisible()
    {
        Afuera = true;
    }

    void Respawn()
    {
        TipoDeEnemigo = 0;
        GameObject Enemy = EnemyPool.Instance.GetEnemy(TipoDeEnemigo, EnemyPool.Instance.Puntos[EnemyPool.Instance.PuntoIndice].position, EnemyPool.Instance.Puntos[EnemyPool.Instance.PuntoIndice].rotation);
        Enemy.SetActive(true);
        EnemyPool.Instance.Randomize();
    }
}
