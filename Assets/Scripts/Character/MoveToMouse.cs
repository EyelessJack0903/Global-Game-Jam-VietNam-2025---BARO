using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    [SerializeField] private float speedMove;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    // void FixedUpdate()
    {
        // Kiểm tra nếu người chơi nhấn chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            // Lấy vị trí con trỏ chuột và chuyển đổi sang tọa độ thế giới
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z; // Giữ nguyên trục Z
            isMoving = true;
        }

        // Di chuyển nhân vật tới vị trí mục tiêu
        if (isMoving)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalk", true);

            Vector3 direction = targetPosition - transform.position;
            // Gọi hàm xử lý xoay
            FlipCharacter(direction);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMove * Time.deltaTime);
            // Dừng di chuyển nếu đã đến gần vị trí mục tiêu
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalk", false);
            }

        }
    }
    private void FlipCharacter(Vector3 direction)
    {
        // Lật nhân vật dựa trên hướng di chuyển
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Di chuyển ngang
            transform.localScale = new Vector3(direction.x > 0 ? -1 : 1, 1, 1);
        }
        else
        {
            // Di chuyển dọc
            transform.localScale = new Vector3(1, direction.y > 0 ? 1 : 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Nếu va chạm với một vật có Collider, dừng di chuyển
        isMoving = false;
    }
}
