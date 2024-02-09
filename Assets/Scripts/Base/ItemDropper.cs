using System.Numerics;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class ItemDropper : MonoBehaviour
{

    [SerializeField] List<ItemDropProb> itemsToDrop;
    [SerializeField] bool destroyOnDrop = true;
    [SerializeField][Range(0, 100)] int defaultItemsCount = 3;


    GameObject prefab;

    /*
    private void OnEnable()
    {
        #if UNITY_EDITOR
        database = (ItemsDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemsDatabaseObject));
        #else
        database = Resources.Load<ItemsDatabaseObject>("Database");
        #endif
    }
    */

    void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/GroundItem");
        if (defaultItemsCount > 0 && itemsToDrop.Count == 0)
        {
            //itemsToDrop = new List<ItemDropProb>();

            var itemsDb = GameManager.GetItemsDB();

            for (int i = 0; i < defaultItemsCount; i++)
            {
                //Debug.Log("added items to drop");
                float rand = Random.Range(1f, 100f);
                ItemObject itemObj = itemsDb.GetItemFromId(Random.Range(0, itemsDb.Count));
                //Debug.Log($"ground item created with item: {itemObj.name} id: {itemObj.data.id}");

                itemsToDrop.Add(new ItemDropProb(itemObj, rand, 0, 1));
            }
        }
    }

    public void Drop()
    {
        if (itemsToDrop != null)
        {
            foreach (ItemDropProb toDrop in itemsToDrop)
            {
                //Debug.Log("dropping item");
                int cantToDrop = toDrop.DiceItemsToDropCount();
                if (cantToDrop > 0)
                    CreateInstance(toDrop.itemObj, cantToDrop);
            }


            if (destroyOnDrop)
                Destroy(this);
        }
        else
        {
            Debug.Log("not gonna drop");
        }
    }

    void CreateInstance(ItemObject itemObj, int count)
    {
        if (itemObj.data.isEmpty())
        {
            Debug.Log($"ERROR: trying to create obj with bad id, name: {itemObj.name}");
        }
        GameObject itemdrop = Instantiate(prefab);//, gameObject.transform);
        itemdrop.transform.position = this.transform.position;
        itemdrop.GetComponent<GroundItem>().item = itemObj;
        itemdrop.GetComponent<GroundItem>().cant = count;
    }
}

[System.Serializable]
public class ItemDropProb
{
    [SerializeField] ItemObject _itemObj;
    [SerializeField][Range(0.0f, 100f)] float probability;
    [SerializeField] int minCount;
    [SerializeField] int maxCount;
    public ItemObject itemObj => _itemObj;

    public ItemDropProb(ItemObject itemObj, float prob, int min, int max)
    {
        this._itemObj = itemObj;
        this.probability = prob;
        this.minCount = min;
        this.maxCount = max;
    }

    public int DiceItemsToDropCount()
    {
        int count = minCount;
        for (int i = 0; i < maxCount - minCount; i++)
        {
            if (Random.Range(1f, 101f) <= probability)
            {
                count++;
            }
        }
        return count;
    }
}