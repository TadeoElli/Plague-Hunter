using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInGameInventory : MonoBehaviour
{

    private HUDEquipment hudEquipment;
    [SerializeField] ItemPrefabMapper itemMapper;
    void Start()
    {
        hudEquipment = GameManager.GetHUD();
    }

    public void SwitchPotionUP() => hudEquipment.throwables.SelectNext();
    public void SwitchPotionDown() => hudEquipment.throwables.SelectPrevious();

    public Throwable UseSelectedPotion()
    {
        Item item = hudEquipment.throwables.UseSelectedItem();
        return itemMapper.GetPrefab(item).GetComponentInChildren<Throwable>();
    }
    public Throwable GetSelectedPotion()
    {
        Item item = hudEquipment.throwables.GetSelectedItem();
        return itemMapper.GetPrefab(item).GetComponentInChildren<Throwable>();
    }

    public bool HasSelectedThrowable() => hudEquipment.throwables.HasSelectedItem();
    public bool HasHealingPotion() => hudEquipment.consumables.HasSelectedItem();

    public Item UseHealingPotion() => hudEquipment.consumables.UseSelectedItem();

    public bool HasSelectedOil(KeyCode key)
    {
        int index = 0;
        if (key == KeyCode.F)
            index = 0;
        else if (key == KeyCode.G)
            index = 1;
        else
            return false;

        if (hudEquipment.enhancers.SetSelectPostion(index))
            return hudEquipment.enhancers.HasSelectedItem();
        else
            return false;
    }

    public OilEffect UseSelectedOil() //TODO: se usa el gameobject entero solo para no hacer 2 clases de mapper distintas
    {
        Item item = hudEquipment.enhancers.UseSelectedItem();
        return itemMapper.GetPrefab(item).GetComponentInChildren<OilEffect>();
    }
    public OilEffect GetSelectedOil()
    {
        Item item = hudEquipment.enhancers.GetSelectedItem();
        return itemMapper.GetPrefab(item).GetComponentInChildren<OilEffect>();
    }

}
