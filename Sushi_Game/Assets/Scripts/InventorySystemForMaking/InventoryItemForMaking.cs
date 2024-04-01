using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemForMaking : MonoBehaviour
{
    public Item item;
    public Transform parentAfterDrag;
    public InventoryManager inventoryManager;
    public TMP_Text itemCountText;
    
    public int itemCount = 1;
    public Image image;
    
    public void Start()
    {
        
    }

    public void InitialiseItemFromLastScene(Sprite imageFromLastScene, int itemCountFromLastScene)
    {
        image.sprite = imageFromLastScene;
        itemCount = itemCountFromLastScene;
        itemCountText.text = itemCount.ToString();
    }
    
    public void InitialiseItem(Item item, InventoryManagerForMaking inventoryManagerForMaking)
    {
        
    }
}
