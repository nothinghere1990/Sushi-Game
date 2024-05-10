using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public bool isRice;
    public int price;
    
    [FoldoutGroup("Crafting Recipes")]
    public Sprite[] recipe1 = new Sprite[3];
    [FoldoutGroup("Crafting Recipes")]
    public Sprite[] recipe2 = new Sprite[3];
    [FoldoutGroup("Crafting Recipes")]
    public Sprite[] recipe3 = new Sprite[3];
}