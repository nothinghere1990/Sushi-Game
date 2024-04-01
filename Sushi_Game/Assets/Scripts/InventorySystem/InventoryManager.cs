using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [HideInInspector] public Transform clickedInventorySlot;

    public DraggingItem currentDraggingItem;
    public bool modeSwitch;
    public Transform multiplyByTen;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public MoneyController moneyController;
    public Sprite[] nonStaticCarryPlayerIngredientImgs;
    public int[] nonStaticCarryPlayerIngredientCounts;

    public static Sprite[] CarryPlayerIngredientImgs;
    public static int[] CarryPlayerIngredientCounts;

    public void Start()
    {
        ModeSwitch(multiplyByTen.GetComponent<Toggle>().isOn);
        nonStaticCarryPlayerIngredientImgs = new Sprite[inventorySlots.Length / 2];
        nonStaticCarryPlayerIngredientCounts = new int[inventorySlots.Length / 2];
    }

    public void ModeSwitch(bool on)
    {
        modeSwitch = on;
        multiplyByTen.GetComponent<Toggle>().enabled = !on;
        multiplyByTen.GetChild(0).gameObject.SetActive(!on);
        DraggingItem itemInSlot;

        if (!on)
        {
            for (int i = 0;
                 i < inventorySlots.Length;
                 i++)
            {
                itemInSlot = inventorySlots[i].GetComponentInChildren<DraggingItem>();

                if (itemInSlot != null)
                {
                    inventorySlots[i].image.raycastTarget = true;
                    itemInSlot.image.raycastTarget = true;
                }
            }
        }
        else
        {
            for (int i = 0;
                 i < inventorySlots.Length;
                 i++)
            {
                itemInSlot = inventorySlots[i].GetComponentInChildren<DraggingItem>();

                if (itemInSlot != null &&
                    i < inventorySlots.Length / 2)
                {
                    inventorySlots[i].image.raycastTarget = false;
                    itemInSlot.image.raycastTarget = false;
                }
            }
        }
    }

    public bool FindSlot(Item item)
    {
        for (int i = 0;
             i < inventorySlots.Length;
             i++)
        {
            InventorySlot slot = inventorySlots[i];
            DraggingItem itemInSlot = slot.GetComponentInChildren<DraggingItem>();

            if (itemInSlot == null)
            {
                slot.image.raycastTarget = false;
                SpawnNewItem(item, slot, true, 1);
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot, bool random, int itemCountAfterDrag)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        DraggingItem draggingItem = newItemGo.GetComponent<DraggingItem>();
        draggingItem.InitialiseItem(item, transform.GetComponent<InventoryManager>(), random, itemCountAfterDrag,
            moneyController);
    }

    public void CurrentBackToSlot()
    {
        currentDraggingItem.BackToSlot();
    }

    public void ManagerDropped()
    {
        if (!modeSwitch &&
            currentDraggingItem.isDragging)
        {
            for (int i = 0;
                 i < inventorySlots.Length;
                 i++)
            {
                DraggingItem itemInSlot = inventorySlots[i].GetComponentInChildren<DraggingItem>();

                if (itemInSlot != null)
                {
                    itemInSlot.image.raycastTarget = true;
                }
            }
        }

        if (currentDraggingItem.isDragging)
        {
            currentDraggingItem.image.raycastTarget = true;
            currentDraggingItem.transform.SetParent(clickedInventorySlot);
            currentDraggingItem.parentAfterDrag = clickedInventorySlot;
            currentDraggingItem.isDragging = false;
            currentDraggingItem = null;
            clickedInventorySlot.GetComponent<InventorySlot>().image.raycastTarget = false;
        }
    }

    public void BringItemToNextScene()
    {
        int i = inventorySlots.Length / 2;

        for (int j = 0;
             j < inventorySlots.Length / 2;
             j++)
        {
            if (inventorySlots[i].GetComponentInChildren<DraggingItem>() != null)
            {
                nonStaticCarryPlayerIngredientImgs[j] = inventorySlots[i].transform.GetChild(1).GetComponent<Image>().sprite;
                nonStaticCarryPlayerIngredientCounts[j] =
                    inventorySlots[i].GetComponentInChildren<DraggingItem>().itemCount;
            }

            i++;
        }

        CarryPlayerIngredientImgs = nonStaticCarryPlayerIngredientImgs;
        CarryPlayerIngredientCounts = nonStaticCarryPlayerIngredientCounts;
    }
}