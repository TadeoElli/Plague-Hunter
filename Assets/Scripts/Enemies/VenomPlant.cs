using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomPlant : MonoBehaviour
{
    // Start is called before the first frame update
    bool isSet = true;
    [SerializeField] GameObject venom;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            if (isSet)
            {
                Debug.Log("Activando hongo veneno");
                GameManager.GetAudioManager().PlayClipByName("gasRelease");
                isSet = false;
                var col = GetComponent<SphereCollider>();
                col.enabled =false;
                //GameObject.Instantiate(venom,this.gameObject.transform);
                Instantiate(venom,transform.position,new Quaternion());
            }

        }

    }

}
