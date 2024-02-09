using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public enum InventoryInterfaceType
{
    inventory,
    equipment,
    chest
}

[CreateAssetMenu(fileName = "Inventory", menuName = "InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemsDatabaseObject database; // Lo privado no lo serializa
    public InventoryInterfaceType type;
    public Inventory Container;
    public InventorySlot[] Slots { get { return Container.Slots; } }

    public bool AddItem(Item item, int amount)
    {

        if (EmptySlotCount <= 0 || item.isEmpty())
        {
            return false;
        }

        InventorySlot slot = FindItemOnInventory(item);
        if (slot == null || !slot.itemObject.stackable)
        {
            SetFirstEmptySlot(item, amount);
        }
        else
        {
            slot.AddAmount(amount);
        }
        return true;


    }

    public InventorySlot FindItemOnInventory(Item item)
    {
        foreach (InventorySlot slot in Slots)
        {
            if (slot.item.id == item.id)
            {

                return slot;
            }
        }
        return null;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            foreach (InventorySlot slot in Slots)
            {
                if (slot.item.isEmpty())
                {
                    counter++;
                }
            }
            return counter;
        }
    }
    public InventorySlot SetFirstEmptySlot(Item item, int amount)
    {
        foreach (InventorySlot slot in Slots)
        {
            if (slot.item.isEmpty())
            {
                slot.UpdateSlot(item, amount);
                return slot;
            }
        }
        return null; //TODO: falta decidir que hacer cuando no hay mas espacio
    }


    public bool SwapSlotsContents(InventorySlot slot1, InventorySlot slot2)
    {
        //Debug.Log("swapping");
        if (slot1.CanPlaceInSlot(slot2.itemObject) && slot2.CanPlaceInSlot(slot1.itemObject)) //chequeamos que se pueden cambiar de lugar
        {
            //Debug.Log("CanSwap");
            InventorySlot temp = new InventorySlot(slot2);
            slot2.UpdateSlot(slot1);
            slot1.UpdateSlot(temp);
            return true;
        }
        return false;
    }
    public void RemoveItem(Item item)
    {
        foreach (InventorySlot slot in Slots)
        {
            if (slot.item == item)
            {
                slot.SetEmpty();
            }
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        //string path  = Application.persistentDataPath + savePath; 
        string path = string.Concat(Application.persistentDataPath, savePath); // more performant

        //Forma de guardar en un json
        /*
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);
        bf.Serialize(file, saveData);
        file.Close();
        */

        //forma para guardar en binario
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {

        string path = string.Concat(Application.persistentDataPath, savePath); // more performant


        if (File.Exists(path))
        {
            /*
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open); // more performant
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
            */

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].UpdateSlot(newContainer.Slots[i]);
            }
            stream.Close();

        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
}
[System.Serializable]
public class Inventory
{
    //public List<InventorySlot> Slots = new List<InventorySlot>();
    public InventorySlot[] Slots = new InventorySlot[25];

    public void Clear()
    {
        foreach (InventorySlot slot in Slots)
        {
            slot.SetEmpty();
        }
    }
    /*
    public Inventory(){
        Slots = new InventorySlot[25];
        this.Clear();
    }
    */
}
public delegate void SlotUpdated(InventorySlot slot);
[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    [System.NonSerialized] public UserInterface parent;
    [System.NonSerialized] public GameObject slotDisplay;
    [System.NonSerialized] public SlotUpdated onBeforeUpdate;
    [System.NonSerialized] public SlotUpdated onAfterUpdate;
    public Item item;
    public int amount;
    public ItemObject itemObject
    {
        get
        {
            if (item.isEmpty())
            {
                return null;
            }
            return parent.inventory.database.GetItemFromId(item.id);
        }
    }


    public InventorySlot(Item _item, int _amount)
    {
        UpdateSlot(_item, _amount);
    }

    public InventorySlot(InventorySlot slot)
    {
        UpdateSlot(slot);
    }
    public InventorySlot()
    {
        SetEmpty();
    }
    public void UpdateSlot(Item _item, int _amount)
    {
        if (onBeforeUpdate != null)
        {
            onBeforeUpdate.Invoke(this);
        }
        item = _item;
        amount = _amount;

        if (onAfterUpdate != null)
        {
            onAfterUpdate.Invoke(this);
        }
    }
    public void UpdateSlot()
    {
        if (onBeforeUpdate != null)
        {
            onBeforeUpdate.Invoke(this);
        }

        if (onAfterUpdate != null)
        {
            onAfterUpdate.Invoke(this);
        }
    }

    public void UpdateSlot(InventorySlot slot)
    {
        UpdateSlot(slot.item, slot.amount);
    }
    public void AddAmount(int value)
    {
        UpdateSlot(item, amount + value);
    }
    public bool RemoveAmount(int value)
    {
        int newAmount = amount - value;
        if (newAmount > 0)
        {
            UpdateSlot(item, newAmount);
            return true;
        }
        else if (newAmount == 0)
        {
            SetEmpty();
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool RemoveOne() => RemoveAmount(1);


    public void SetEmpty()
    {
        UpdateSlot(new Item(), 0);
    }
    public bool CanPlaceInSlot(ItemObject _itemObject)
    {
        //Debug.Log("Place in Slot");

        if (_itemObject == null || _itemObject.data.isEmpty() || AllowedItems.Length <= 0 || AllowedItems.Contains(_itemObject.type))
        {
            //Debug.Log("Can Place in Slot");
            return true;
        }
        return false;
    }
}