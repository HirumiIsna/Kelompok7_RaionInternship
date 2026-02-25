using UnityEngine;

public class Note : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject noteUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player got a Note");
            noteUI.SetActive(true);
            Destroy(gameObject);
        }
    }
}
