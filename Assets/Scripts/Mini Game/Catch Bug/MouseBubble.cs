using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseBubble : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    public float bugSpeed = 0.5f;
    public float checkBug = 2f;

    [Header("Coins")]
    public int coinPerBug = 10;

    [Header("Component")]
    public Transform bugContainer;
    public BoxCollider2D boxCollider2D;

    private Vector3 targetPosition;
    private List<GameObject> caughtBugs = new List<GameObject>();
    private CircleCollider2D circleCollider2D;

    private bool canCatchBugs = true;
    private SpawnBug bugSpawn;
    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();

        if (circleCollider2D == null)
        {
            Debug.LogError("CircleCollider2D not found on " + gameObject.name);
        }
    }

    private void Start()
    {
        bugSpawn = FindFirstObjectByType<SpawnBug>();

        Vector3 center = boxCollider2D.bounds.center;
        transform.position = center;
        targetPosition = center;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (bugContainer == null)
        {
            Debug.LogError("BugContainer not assigned in the inspector.");
        }
    }

private void Update()
{
    Vector3 mousePosition = Input.mousePosition;
    mousePosition.z = -Camera.main.transform.position.z;

    targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

    Bounds bounds = boxCollider2D.bounds;

    // Giới hạn vị trí mục tiêu trong phạm vi BoxCollider2D
    targetPosition.x = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x);
    targetPosition.y = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y);

    // Đặt giá trị z = 10
    targetPosition.z = 10f;

    // Di chuyển đối tượng dần dần về phía vị trí mục tiêu với tốc độ moveSpeed
    transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

    if (Input.GetMouseButtonDown(0))
    {
        TryCatchBug();
    }

    if (Input.GetMouseButtonDown(1))
    {
        TryReleaseBugs();
    }

    MoveBugsInsideCircle();
}

    private void TryCatchBug()
    {
        if (!canCatchBugs) return;

        Vector3 bubblePosition = transform.position;

        int bugLayer = LayerMask.GetMask("Bug");

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(bubblePosition, checkBug, bugLayer);

        int caughtBugsCount = caughtBugs.Count;

        // Kiểm tra nếu giỏ đầy
        if (caughtBugsCount >= 5)
        {
            Debug.Log("Cannot catch more bugs. Giỏ đã đầy.");
            return;
        }

        List<GameObject> alreadyCaughtBugs = new List<GameObject>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Bug"))
            {
                GameObject caughtBug = hitCollider.gameObject;

                if (!alreadyCaughtBugs.Contains(caughtBug) && !caughtBugs.Contains(caughtBug))
                {
                    caughtBug.transform.SetParent(bugContainer);
                    caughtBug.transform.position = circleCollider2D.transform.position;

                    caughtBug.GetComponent<BugMove>().SetMovementArea(circleCollider2D);
                    caughtBugs.Add(caughtBug); 

                    alreadyCaughtBugs.Add(caughtBug); 

                    if (caughtBugs.Count >= 5)
                    {
                        canCatchBugs = false;  
                        Debug.Log("You have caught 5 bugs! You cannot catch more until you release some.");
                        break;  
                    }
                }
            }
        }
    }

    private void TryReleaseBugs()
    {
        int trashLayer = LayerMask.GetMask("Trash");

        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, checkBug, trashLayer);

        if (hitCollider != null)
        {
            Debug.Log("Trash detected! Releasing all bugs...");
            ReleaseAllBugs();
        }
    }

    private void ReleaseAllBugs()
    {
        int totalCoins = 0;

        foreach (var bug in caughtBugs)
        {
            if (bug != null)
            {
                totalCoins += coinPerBug;
                if (bugSpawn != null)
                {
                    bugSpawn.currentSpawn--;
                }
                Destroy(bug);
            }
        }

        caughtBugs.Clear(); 
        canCatchBugs = true;
        Debug.Log("Released all bugs. Total Bugs: " + caughtBugs.Count + ", Total Coins: " + totalCoins);
    }


    private void MoveBugsInsideCircle()
    {
        foreach (var bug in caughtBugs)
        {
            if (bug != null)
            {
                Vector3 bugTargetPosition = circleCollider2D.transform.position;

                float distance = Vector3.Distance(bug.transform.position, bugTargetPosition);

                if (distance > 0.1f)
                {
                    Vector3 direction = (bug.transform.position - bugTargetPosition).normalized;
                    bug.transform.position = bugTargetPosition + direction * 0.5f; 
                }

                bug.transform.position = Vector3.MoveTowards(bug.transform.position, bugTargetPosition, bugSpeed * Time.deltaTime); 
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkBug);
    }
}
