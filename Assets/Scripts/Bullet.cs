using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.up * speed;
        Destroy(gameObject, lifeTime);
        transform.localScale = Vector3.one * 0.8f;
    }

    void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime * .6f;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            GameManager.instance.AddScore(100);
            Asteroid ast = collision.gameObject.GetComponent<Asteroid>();

            ast.Break(); 

            Destroy(gameObject);
        }
    }
}