using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 2f;

    public bool isBig = true; 

    public GameObject asteroidPrefab;

    bool hasBroken = false;

    public GameObject explosionPrefab;

    public void SetDirectionTowardsCenter()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 direction = (Vector2.zero - (Vector2)transform.position).normalized;

        float offsetX = Random.Range(-0.5f, 0.5f);
        float offsetY = Random.Range(-0.5f, 0.5f);

        direction += new Vector2(offsetX, offsetY);

        rb.linearVelocity = direction.normalized * speed;
        rb.angularVelocity = Random.Range(-50f, 50f);
    }

    public void Break()
    {
        if (hasBroken) return; 
        hasBroken = true;

        
        GameManager.instance.AddScore(100);
        SoundManager.instance.PlaySound(SoundManager.instance.breakSound, 0.7f);
        ScreenShake.instance.Shake(0.1f, 0.1f);

        
        if (isBig)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject newAsteroid = Instantiate(asteroidPrefab, transform.position, Quaternion.identity);

                Asteroid ast = newAsteroid.GetComponent<Asteroid>();

                ast.isBig = false; 
                ast.speed = speed + 1f;

                newAsteroid.transform.localScale = transform.localScale * 0.6f;

                ast.SetDirectionTowardsCenter();
            }
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}