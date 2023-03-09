using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10.0f;
    public float fireRate = 0.5f;

    private float nextFire = 0.0f;
    private List<GameObject> bulletPool = new List<GameObject>();
    private Vector3 bulletPoolPosition = new Vector3(-100.0f, -100.0f, 0.0f);

    private void Start()
    {
        // Crea un pool de disparos y desactívalos
        for (int i = 0; i < 20; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPoolPosition, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    private void Update()
    {
        // Mueve la nave del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontalInput, 0, 0) * speed * Time.deltaTime);

        // Dispara si el jugador pulsa el botón y ha pasado el tiempo de enfriamiento
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
    }

    private void Fire()
    {
        // Busca un disparo disponible en el pool y lo activa
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.transform.position = transform.position;
                bullet.GetComponent<Rigidbody>().velocity = Vector3.up * bulletSpeed;
                return;
            }
        }
    }
}