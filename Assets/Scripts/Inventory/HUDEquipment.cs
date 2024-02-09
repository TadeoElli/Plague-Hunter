using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDEquipment : StaticInterface
{
    public SelectableItemSlotList throwables = new SelectableItemSlotList();
    public SelectableItemSlotList consumables = new SelectableItemSlotList();
    public SelectableItemSlotList enhancers = new SelectableItemSlotList();
    override protected void Start()
    {
        base.Start();

        foreach (InventorySlot item in slotsOnInterface.Values)
        {
            if (Array.Exists(item.AllowedItems, type => type == ItemType.Throwable))
                throwables.Add(item);
            if (Array.Exists(item.AllowedItems, type => type == ItemType.Consumable))
                consumables.Add(item);
            if (Array.Exists(item.AllowedItems, type => type == ItemType.WeaponEnhancer))
                enhancers.Add(item);
        }
        //Debug.Log($"throwable slots: {throwables.Count}");
        //Debug.Log($"consumable slots: {consumables.Count}");
        //Debug.Log($"enhancers slots: {enhancers.Count}");
        throwables.EnableVisualSelection();
    }

}
//[System.Serializable]
public class SelectableItemSlotList : List<InventorySlot>
{
    bool visualSelection = false;
    private int index = 0;

    public bool HasNoItems()
    {
        foreach (InventorySlot slot in this)
            if (slot.item.isEmpty() == false)
                return false;
        return true;
    }
    public void EnableVisualSelection()
    {
        visualSelection = true;
        VisualSelect();
    }
    public void VisualSelect()
    {
        if (visualSelection)
        {
            //Debug.Log($"items in list {this.Count}");
            for (int i = 0; i < this.Count; i++)
            {
                //TODO: en vez de esto podria usar un empty monobehaviour
                Image imageObj = this[i].slotDisplay.transform.GetChild(0).GetComponentsInChildren<Image>().Where(r => r.tag == Tags.InventorySelection).ToArray()[0];
                //Debug.Log($"{imageObj.color} on obj in index {i}");
                if (i == index)
                {
                    imageObj.enabled = true;
                    //imageObj.color = new Color(imageObj.color.r, imageObj.color.g, imageObj.color.b, 0);
                }
                else
                {
                    imageObj.enabled = false;
                    //imageObj.color = new Color(imageObj.color.r, imageObj.color.g, imageObj.color.b, 1);
                }
            }
        }
    }
    public void SelectNext()
    {
        if (HasNoItems())
            return;

        if (index == this.Count - 1)
            index = 0;
        else
            index++;

        if (this[index].item.isEmpty())
        {
            SelectNext();
        }
        else
        {
            //Debug.Log($"index is now {index}");
            VisualSelect();
        }

    }

    public void SelectPrevious()
    {
        if (HasNoItems())
            return;

        if (index == 0)
            index = this.Count - 1;
        else
            index--;

        if (this[index].item.isEmpty())
        {
            SelectPrevious();
        }
        else
        {
            //Debug.Log($"index is now {index}");
            VisualSelect();
        }
    }

    public Item UseSelectedItem()
    {
        InventorySlot itemSlot = this[index];
        InventorySlot oldItemSlot = new InventorySlot(itemSlot); //esto es porque despues se puede llegar a eliminar el obj.
        if (itemSlot != null)
        {
            itemSlot.RemoveOne();
            if (GetSelectedItem() == null) //aca forzamos la seleccion de un nuevo item
                SelectNext();
            return oldItemSlot.item;
        }

        return null;
    }

    public Item GetSelectedItem()
    {
        if (HasSelectedItem())
            return this[index].item;
        return null;
    }

    public bool HasSelectedItem()
    {
        return !this[index].item.isEmpty() && this[index].amount > 0;
    }

    public bool SetSelectPostion(int value)
    {
        if (value >= 0 && value < this.Count)
        {
            index = value;
            return true;
        }
        return false;
    }

}