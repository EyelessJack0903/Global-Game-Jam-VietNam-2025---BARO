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

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMove * Time.deltaTime);
            // Dừng di chuyển nếu đã đến gần vị trí mục tiêu
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                Flip();
                isMoving = false;
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalk", false);
            }

        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Nếu va chạm với một vật có Collider, dừng di chuyển
        isMoving = false;
    }
}
