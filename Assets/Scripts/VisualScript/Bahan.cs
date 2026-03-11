using System.Resources;
using UnityEngine;

public class Bahan : MonoBehaviour, IItem
{   
    public void Collect()
    {
        switch (gameObject.name)
        {
            case "DropItem1(Clone)":
                ResourceManager.AddBahanAmount(ResourceManager.ResourceType.Bahan1);
                Destroy(gameObject);
                break;
            case "DropItem2(Clone)":
                ResourceManager.AddBahanAmount(ResourceManager.ResourceType.Bahan2);
                Destroy(gameObject);
                break;
            case "DropItem3(Clone)":
                ResourceManager.AddBahanAmount(ResourceManager.ResourceType.Bahan3);
                Destroy(gameObject);
                break;
            case "DropItem4(Clone)":
                ResourceManager.AddBahanAmount(ResourceManager.ResourceType.Bahan4);
                Destroy(gameObject);
                break;
        }
    }
}
