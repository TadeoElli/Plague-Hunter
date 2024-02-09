using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class UserInterface : MonoBehaviour
{
    public InventoryObject inventory; //no podemos serializarlo
    protected Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    public int itemSize = 50;

    private AudioManager audioManager;

    protected virtual void Start()
    {
        audioManager = GameManager.GetAudioManager();
        foreach (InventorySlot slot in inventory.Slots)
        {
            slot.parent = this;
            slot.onAfterUpdate += OnSlotUpdate;
        }

        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
        slotsOnInterface.UpdateSlotDisplay();
    }

    private void OnSlotUpdate(InventorySlot slot)
    {
        //TODO: en vez de esto podria usar un empty monobehaviour
        /*
        if (slot.slotDisplay == null) //TODO: Sin esto tira error solo cuando arranca, pero cuando cambia de nivel borra los items
        {
            return;
        }
        */
        Image imageObj = slot.slotDisplay.transform.GetComponentsInChildren<Image>().Where(r => r.tag == Tags.InventoryImageSlot).ToArray()[0];

        if (!slot.item.isEmpty()) //Slot with Item
        {
            imageObj.sprite = slot.itemObject.uiDisplay;
            imageObj.color = new Color(1, 1, 1, 1);
            slot.slotDisplay.transform.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount == 1 ? "" : slot.amount.ToString("n0");
        }
        else //Empty Slot
        {
            imageObj.sprite = null;
            imageObj.color = new Color(1, 1, 1, 0);
            slot.slotDisplay.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    // Update is called once per frame
    /*
    void Update()
    {
        slotsOnInterface.UpdateSlotDisplay();
    }
    */


    #region drawing


    protected abstract void CreateSlots();

    protected void AddSlotsEvents(GameObject obj)
    {
        AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
        AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
        AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
        AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
        AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
    }

    #endregion

    #region drag and drop

    private void OnEnterInterface(GameObject obj)
    {
        //Debug.Log("enter interface");
        MouseData.interfaceIsOver = obj.GetComponent<UserInterface>();
    }

    private void OnExitInterface(GameObject obj)
    {
        //Debug.Log("exit interface");

        MouseData.interfaceIsOver = null;
    }

    protected void OnDragStart(GameObject obj)
    {
        if (slotsOnInterface[obj].item.isEmpty())
        {
            return;
        }
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
        audioManager.PlayClipByName("SelectedOrClicked");
    }
    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = new GameObject();
        var rt = tempItem.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(itemSize, itemSize);
        tempItem.transform.SetParent(transform.parent);

        Image image = tempItem.AddComponent<Image>();
        image.sprite = slotsOnInterface[obj].itemObject.uiDisplay;
        image.raycastTarget = false;
        return tempItem;
    }
    protected void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    protected void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);
        if (!MouseData.isOverInterface())
        {
            slotsOnInterface[obj].SetEmpty();
            audioManager.PlayClipByName("DiscardItem");
            return;
        }
        if (MouseData.slotHoveredOver)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            bool couldSwap = inventory.SwapSlotsContents(slotsOnInterface[obj], mouseHoverSlotData);
            if (couldSwap)
            {
                audioManager.PlayClipByName("PlaceItem");
            }
            else
            {
                audioManager.PlayClipByName("IncorrectPlace");
            }
        }
    }

    protected void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
        //Debug.Log("over item"); //TODO: preview de objetos
    }

    protected void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
    }
    #endregion

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

}

public static class MouseData
{
    public static UserInterface interfaceIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHoveredOver;

    public static bool isOverInterface()
    {
        return interfaceIsOver != null;
    }

}

public static class ExtentionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> slot in _slotsOnInterface)
        {
            slot.Value.UpdateSlot();
            /*
            if (!slot.Value.item.isEmpty()) //Slot with Item
            {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = slot.Value.itemObject.uiDisplay;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = slot.Value.amount == 1 ? "" : slot.Value.amount.ToString("n0");
            }
            else //Empty Slot
            {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
            */
        }
    }
}