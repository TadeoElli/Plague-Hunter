using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public GameObject itemSlotPrefab;
    public int xStart = -110;
    public int yStart = 210;
    public int xSpaceBetweenItems = 55;
    public int ySpaceBetweenItems = 55;
    public int columns = 5;
    protected override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Slots.Length; i++)
        {
            var obj = CreateSlot(i);
            AddSlotsEvents(obj);

            inventory.Slots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.Slots[i]);
        }
    }
    GameObject CreateSlot(int i)
    {
        var obj = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        if (inventory.Slots[i].amount > 0)
        {
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Slots[i].amount.ToString("n0");
        }
        return obj;
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(xStart + (xSpaceBetweenItems * (i % columns)), yStart - (ySpaceBetweenItems * (i / columns)), 0f);
    }
}
