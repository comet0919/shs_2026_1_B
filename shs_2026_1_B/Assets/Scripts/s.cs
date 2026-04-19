using UnityEngine;
using System.Collections;
public class SpeedBoostItem : MonoBehaviour
{
    public float boostMultiplier = 2.0f;
    public float duration = 5.0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                StartCoroutine(ApplySpeedBoost(player));
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
    IEnumerator ApplySpeedBoost(PlayerController player)
    {
        float originalSpeed = player.moveSpeed;
        player.moveSpeed *= boostMultiplier;
        Debug.Log("이동 속도 증가!");
        yield return new WaitForSeconds(duration);
        player.moveSpeed = originalSpeed;
        Debug.Log("이동 속도 정상화");
        Destroy(gameObject);
    }
}