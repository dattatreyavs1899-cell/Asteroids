using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float thrustForce = 5f;

    Rigidbody2D rb;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private bool isInvincible = false; 
    public float invincibleTime = 1.5f;

    SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        float rotate = -Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, rotate * rotationSpeed * Time.deltaTime);

        float maxSpeed = 8f;

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * thrustForce);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.up * thrustForce * 0.7f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            SoundManager.instance.PlaySound(SoundManager.instance.shootSound, 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") && !isInvincible)
        {
            GameManager.instance.PlayerHit();
            SoundManager.instance.PlaySound(SoundManager.instance.hitSound, 0.6f);
            StartCoroutine(HitFlash());

            
            transform.position = Vector3.zero;

            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            StartCoroutine(Invincibility());
        }
    }

    IEnumerator HitFlash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
    IEnumerator Invincibility()
    {
        isInvincible = true;


        yield return new WaitForSeconds(invincibleTime);

        isInvincible = false;
    }
}