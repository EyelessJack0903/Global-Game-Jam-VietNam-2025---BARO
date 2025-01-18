using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public static GameController Instance; 

    public GameObject greenBallPrefab;
    public GameObject redBallPrefab;
    public GameObject deathZonePrefab;

    public float spawnTime = 1.5f; 
    private float m_spawnTime;

    public int lives = 3; 
    private bool m_isGameover;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Instantiate(deathZonePrefab, new Vector2(0, 5.02f), Quaternion.identity);
        m_spawnTime = 0;
    }

    private void Update()
    {
        if (m_isGameover) return;

        m_spawnTime -= Time.deltaTime;

        if (m_spawnTime <= 0)
        {
            SpawnBall();

            m_spawnTime = spawnTime;
        }
    }

    public void SpawnBall()
    {
        GameObject ballPrefab = Random.value > 0.5f ? greenBallPrefab : redBallPrefab;

        Vector2 spawnPos = new Vector2(Random.Range(-8, 8), -5.6f);

        if (ballPrefab)
        {
            GameObject spawnedBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);

            float randomSpeed = Random.Range(0.75f, 2f);

            if (spawnedBall.CompareTag("RedBall"))
            {
                RedBall redBallScript = spawnedBall.GetComponent<RedBall>();
                if (redBallScript != null)
                {
                    redBallScript.SetSpeed(randomSpeed);
                }
            }
            else if (spawnedBall.CompareTag("BlueBall"))
            {
                BlueBall blueBallScript = spawnedBall.GetComponent<BlueBall>();
                if (blueBallScript != null)
                {
                    blueBallScript.SetSpeed(randomSpeed);
                }
            }
        }
    }



    public void LoseLife()
    {
        lives--;

        Debug.Log("Lives remaining: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        m_isGameover = true;
        Debug.Log("Game Over!");
    }

    public bool IsGameover()
    {
        return m_isGameover;
    }

    public void SetGameoverState(bool state)
    {
        m_isGameover = state;
    }
}
