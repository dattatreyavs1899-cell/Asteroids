using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject asteroidPrefab;

    public float spawnInterval = 3f;
    public float spawnRadius = 8f;

    public int score = 1000; 
    public TextMeshProUGUI scoreText;

    private int hitCount = 0; 

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 0f, spawnInterval);
        UpdateScore();
    }

    public void PlayerHit()
    {
        hitCount++;

        int penalty = 100 * (int)Mathf.Pow(2, hitCount - 1);

        score -= penalty;

        Debug.Log("Hit! -" + penalty + " | Score: " + score);

        UpdateScore();

        if (score <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");

        Time.timeScale = 0f; 
    }

    void SpawnAsteroid()
    {
        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        Vector2 spawnPos = Vector2.zero;

        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: 
                spawnPos = new Vector2(Random.Range(-width, width), height);
                break;

            case 1: 
                spawnPos = new Vector2(Random.Range(-width, width), -height);
                break;

            case 2: 
                spawnPos = new Vector2(-width, Random.Range(-height, height));
                break;

            case 3: 
                spawnPos = new Vector2(width, Random.Range(-height, height));
                break;
        }

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        asteroid.GetComponent<Asteroid>().SetDirectionTowardsCenter();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScore();
    }

    void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}