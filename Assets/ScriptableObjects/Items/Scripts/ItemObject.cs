using UnityEngine;
using System.Collections.Generic;
public enum ItemType
{
    Default,
    Ingredient,
    Head,
    Chest,
    Feet,
    Weapon,
    Shield,
    Consumable,
    Throwable,
    WeaponEnhancer,
}

public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

/*
public enum ItemType
{
    Ingredient,
    Consumable,
    WeaponEnhancer,
    Throwable,
}
*/
public enum AttributeType
{
    Agility,
    Strength,
    Stamina,
    Intellect,
}
[CreateAssetMenu(menuName = "InventorySystem/Items", fileName = "Items")]

public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public bool stackable = true;
    public ItemType type;
    public ItemRarity rarity; //TODO: implementar en logica
    [TextArea(15, 20)] public string description;
    public Item data = new Item();
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public List<ItemAttribute> attributes = new List<ItemAttribute>(); //TODO: (no seria mejor un diccionario, asi no puede haber atributos duplicados?)
    public bool isEmpty()
    {
        //return id == -1;
        return id < 0;
    }

    public Item()
    {
        id = -1;
        name = "";
    }
    public Item(ItemObject itemObject)
    {
        name = itemObject.name;
        id = itemObject.data.id;

        foreach (ItemAttribute attr in itemObject.data.attributes)
        {
            attributes.Add(attr);
        }
    }
}
[System.Serializable]
public class ItemAttribute : IntModifier
{
    public AttributeType type;
    //public int min;
    //public int max;
    /*
    public ItemAttribute(int _min, int _max, AttributeType _type)
    {
        type = _type;
        min = _min;
        max = _max;
        GenerateValues();
    }
    */
    public ItemAttribute(int _value, AttributeType _type)
    {
        type = _type;
        value = _value;
    }
    public ItemAttribute(ItemAttribute attribute)
    {
        type = attribute.type;
        value = attribute.value;
        //min = attribute.min;
        //max = attribute.max;
        //GenerateValues();
    }

    /*
    public void GenerateValues()
    {
        value = UnityEngine.Random.Range(min, max);
    }
    */
}