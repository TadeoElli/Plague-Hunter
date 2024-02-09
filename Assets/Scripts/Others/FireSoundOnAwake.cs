using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSoundOnAwake : MonoBehaviour
{
    private void Start()
    {
        GameManager.GetAudioManager().PlayClipByName("FireThreeSeconds");
    }
}
