using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;


[CreateAssetMenu(fileName = "ItemPrefabMapper", menuName = "InventorySystem/ItemPrefabMapper")]
public class ItemPrefabMapper : ScriptableObject//, ISerializationCallbackReceiver
{
    //private ItemsDatabaseObject DB;
    [SerializeField] List<ItemToPrefab> itemsToPrefab;

    /*
    public void OnAfterDeserialize()
    {  
        foreach (ItemToPrefab itemMap in itemsToPrefab)
        {
            if(itemMap.itemObj.data != null){
                Debug.Log($"item id es: {itemMap.itemObj.data.id}");
            }
        }
    }

    public void OnBeforeSerialize()
    {
        //throw new NotImplementedException();
    }
    */

    public GameObject GetPrefab(Item item){
        foreach (ItemToPrefab itemMap in itemsToPrefab)
        {
            if(item.id == itemMap.itemObj.data.id){
                return itemMap.prefab;
            }
        }
        return null;
    }
}

[System.Serializable]
public class ItemToPrefab
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public ItemObject itemObj;

}