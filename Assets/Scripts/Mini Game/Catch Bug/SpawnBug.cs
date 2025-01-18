using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SpawnBug : MonoBehaviour
{
    public GameObject bug;
    public Transform[] spawnPositions;

    public float spawnDelay;
    public float spawnDuration = 20f;
    private bool canSpawn = true;

    public int maxSpawn = 10;
    public int currentSpawn;

    public int coinPerBug = 10;

    private float spawnTimer = 0f;
    private MiniGameController miniGame;
    private void Start()
    {
        miniGame = FindFirstObjectByType<MiniGameController>();
        StartCoroutine(SpawnBugs());
    }
private IEnumerator SpawnBugs()
{
    while (canSpawn)
    {
        if (spawnTimer >= spawnDuration)
        {
            canSpawn = false;
            StartCoroutine(CheckRemainingBugs());
            yield break;
        }

        if (currentSpawn < maxSpawn)
        {
            yield return new WaitForSeconds(spawnDelay);

            int randomIndex = Random.Range(0, spawnPositions.Length);
            Transform spawnPosition = spawnPositions[randomIndex];

            // Đặt z = 0 bằng cách tạo một Vector3 mới
            Vector3 spawnPosAdjusted = new Vector3(spawnPosition.position.x, spawnPosition.position.y, 10f);

            // Tạo bug tại vị trí đã điều chỉnh
            Instantiate(bug, spawnPosAdjusted, Quaternion.identity);
            currentSpawn++;
        }

        spawnTimer += spawnDelay;
        yield return null;
    }
}

    private IEnumerator CheckRemainingBugs()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            GameObject[] remainingBugs = GameObject.FindGameObjectsWithTag("Bug");

            if (remainingBugs.Length == 0 && spawnTimer >= spawnDuration)
            {
                Debug.Log("het bug");
                miniGame.CloseComputer();
                miniGame.isFinishComputerMinigame = true;
                yield break;
            }
        }
    }

}
