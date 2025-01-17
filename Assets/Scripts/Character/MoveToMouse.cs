using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    [SerializeField] private float speedMove;

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
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
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMove * Time.deltaTime);

            // Dừng di chuyển nếu đã đến gần vị trí mục tiêu
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}
