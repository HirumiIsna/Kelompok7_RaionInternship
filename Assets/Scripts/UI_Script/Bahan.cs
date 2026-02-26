using UnityEngine;
using System.Collections;
using System.Collections.Generic;   
public class Bahan : MonoBehaviour, IItem
{
    public void Collect()
    {
            Debug.Log("Bahan collected!");
            Destroy(gameObject);     
    }
}
