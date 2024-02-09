using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundOnAwake : MonoBehaviour
{
    private void Awake()
    {
        GameManager.GetAudioManager().PlayClipByName("Explosion");
    }

}
