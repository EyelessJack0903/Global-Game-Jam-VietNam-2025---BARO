using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject happyBallPrefab;
    public GameObject sadBallPrefab;
    public GameObject angryBallPrefab;
    public GameObject scaredBallPrefab;
    public GameObject deathZonePrefab;

    public float spawnTime = 1.5f;
    private float m_spawnTime;

    public int lives = 3;
    private bool m_isGameover;

    private float gameTime = 20f;
    private float currentTime;
    private bool m_isTimeStopped; 

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
        currentTime = gameTime;
        Instantiate(deathZonePrefab, new Vector2(0, 5.02f), Quaternion.identity);
        m_spawnTime = 0;
        m_isTimeStopped = false; 
    }

    private void Update()
    {
        if (m_isGameover) return; 

        if (!m_isTimeStopped) 
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                if (lives > 0)
                {
                    GameWin();
                }
                else
                {
                    GameOver();
                }
            }
        }

        if (!m_isTimeStopped) 
        {
            m_spawnTime -= Time.deltaTime;

            if (m_spawnTime <= 0)
            {
                SpawnBall();
                m_spawnTime = spawnTime;
            }
        }
    }

    public void SpawnBall()
    {
        GameObject ballPrefab = ChooseBallPrefab();

        Vector2 spawnPos = new Vector2(Random.Range(-8, 8), -5.6f);

        if (ballPrefab)
        {
            GameObject spawnedBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);

            float randomSpeed = Random.Range(0.75f, 2f);

            SetBallSpeed(spawnedBall, randomSpeed);
        }
    }

    private GameObject ChooseBallPrefab()
    {
        float randomValue = Random.value;
        if (randomValue < 0.25f)
            return happyBallPrefab;
        else if (randomValue < 0.5f)
            return sadBallPrefab;
        else if (randomValue < 0.75f)
            return angryBallPrefab;
        else
            return scaredBallPrefab;
    }

    private void SetBallSpeed(GameObject ball, float speed)
    {
        if (ball.CompareTag("RedBall"))
        {
            RedBall redBallScript = ball.GetComponent<RedBall>();
            if (redBallScript != null)
            {
                redBallScript.SetSpeed(speed);
            }
        }
        else if (ball.CompareTag("BlueBall"))
        {
            BlueBall blueBallScript = ball.GetComponent<BlueBall>();
            if (blueBallScript != null)
            {
                blueBallScript.SetSpeed(speed);
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
        m_isTimeStopped = true;
        Debug.Log("Game Over!");
    }

    private void GameWin()
    {
        m_isGameover = true;
        m_isTimeStopped = true;
        Debug.Log("You Win!");
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
