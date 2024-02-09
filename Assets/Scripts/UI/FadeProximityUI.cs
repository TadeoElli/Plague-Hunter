using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeProximityUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    [SerializeField] private float proximity;
    public Image image;
    void Start()
    {
        PlayerController player = GameManager.GetPlayer();
        target = player.transform;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance >= proximity)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 255f);
        }
    }
}
