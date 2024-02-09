using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventorySystem/Items/PotionSlot", fileName ="PotionSlot")]
public class ThrowableSlot : ScriptableObject 
{
    public int amount;
    public Throwable throwable;
    public Sprite uiDisplay;
}
