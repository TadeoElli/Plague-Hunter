using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private InventoryObject inventory;
    private InventoryObject equipmentInventory;
    private InventoryObject hudEquipmentInventory;
    [SerializeField] EntityAttributes playerAttributes;

    private void Start()
    {
        inventory = GameManager.GetInventory();
        equipmentInventory = GameManager.GetEquipmentInventory();
        hudEquipmentInventory = GameManager.GetHudEquipmentInventory();

        playerAttributes.Init(); //TODO: queria agregarlo en el constructor, pero por algun motivo se llama como 3  veces

        foreach (InventorySlot slot in equipmentInventory.Slots)
        {
            slot.onBeforeUpdate += OnBeforeSlotUpdate;
            slot.onAfterUpdate += OnAfterSlotUpdate;
        }
    }
    public void OnBeforeSlotUpdate(InventorySlot slot)
    {
        if (slot.item.isEmpty())
            return;

        switch (slot.parent.inventory.type)
        {
            case InventoryInterfaceType.inventory:
                break;

            case InventoryInterfaceType.equipment:
                Debug.Log($"Removed item: {slot.itemObject.name} on {slot.parent.inventory.type}, Allowed Items: {string.Join(", ", slot.AllowedItems)}");
                foreach (ItemAttribute itemAttr in slot.item.attributes)
                {
                    playerAttributes.RemoveAttribute(itemAttr);
                }
                break;

            case InventoryInterfaceType.chest:
                break;

            default:
                break;
        }
    }
    public void OnAfterSlotUpdate(InventorySlot slot)
    {
        if (slot.item.isEmpty())
            return;

        switch (slot.parent.inventory.type)
        {
            case InventoryInterfaceType.inventory:
                break;

            case InventoryInterfaceType.equipment:
                Debug.Log($"Added item: {slot.itemObject.name} on {slot.parent.inventory.type}, Allowed Items: {string.Join(", ", slot.AllowedItems)}");
                foreach (ItemAttribute itemAttr in slot.item.attributes)
                {

                    playerAttributes.AddAttribute(itemAttr);
                }
                break;

            case InventoryInterfaceType.chest:
                break;

            default:
                break;
        }
    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.Load();
            equipment.Load();
        }
    }
    */
    void OnTriggerEnter(Collider other)
    {
        GroundItem groundItem = other.gameObject.GetComponent<GroundItem>();
        //Debug.Log($"colliding con {other.name}");

        if (groundItem != null)
        {
            //Debug.Log($"ground item found with item: {groundItem.item.data.name} id: {groundItem.item.data.id}  cant: {groundItem.cant}");
            GameManager.GetAudioManager().PlayClipByName("ItemPickUp");

            if (hudEquipmentInventory.FindItemOnInventory(groundItem.item.data) != null)
            {
                if (hudEquipmentInventory.AddItem(new Item(groundItem.item), groundItem.cant))
                {
                    Debug.Log($"picked up item: {groundItem.item.name}, cant: {groundItem.cant}");
                    Destroy(other.gameObject);
                }
            }
            else
            {
                if (inventory.AddItem(new Item(groundItem.item), groundItem.cant))
                {
                    Debug.Log($"picked up item: {groundItem.item.name}, cant: {groundItem.cant}");
                    Destroy(other.gameObject);
                }
            }

        }
    }

}

[System.Serializable]
public class Attribute
{
    public AttributeType type; //TODO: hacer un generic con esto https://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
    public Modifiable<IntModifier, int> value;
    [SerializeField] IntModifier baseValue;

    [System.NonSerialized] public EntityAttributes parent; //TODO: cambiar esto para que sea portable entre enemigos.
    public void SetParent(EntityAttributes parent)
    {
        this.parent = parent;
        value = new Modifiable<IntModifier, int>(AttributeModifiedEvent);
        value.AddModifier(baseValue);
    }
    public void AttributeModifiedEvent()
    {
        parent.AttributeModifiedEvent(this);
    }

}
[System.Serializable]
public class EntityAttributes //TODO: hacer un generic con esto.
{
    [SerializeField] private List<Attribute> attributes = new List<Attribute>(); //TODO: esto podria ser mejor un dic siendo type el key. ya que no deberia tener atributos duplicados. pero a unity no le gusta serializar diccionarios
    public void Init()
    {
        //Debug.Log("playerAttributes");
        foreach (Attribute attr in attributes)
        {
            //Debug.Log("setParent");
            attr.SetParent(this);
        }
    }
    public void AddAttribute(ItemAttribute itemAttr)
    {
        foreach (Attribute playerAttr in attributes)
        {
            if (itemAttr.type == playerAttr.type)
            {
                Debug.Log($"{itemAttr.type} {itemAttr.value}");
                playerAttr.value.AddModifier(itemAttr);
            }
        }
        /*
        Attribute playerAttr = attributes.Find(x => x.type == itemAttr.type);
        if (playerAttr != null)
            playerAttr.value.AddModifier(itemAttr);
        */
    }
    public void RemoveAttribute(ItemAttribute itemAttr)
    {
        foreach (Attribute playerAttr in attributes)
        {
            if (itemAttr.type == playerAttr.type)
            {
                playerAttr.value.RemoveModifier(itemAttr);
            }
        }
        /*
        Attribute playerAttr = attributes.Find(x => x.type == itemAttr.type);
        if (playerAttr != null)
            playerAttr.value.RemoveModifier(itemAttr);
        */
    }
    public void AttributeModifiedEvent(Attribute attribute) //TODO: hacer esto como delegado en el PlayerInventory en vez de pasar el parent
    {
        //Debug.Log($"{attribute.type} was updated! Value is now: {attribute.value.Value}");
    }
}