using System.Collections;
using UnityEngine;

public abstract class BaseBubbleCollisionHandler : MonoBehaviour
{
    [Header("Prefab to Spawn")]
    public GameObject spawnPrefab;

    private bool hasHandledCollision = false; 
    protected float collisionCooldown = 0.5f;

    private EmotionManager EmotionManager;

    private void Start()
    {
        EmotionManager = FindFirstObjectByType<EmotionManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasHandledCollision) return;

        BubbleType thisBubble = GetComponent<BubbleType>();
        BubbleType otherBubble = collision.gameObject.GetComponent<BubbleType>();

        if (thisBubble != null && otherBubble != null && IsMatchingType(thisBubble.bubble, otherBubble.bubble))
        {
            BaseBubbleCollisionHandler otherHandler = collision.gameObject.GetComponent<BaseBubbleCollisionHandler>();
            if (otherHandler != null && !otherHandler.hasHandledCollision)
            {
                hasHandledCollision = true;
                otherHandler.hasHandledCollision = true;

                StartCoroutine(HandleCollision(this.gameObject, collision.gameObject));
            }
        }
    }

    protected abstract bool IsMatchingType(BubbleType.BubbleEmotion thisEmotion, BubbleType.BubbleEmotion otherEmotion);

    private IEnumerator HandleCollision(GameObject bubble1, GameObject bubble2)
    {
        EmotionManager.AdjustEmotion("happy", 1f);

        DisableBubble(bubble1);
        DisableBubble(bubble2);

        Vector3 spawnPosition = (bubble1.transform.position + bubble2.transform.position) / 2;
        GameObject newBubble = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);

        CircleCollider2D collider = newBubble.GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        Rigidbody2D rb = newBubble.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = -0.1f;
        }

        yield return new WaitForSeconds(3f);
        Destroy(bubble1);
        Destroy(bubble2);

        yield return new WaitForSeconds(collisionCooldown);
    }

    private void DisableBubble(GameObject bubble)
    {
        bubble.GetComponent<Renderer>().enabled = false;
        Collider2D collider = bubble.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        Debug.Log($"Bubble disabled: {bubble.name}");
    }
}
