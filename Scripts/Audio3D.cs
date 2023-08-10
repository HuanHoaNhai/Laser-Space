using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio3D : MonoBehaviour
{
    public float speed = 1f;
    public float minX = -5f;
    public float maxX = 5f;
    public float returnSpeed = 0.5f; // Tốc độ di chuyển dần về vị trí ban đầu
    private Vector3 startPosition;
    private bool isMovingRight = true;
    private bool isReturning = false; // Biến đánh dấu xem đối tượng có đang di chuyển về vị trí ban đầu hay không
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        // Nếu đang quay về vị trí ban đầu
        if (isReturning)
        {
            // Di chuyển đối tượng về vị trí ban đầu dần dần với tốc độ returnSpeed
            transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);

            // Kiểm tra xem đã quay trở về vị trí ban đầu hay chưa
            if (transform.position == startPosition)
            {
                isReturning = false; // Dừng việc di chuyển về vị trí ban đầu
            }
        }
        else
        {
            float direction = (isMovingRight) ? 1 : -1;
            Vector2 movement = new Vector2(direction * speed, 0f);
            transform.Translate(movement * Time.deltaTime);

            // Kiểm tra xem đối tượng đã điều hướng hết khoảng di chuyển hay chưa
            if ((isMovingRight && transform.position.x >= maxX) || (!isMovingRight && transform.position.x <= minX))
            {
                // Nếu đã điều hướng hết khoảng di chuyển, đổi hướng và bắt đầu di chuyển về vị trí ban đầu
                isMovingRight = !isMovingRight;
                isReturning = true; // Bắt đầu di chuyển về vị trí ban đầu
            }
        }
    }
}
