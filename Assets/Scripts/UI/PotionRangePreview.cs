using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionRangePreview : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer my_render;

    void Start()
    {
        my_render = GetComponent<SpriteRenderer>();
    }

    public void ActivatePotionPreview()
    {
        my_render.enabled = true;
    }

    public void DesactivatePotionPreview()
    {
        my_render.enabled =false;
    }
}
