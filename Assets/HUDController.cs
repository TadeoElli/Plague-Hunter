using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] GameObject SelectedPotionImage;
    [SerializeField] GameObject SelectedPotionCount;


    public void SetSelectedPotion(Sprite image, int amount)
    {
        SelectedPotionImage.GetComponent<Image>().sprite = image;
        SelectedPotionCount.GetComponent<TextMeshProUGUI>().text = amount.ToString("n0");
    }
}
