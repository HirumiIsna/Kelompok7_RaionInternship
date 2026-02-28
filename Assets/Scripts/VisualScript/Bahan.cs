using System.Resources;
using UnityEngine;

public class Bahan : MonoBehaviour, IItem
{   
    public void Collect()//DropItem3(Clone)
    {
        switch (gameObject.name)
        {
            case "DropItem1(Clone)":
                ResourceManager.AddBahan1Amount();
                Destroy(gameObject);
                break;
            case "DropItem2(Clone)":
                ResourceManager.AddBahan2Amount();
                Destroy(gameObject);
                break;
            case "DropItem3(Clone)":
                ResourceManager.AddBahan3Amount();
                Destroy(gameObject);
                break;
        }
    }
}
