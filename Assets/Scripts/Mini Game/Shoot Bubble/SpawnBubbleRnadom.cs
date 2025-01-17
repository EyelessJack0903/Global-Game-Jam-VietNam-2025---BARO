using System.Collections;
using UnityEngine;

public class SpawnBubbleRnadom : MonoBehaviour
{
    [Header("Component")]
    public GameObject[] bubblePrefabs;
    public Transform spawnArea;

    [Header("Speed and Position")]
    public float moveSpeed = 2f;
    public EdgeCollider2D edgeCollider;
    private float leftLimit;
    private float rightLimit;

    [Header("Emotion Bubble")]
    public GameObject angryPrefab;
    public GameObject sadPrefab;
    public GameObject happyPrefab;
    public GameObject fearPrefab;

    private Rigidbody2D bubbleRigidbody;
    private CircleCollider2D circleCollider2D;
    private GameObject currentBubble;

    [Header("Bool Active")]
    private bool isBubbleActive = false;
    private bool canSpawnNewBubble = true;
    private bool canMoveBubble = true;
    private bool isWaitingForNewBubble = false; 

    void Start()
    {
        if (bubblePrefabs.Length > 0)
        {
            SpawnRandomBubble();
        }

        if (edgeCollider != null)
        {
            leftLimit = edgeCollider.bounds.min.x;
            rightLimit = edgeCollider.bounds.max.x;
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    ChangeBubbleEmotion();
        //}

        if (currentBubble != null)
        {
            MoveBubble();
        }

        if (Input.GetMouseButtonDown(0) && !isBubbleActive && canSpawnNewBubble && !isWaitingForNewBubble)
        {
            ActivateBubbleRigidbody();
        }

        if (Input.GetMouseButtonUp(0) && isBubbleActive)
        {
            canMoveBubble = false;
        }
    }
    void SpawnRandomBubble()
    {
        GameObject chosenBubblePrefab = bubblePrefabs[Random.Range(0, bubblePrefabs.Length)];
        Vector3 spawnPosition = new Vector3(spawnArea.position.x, spawnArea.position.y, 0f);

        CircleCollider2D circleCollider = chosenBubblePrefab.GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            circleCollider.enabled = false;
        }

        currentBubble = Instantiate(chosenBubblePrefab, spawnPosition, Quaternion.identity);
        bubbleRigidbody = currentBubble.GetComponent<Rigidbody2D>();
        circleCollider2D = currentBubble.GetComponent<CircleCollider2D>();
    }

    void ChangeBubbleEmotion()
    {
        if (currentBubble != null)
        {
            SpawnRandomBubble();
        }
    }

    void MoveBubble()
    {
        if (!canMoveBubble) return;

        float moveInput = Input.GetAxis("Horizontal");
        Vector3 newPosition = currentBubble.transform.position + Vector3.right * moveInput * moveSpeed * Time.deltaTime;
        float padding = 0.35f;
        newPosition.x = Mathf.Clamp(newPosition.x, leftLimit + padding, rightLimit - padding);

        currentBubble.transform.position = newPosition;
    }

    void ActivateBubbleRigidbody()
    {
        if (bubbleRigidbody != null)
        {
            isBubbleActive = true;
            circleCollider2D.enabled = true;
            bubbleRigidbody.bodyType = RigidbodyType2D.Dynamic;
            bubbleRigidbody.gravityScale = -0.05f;
        }
        StartCoroutine(WaitAndChangeBubble()); 
    }

    IEnumerator WaitAndChangeBubble()
    {
        isWaitingForNewBubble = true;
        canSpawnNewBubble = false;

        yield return new WaitForSeconds(2f); 

        SpawnRandomBubble();

        isBubbleActive = false;
        canMoveBubble = true;
        canSpawnNewBubble = true;
        isWaitingForNewBubble = false;
    }

}
