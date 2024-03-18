using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Ingredient,
    Sushi
};

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public ItemType itemType;
    public bool isRice;
    public int price;
}
