using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DraggingItem : MonoBehaviour
{
    [HideInInspector] public Item item;
    public Transform parentAfterDrag;
    [HideInInspector] public Transform parentAfterDrag2;
    [HideInInspector] public InventoryManager inventoryManager;
    [HideInInspector] public Toggle multiplyByTen;
    [HideInInspector] public int itemCountAfterDrag;
    [HideInInspector] public TMP_Text itemCountText;
    [HideInInspector] public MoneyController moneyController;

    public int itemCount = 1;
    public int itemPrice;
    public Image image;
    public bool isDragging;


    public void InitialiseItem(Item newItem, InventoryManager inventoryManager, bool random, int itemCountAfterDrag,
        MoneyController moneyController)
    {
        item = newItem;
        image.sprite = newItem.image;
        itemPrice = item.price;
        this.inventoryManager = inventoryManager;
        this.moneyController = moneyController;

        if (random)
        {
            itemCount = item.isRice ? Random.Range(30, 61) : Random.Range(20, 41);
            inventoryManager.currentDraggingItem = null;
        }
        else
        {
            itemCount = itemCountAfterDrag;
        }

        RefreshCount();
    }

    public void RefreshCount()
    {
        itemCountText.text = itemCount.ToString();
    }

    public void Start()
    {
        parentAfterDrag = transform.parent;
        parentAfterDrag2 = transform.parent;
        multiplyByTen = inventoryManager.multiplyByTen.GetComponent<Toggle>();
    }

    public void CombineItems()
    {
        for (int i = 0;
             i < inventoryManager.inventorySlots.Length;
             i++)
        {
            if (!inventoryManager.modeSwitch &&
                transform.parent == inventoryManager.inventorySlots[i].transform)
            {
                for (int j = 0;
                     j < inventoryManager.inventorySlots.Length;
                     j++)
                {
                    if (inventoryManager.currentDraggingItem != null &&
                        inventoryManager.currentDraggingItem.parentAfterDrag ==
                        inventoryManager.inventorySlots[j].transform)
                    {
                        if (i < inventoryManager.inventorySlots.Length / 2 &&
                            j >= inventoryManager.inventorySlots.Length / 2)
                        {
                            moneyController.StockingEarnMoney(itemPrice * inventoryManager.currentDraggingItem.itemCount);
                        }
                        else if (i >= inventoryManager.inventorySlots.Length / 2 &&
                                 j < inventoryManager.inventorySlots.Length / 2)
                        {
                            moneyController.StockingSpendMoney(itemPrice * inventoryManager.currentDraggingItem.itemCount);
                        }
                    }
                }
            }
        }

        if (inventoryManager.currentDraggingItem != null &&
            inventoryManager.currentDraggingItem.image.sprite == image.sprite)
        {
            itemCount += inventoryManager.currentDraggingItem.itemCount;
            RefreshCount();
            Destroy(inventoryManager.currentDraggingItem.GameObject());
            inventoryManager.currentDraggingItem = null;

            for (int i = 0;
                 i < inventoryManager.inventorySlots.Length;
                 i++)
            {
                DraggingItem itemInSlot = inventoryManager.inventorySlots[i].GetComponentInChildren<DraggingItem>();
                if (itemInSlot != null)
                {
                    itemInSlot.image.raycastTarget = true;
                }
            }
        }
        else
        {
            Dragging();
        }
    }

    public void Dragging()
    {
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        parentAfterDrag.GetComponent<InventorySlot>().image.raycastTarget = true;

        //Arrange mode is on.
        if (inventoryManager.modeSwitch &&
            inventoryManager.currentDraggingItem != null &&
            inventoryManager.currentDraggingItem.isDragging)
        {
            inventoryManager.currentDraggingItem.transform.SetParent(parentAfterDrag);
            parentAfterDrag2 = inventoryManager.currentDraggingItem.parentAfterDrag;
            inventoryManager.currentDraggingItem.parentAfterDrag = parentAfterDrag;
            inventoryManager.currentDraggingItem.isDragging = false;
            inventoryManager.currentDraggingItem.image.raycastTarget = true;
            parentAfterDrag.GetComponent<InventorySlot>().image.raycastTarget = false;
        }

        inventoryManager.currentDraggingItem = transform.GetComponent<DraggingItem>();

        //Buy and sell mode is on.
        if (!inventoryManager.modeSwitch &&
            !inventoryManager.currentDraggingItem.isDragging)
        {
            for (int i = 0;
                 i < inventoryManager.inventorySlots.Length;
                 i++)
            {
                DraggingItem itemInSlot = inventoryManager.inventorySlots[i].GetComponentInChildren<DraggingItem>();

                if (itemInSlot != null &&
                    itemInSlot.image.sprite != inventoryManager.currentDraggingItem.image.sprite)
                {
                    itemInSlot.image.raycastTarget = false;
                }
            }

            SplitItems();
        }

        isDragging = true;
        image.raycastTarget = false;
    }

    public void SplitItems()
    {
        int tenOrOne = multiplyByTen.isOn ? 10 : 1;
        itemCountAfterDrag = itemCount - tenOrOne;

        if (itemCountAfterDrag > 0)
        {
            inventoryManager.SpawnNewItem(item, parentAfterDrag.GetComponent<InventorySlot>(), false,
                itemCountAfterDrag);
        }

        if (itemCount >= 10)
        {
            itemCount = tenOrOne;
        }
        else if (!multiplyByTen.isOn)
        {
            itemCount = 1;
        }

        if (parentAfterDrag.childCount != 0)
        {
            parentAfterDrag.GetComponent<Image>().raycastTarget = false;
        }

        RefreshCount();
    }

    public void BackToSlot()
    {
        if (!inventoryManager.modeSwitch &&
            inventoryManager.currentDraggingItem.isDragging)
        {
            for (int i = 0;
                 i < inventoryManager.inventorySlots.Length;
                 i++)
            {
                DraggingItem itemInSlot = inventoryManager.inventorySlots[i].GetComponentInChildren<DraggingItem>();

                if (itemInSlot != null)
                {
                    itemInSlot.image.raycastTarget = true;
                }
            }
        }

        if (parentAfterDrag.childCount < 2)
        {
            transform.SetParent(parentAfterDrag);
        }
        else if (parentAfterDrag.GetComponentInChildren<DraggingItem>().image.sprite ==
                 inventoryManager.currentDraggingItem.image.sprite)
        {
            parentAfterDrag.GetComponentInChildren<DraggingItem>().CombineItems();
        }
        else
        {
            transform.SetParent(parentAfterDrag2);
            parentAfterDrag = parentAfterDrag2;
        }

        isDragging = false;
        image.raycastTarget = true;
        inventoryManager.currentDraggingItem = null;
        parentAfterDrag.GetComponent<InventorySlot>().image.raycastTarget = false;
    }

    public void Update()
    {
        if (isDragging)
        {
            transform.position = Input.mousePosition;
        }
    }
}