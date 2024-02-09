using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInterface : UserInterface
{
    [SerializeField] GameObject[] slots;
    protected override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            var obj = slots[i];
            AddSlotsEvents(obj);
            inventory.Slots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.Slots[i]);
        }
    }

}
