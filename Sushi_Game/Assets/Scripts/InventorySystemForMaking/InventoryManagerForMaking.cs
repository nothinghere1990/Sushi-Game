using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InventoryManagerForMaking : MonoBehaviour
{
    public GameObject inventoryItemPrefabForMaking;
    public Transform draggingItem;
    public Transform craftingResultSlot;
    public MoneyController moneyController;
    //public List<Item> matchSushiList;
    
    public float remainingTime;

    public InventorySlotForMaking[] playerIngredientSlots;
    public Item[] sushiArray;
    public Item[] sushiOrdersArray;
    public Dictionary<int, Sprite[]> RecipesDict = new Dictionary<int, Sprite[]>();
    public Transform[] craftingSlots;
    public Transform[] servingTable;
    public Transform[] orderHanger;
    

    public void Start()
    {
        StartCoroutine(OrderMatching());
        RecipesDict.Add(0, sushiArray[0].recipe1);
        RecipesDict.Add(1, sushiArray[0].recipe2);
        RecipesDict.Add(2, sushiArray[0].recipe3);
        BringItemFromLastScene();
        
    }

    public void BringItemFromLastScene()
    {
        for (int i = 0;
             i < InventoryManager.CarryPlayerIngredientImgs.Length;
             i++)
        {
            if (InventoryManager.CarryPlayerIngredientImgs[i] != null)
            {
                GameObject newItemGo = Instantiate(inventoryItemPrefabForMaking, playerIngredientSlots[i].transform);
                InventoryItemForMaking inventoryItemForMaking = newItemGo.GetComponent<InventoryItemForMaking>();
                inventoryItemForMaking.InitializeItemFromLastScene(
                    InventoryManager.CarryPlayerIngredientImgs[i],
                    InventoryManager.CarryPlayerIngredientCounts[i],
                    transform.GetComponent<InventoryManagerForMaking>());
            }
        }
    }

    public void ClickOutsideOfSlots()
    {
        if (draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag.childCount == 0)
        {
            draggingItem.SetParent(draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag);
            draggingItem.GetComponent<Image>().raycastTarget = true;
            draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag = null;
            draggingItem = null;
        }
        else
        {
            draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag
                .GetComponentInChildren<InventoryItemForMaking>().CheckIfHoldItemAndPutBackAndPickup();
        }
    }

    public void Crafting()
    {
        foreach (var sushi in sushiArray)
        {
            RecipesDict[0] = sushi.recipe1;
            RecipesDict[1] = sushi.recipe2;
            RecipesDict[2] = sushi.recipe3;

            for (int i = 0; i < RecipesDict.Count; i++)
            {
                for (int j = 0; j < craftingSlots.Length; j++)
                {
                    Debug.Log(j);

                    if (craftingSlots[j].childCount == 0 &&
                        RecipesDict[i][j] == null)
                    {
                        Debug.Log("Null");
                    }
                    else if (craftingSlots[j].childCount != 0 &&
                             craftingSlots[j].GetChild(0).GetComponent<Image>().sprite == RecipesDict[i][j])
                    {
                        Debug.Log("Right Recipe");
                    }
                    else
                    {
                        Debug.Log("Wrong Recipe");
                        break;
                    }

                    if (j == craftingSlots.Length - 1)
                    {
                        CompleteCrafting(sushi.image);
                        return;
                    }
                }
            }
        }

        if (draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag != craftingResultSlot)
        {
            Destroy(craftingResultSlot.GetChild(0).gameObject);
        }
    }

    public void CompleteCrafting(Sprite sushiImage)
    {
        if (craftingResultSlot.childCount != 0)
        {
            Destroy(craftingResultSlot.GetChild(0).gameObject);
        }

        GameObject newItemGo = Instantiate(inventoryItemPrefabForMaking, craftingResultSlot);
        InventoryItemForMaking inventoryItemForMaking = newItemGo.GetComponent<InventoryItemForMaking>();
        inventoryItemForMaking.InitializeItemFromCrafting(transform.GetComponent<InventoryManagerForMaking>());
        inventoryItemForMaking.image.sprite = sushiImage;
    }

    public void RemoveItemFromCrafting()
    {
        foreach (var craftingSlot in craftingSlots)
        {
            if (craftingSlot.childCount != 0)
            {
                if (craftingSlot.GetComponentInChildren<InventoryItemForMaking>().itemCount > 1)
                {
                    craftingSlot.GetComponentInChildren<InventoryItemForMaking>().itemCount -= 1;
                    craftingSlot.GetComponentInChildren<InventoryItemForMaking>().itemCountText.text =
                        craftingSlot.GetComponentInChildren<InventoryItemForMaking>().itemCount
                            .ToString();
                }
                else
                {
                    DestroyImmediate(craftingSlot.GetChild(0).gameObject);
                }
            }
        }

        Crafting();
    }

    public void Update()
    {
        remainingTime = transform.GetComponent<CountDownTimer>().remainingTime;
    }

    IEnumerator OrderMatching()
    {
        while (remainingTime > 0f)
        {
            int randomNum = Random.Range(0, sushiArray.Length);
            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
            Debug.Log("order");

            foreach (var orderSlot in orderHanger)
            {
                if (orderSlot.childCount == 0)
                {
                    GameObject order = Instantiate(inventoryItemPrefabForMaking, orderSlot);
                    InventoryItemForMaking inventoryItemForMaking = order.GetComponent<InventoryItemForMaking>();
                    order.GetComponent<Image>().raycastTarget = false;
                    order.transform.GetComponentInChildren<TMP_Text>().alpha = 0;
                    inventoryItemForMaking.sushiSpriteOnOrder = sushiArray[randomNum].image;
                    inventoryItemForMaking.orderPrice = sushiOrdersArray[randomNum].price;
                    inventoryItemForMaking.InitializeOrder(transform.GetComponent<InventoryManagerForMaking>());
                    break;
                }
            }
        }
    }

    /*public void Crafting(Transform clickedCraftingSlot, int clickedCraftingSlotIndex)
    {
        if (matchSushiList.Count > 0)
        {
            foreach (var sushi in matchSushiList)
            {
                RecipesDict[0] = sushi.recipe1;
                RecipesDict[1] = sushi.recipe2;
                RecipesDict[2] = sushi.recipe3;

                for (int i = 0; i < RecipesDict.Count; i++)
                {
                    if (clickedCraftingSlot.GetChild(0).GetComponent<Image>().sprite !=
                        RecipesDict[i][clickedCraftingSlotIndex])
                    {
                        matchSushiList.Remove(sushi);
                    }
                }
            }

            if (matchSushiList.Count == 1)
            {
                GameObject newItemGo = Instantiate(inventoryItemPrefabForMaking, craftingResultSlot);
                newItemGo.GetComponent<InventoryItemForMaking>().image.sprite = matchSushiList[0].image;
            }
        }
        else
        {
            foreach (var sushi in sushiArray)
            {
                RecipesDict[0] = sushi.recipe1;
                RecipesDict[1] = sushi.recipe2;
                RecipesDict[2] = sushi.recipe3;

                for (int i = 0; i < RecipesDict.Count; i++)
                {
                    if (clickedCraftingSlot.GetChild(0).GetComponent<Image>().sprite ==
                        RecipesDict[i][clickedCraftingSlotIndex])
                    {
                        matchSushiList.Add(sushi);
                    }
                }
            }
        }
    }*/
}