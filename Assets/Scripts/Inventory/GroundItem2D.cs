using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GroundItem2D : GroundItem, ISerializationCallbackReceiver
{
    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        //Debug.Log(item.name); 
        renderer.sprite = item.uiDisplay;
        EditorUtility.SetDirty(renderer);
#endif
    }
}
