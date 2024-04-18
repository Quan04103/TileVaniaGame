using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    Rigidbody2D bulletRigidbody;
    PlayerMovement playerMovement;
    float correctlyShooting;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        correctlyShooting = playerMovement.transform.localScale.x * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidbody.velocity = new Vector2(correctlyShooting, bulletRigidbody.velocity.y);
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy"){
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
}
