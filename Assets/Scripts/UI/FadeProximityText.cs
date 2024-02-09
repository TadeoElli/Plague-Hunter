using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeProximityText : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    [SerializeField] private float proximity;
    public TextMeshProUGUI text;
    void Start()
    {
        PlayerController player = GameManager.GetPlayer();
        target = player.transform;
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance >= proximity)
        {
            text.color = new Color32(255, 255, 255, 0);
        }
        else
        {
            text.color = new Color32(255, 255, 255, 255);
        }
    }
}
