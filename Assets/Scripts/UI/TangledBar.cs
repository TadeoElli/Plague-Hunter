using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TangledBar : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] EntityOnFrozen onFrozen;
    [SerializeField] Image eventBar;
    public float maxTangledTime;

    // Update is called once per frame

    void Update()
    {
        BarFollow();
    }

    void BarFollow()
    {
        eventBar.fillAmount = onFrozen.timeLeft / maxTangledTime;
    }
}
