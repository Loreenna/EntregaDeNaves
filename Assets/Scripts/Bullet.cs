using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.forward * 40 * Time.deltaTime;
    }


    void OnTriggerEnter(Collider collider)
    {

        this.gameObject.SetActive(false);

    }
    
    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}