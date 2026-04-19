using UnityEngine;
using System.Collections;

public class GlobalAntiRespawn : MonoBehaviour
{
    public float duration = 5.0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DisableAllHazards(duration));
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
    IEnumerator DisableAllHazards(float time)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] hazards = GameObject.FindGameObjectsWithTag("Respawn");
        SetColliders(enemies, false);
        SetColliders(hazards, false);
        Debug.Log("비활성화");
        yield return new WaitForSeconds(time);
        SetColliders(enemies, true);
        SetColliders(hazards, true);
        Debug.Log("활성화");
        Destroy(gameObject);
    }
    void SetColliders(GameObject[] objects, bool state)
    {
        foreach (GameObject obj in objects)
        {
            Collider2D col = obj.GetComponent<Collider2D>();
            if (col != null) col.enabled = state;
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = state ? Color.white : new Color(1, 1, 1, 0.3f);
        }
    }
}