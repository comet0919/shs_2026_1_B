using UnityEngine;
using System.Collections;

public class HardMovingPlatform : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.right;
    public float speed = 2.0f;
    public float moveDuration = 3.0f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.useFullKinematicContacts = true;

        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            float timer = 0f;
            Vector2 currentDir = moveDirection.normalized;

            while (timer < moveDuration)
            {
                // yield return null 대신 WaitForFixedUpdate를 사용하면 
                // 물리 연산 주기와 딱 맞아떨어져서 떨림이 사라집니다.
                yield return new WaitForFixedUpdate();

                timer += Time.fixedDeltaTime; // FixedUpdate 주기에 맞게 더해줌

                Vector2 nextPosition = rb.position + (currentDir * speed * Time.fixedDeltaTime);
                rb.MovePosition(nextPosition);
            }

            moveDirection *= -1;
        }
    }

    // 플레이어가 올라탔을 때 미끄러지지 않게 부모 설정
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}