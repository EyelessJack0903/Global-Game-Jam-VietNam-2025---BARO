using System.Collections;
using UnityEngine;

public class BubbleCollisionHandler : MonoBehaviour
{
    public GameObject angryPrefab;
    public GameObject sadPrefab;
    public GameObject happyPrefab;
    public GameObject fearPrefab;

    private float collisionCooldown = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BubbleType thisBubble = GetComponent<BubbleType>();
        BubbleType otherBubble = collision.gameObject.GetComponent<BubbleType>();

        if (thisBubble != null && otherBubble != null)
        {
            if (thisBubble.bubble == BubbleType.BubbleEmotion.Angry && otherBubble.bubble == BubbleType.BubbleEmotion.Angry)
            {
                StartCoroutine(HandleCollision(this.gameObject, collision.gameObject, fearPrefab));
            }
            else if (thisBubble.bubble == BubbleType.BubbleEmotion.Fear && otherBubble.bubble == BubbleType.BubbleEmotion.Fear)
            {
                StartCoroutine(HandleCollision(this.gameObject, collision.gameObject, sadPrefab));
            }
            else if (thisBubble.bubble == BubbleType.BubbleEmotion.Sad && otherBubble.bubble == BubbleType.BubbleEmotion.Sad)
            {
                StartCoroutine(HandleCollision(this.gameObject, collision.gameObject, happyPrefab));
            }
        }
    }

    private IEnumerator HandleCollision(GameObject bubble1, GameObject bubble2, GameObject bubbleToSpawn)
    {
        DisableBubble(bubble1);
        DisableBubble(bubble2);

        Vector3 spawnPosition = (bubble1.transform.position + bubble2.transform.position) / 2;
        GameObject newBubble = Instantiate(bubbleToSpawn, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = newBubble.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = -0.01f;
        }

        yield return new WaitForSeconds(2f);

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
    }
}
