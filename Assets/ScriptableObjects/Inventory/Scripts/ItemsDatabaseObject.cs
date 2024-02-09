using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item Database", menuName = "InventorySystem/Database")]
public class ItemsDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private ItemObject[] itemObjects;

    public int Count => itemObjects.Length;

    [ContextMenu("Update ID's")]
    public void UpdateIDs()
    {
        Debug.Log("Updating IDs in DB: DESACTIVADO");
        /*
        for (int i = 0; i < itemObjects.Length; i++)
        {
            if (itemObjects[i].data.id != i)
                itemObjects[i].data.id = i;
        }
        */
    }
    public void OnAfterDeserialize()
    {
        UpdateIDs();
    }

    public void OnBeforeSerialize()
    {
    }

    public ItemObject GetItemFromId(int id) => itemObjects[id];

}
