using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float padding = 0.5f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float firingRate = 0.2f;
    public int health = 3;
    public AudioClip fireSound;
    public AudioClip damageSound;
    public AudioClip deathSound;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    private float lastShotTime;
    private Rigidbody2D rb2d;
    private AudioSource audioSource;

    private void Start()
    {
        // Calcula los límites de movimiento del jugador en la pantalla
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 topmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 bottommost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        xMin = leftmost.x + padding;
        xMax = rightmost.x - padding;
        yMin = bottommost.y + padding;
        yMax = topmost.y - padding;

        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Mueve al jugador horizontalmente
        float horizontalInput = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(horizontalInput * speed, 0);

        // Limita el movimiento del jugador a los bordes de la pantalla
        float clampedX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float clampedY = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        // Dispara un proyectil si se presiona el botón de disparo y ha pasado suficiente tiempo desde el último disparo
        if (Input.GetButtonDown("Fire1") && Time.time - lastShotTime > firingRate)
        {
            lastShotTime = Time.time;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            audioSource.PlayOneShot(fireSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Resta salud al jugador si colisiona con un enemigo
        if (collision.CompareTag("Enemy"))
        {
            health--;
            audioSource.PlayOneShot(damageSound);
            if (health == 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        // Desactiva al jugador, incrementa el contador de muertes y reproduce el sonido de muerte
        gameObject.SetActive(false);
        //ScoreManager.Instance.IncrementDeaths();
        audioSource.PlayOneShot(deathSound);
    }
}