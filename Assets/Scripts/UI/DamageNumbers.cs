using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//TPFinal - Tadeo Elli.

public class DamageNumbers : MonoBehaviour
{

    public float upVelocity;
    public float lifeTime;
    public float minFont;
    public float maxFont;


    public void Inicializar(float _number, DamageTypes type)
    {
        TextMeshProUGUI number = GetComponentInChildren<TextMeshProUGUI>();

        switch (type)
            {
                case DamageTypes.basic:
                    number.color = new Color32(255, 255, 255, 255);
                    break;

                case DamageTypes.venom:
                    number.color = new Color32(0, 113, 27, 255);
                    break;

                case DamageTypes.fire:
                    number.color = new Color32(243, 0, 0, 255);
                    break;

                case DamageTypes.critical:
                    number.color = new Color32(243, 142, 1, 255);
                    break;

                case DamageTypes.ice:
                    number.color = new Color32(1, 231, 243, 255);
                    break;

                case DamageTypes.explosion:
                    number.color = new Color32(240, 243, 1, 255);
                    break;
                    
                case DamageTypes.player:
                    number.color = new Color32(255, 0, 0, 255);
                    break;

                default:
                    number.color = new Color32(255, 255, 255, 255);
                    break;
            }
            number.text = _number.ToString();
            number.fontSize = ((_number/3) * minFont) / maxFont;
            if(number.fontSize < minFont)
            {
                number.fontSize = 0.4f;
            }
            Destroy(gameObject, lifeTime);
    }
    
    public void HealNumbers(float _number)
    {
        TextMeshProUGUI number = GetComponentInChildren<TextMeshProUGUI>();
        number.color = new Color32(0, 255, 40, 255);
        number.text = _number.ToString();
        Destroy(gameObject, lifeTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * upVelocity * Time.deltaTime;
    }
}
