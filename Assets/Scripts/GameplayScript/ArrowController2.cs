using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    public void DeflectArrow()
    {
        StartCoroutine(Deflect());
    }

    private IEnumerator Deflect()
    {
        Time.timeScale = 0.2f;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        yield return new WaitForSeconds(.125f);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
