using UnityEngine;

public class InventoryManagerForMaking : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    
    public void Start()
    {
        
    }

    public void FindSlot(Item item)
    {
        for (int i = 0; 
             i < inventorySlots.Length; 
             i++)
        {
            InventorySlot slot = inventorySlots[i];
            DraggingItem itemInSlot = slot.GetComponentInChildren<DraggingItem>();
            
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }
    }
    
    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        
    }
}
