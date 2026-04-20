using UnityEngine;
public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float raycastDistance = 2f;
    public float traceDistance = 10f;
    private Transform Player;
    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) Player = playerObj.transform;
    }
    private void Update()
    {
        if (Player == null) return;
        Vector2 direction = Player.position - transform.position;
        float distance = direction.magnitude;
        if (distance > traceDistance)
        {
            return;
        }
        Vector2 directionNormalized = direction.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionNormalized, raycastDistance, LayerMask.GetMask("Obstacle"));
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            Vector3 alternativeDirection = Quaternion.Euler(0f, 0f, -90f) * directionNormalized;
            transform.Translate(alternativeDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(directionNormalized * moveSpeed * Time.deltaTime);
        }
    }
}