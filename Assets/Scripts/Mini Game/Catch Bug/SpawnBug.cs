using System.Collections;
using UnityEngine;

public class SpawnBug : MonoBehaviour
{
    public GameObject bug;
    public Transform[] spawnPositions;

    public float spawnDelay;
    private bool canSpawn = true;

    public int maxSpawn = 10;
    public int currentSpawn;

    private void Start()
    {
        StartCoroutine(SpawnBugs());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && currentSpawn > 0)
        {
            currentSpawn--;
            DestroyBug();
        }
    }

    private void DestroyBug()
    {
        GameObject[] bugs = GameObject.FindGameObjectsWithTag("Bug");

        if (bugs.Length > 0)
        {
            Destroy(bugs[bugs.Length - 1]); 
        }
    }

    private IEnumerator SpawnBugs()
    {
        while (canSpawn)
        {
            if (currentSpawn < maxSpawn)
            {
                yield return new WaitForSeconds(spawnDelay);

                int randomIndex = Random.Range(0, spawnPositions.Length);
                Transform spawnPosition = spawnPositions[randomIndex];

                Instantiate(bug, spawnPosition.position, Quaternion.identity);
                currentSpawn++;
            }

            yield return null;  
        }
    }
}
