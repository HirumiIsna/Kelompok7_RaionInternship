using UnityEngine;

public class Bahan : MonoBehaviour, IItem
{
    public void Collect()
    {
        Debug.Log("Bahan collected!");
        Destroy(gameObject);
    }
}
