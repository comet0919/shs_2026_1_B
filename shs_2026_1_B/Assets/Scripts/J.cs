using UnityEngine;
using System.Collections;

public class LowGravityItem : MonoBehaviour
{
    public float gravityMultiplier = 0.5f;
    public float duration = 5.0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                StartCoroutine(ApplyLowGravity(rb));
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
    IEnumerator ApplyLowGravity(Rigidbody2D rb)
    {
        float originalGravity = rb.gravityScale;
        rb.gravityScale *= gravityMultiplier;
        Debug.Log("중력 감so");
        yield return new WaitForSeconds(duration);
        if (rb != null)
        {
            rb.gravityScale = originalGravity;
            Debug.Log("정상");
        }
        Destroy(gameObject);
    }
}