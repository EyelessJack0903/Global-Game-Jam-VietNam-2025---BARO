using UnityEngine;

public class BugMove : MonoBehaviour
{
    public float moveSpeed;
    public int changeDirectionInterval;

    public BoxCollider2D boxCollider2D;
    private Vector3 targetPosition;
    private float timer;
    private bool canMove = true;

    private void Start()
    {
        GameObject area = GameObject.FindGameObjectWithTag("BoxCollider");
        if (area != null)
        {
            boxCollider2D = area.GetComponent<BoxCollider2D>();
        }
        else
        {
            Debug.Log("Khong tim thay box collider");
        }

        GenerateRandomTarget();
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f || timer <= 0)
            {
                GenerateRandomTarget();
                timer = changeDirectionInterval;
            }

            timer -= Time.deltaTime;
        }
    }

    private void GenerateRandomTarget()
    {
        if (boxCollider2D == null) return;

        Bounds bounds = boxCollider2D.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(0, 0);

        // Đặt vị trí mục tiêu mới
        targetPosition = new Vector3(randomX, randomY, randomZ);

    }

    public void SetMovementArea(CircleCollider2D bubbleCollider)
    {
        if (bubbleCollider == null) return;

        float radius = bubbleCollider.radius;

        Bounds bounds = bubbleCollider.bounds;

        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y),
            targetPosition.z);
    }


    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void EnableMovement(bool isEnabled)
    {
        canMove = isEnabled;
    }
}
