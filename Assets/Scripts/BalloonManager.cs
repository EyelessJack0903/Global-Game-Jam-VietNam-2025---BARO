using UnityEngine;
using System.Collections;

public class BalloonManager : MonoBehaviour
{
    public GameObject balloonPrefab; 
    public float spawnInterval = 2f; 
    public float balloonSpeed = 2f; 
    public int maxExplosions = 3; 
    public float gameDuration = 15f; 

    private int explosionCount = 0;
    private int score = 0;
    private int lives = 3; 

    public Transform spawnPoint; 
    public Transform topBoundary; 


    private float gameTime; 

    void Start()
    {
        gameTime = gameDuration;
        StartCoroutine(SpawnBalloon());
    }

    void Update()
    {
        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            EndGame(true);
        }
    }

    IEnumerator SpawnBalloon()
    {
        while (explosionCount < maxExplosions && gameTime > 0)
        {
            GameObject newBalloon = Instantiate(balloonPrefab, spawnPoint.position, Quaternion.identity);
            newBalloon.GetComponent<Balloon>().SetSpeed(balloonSpeed);
            newBalloon.GetComponent<Balloon>().SetManager(this);

            newBalloon.GetComponent<Balloon>().SetBalloonType(Random.Range(0, 2) == 0 ? "green" : "red");

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void BalloonExploded()
    {
        explosionCount++;
        Debug.Log($"Explosion {explosionCount}! Emoji: ??");

        if (explosionCount >= maxExplosions)
        {
            EndGame(false); 
        }
    }

    void EndGame(bool isVictory)
    {
        if (isVictory)
        {
            Debug.Log("You Win!");
        }
        else
        {
            Debug.Log("Game Over!");
        }
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log($"Lives left: {lives}");

        if (lives <= 0)
        {
            EndGame(false); 
        }
    }
}
