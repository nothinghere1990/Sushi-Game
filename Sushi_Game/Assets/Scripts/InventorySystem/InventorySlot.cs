using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Image image;
    
    public void Dropped()
    {
        inventoryManager.clickedInventorySlot = transform;
        inventoryManager.ManagerDropped();
    }
}
