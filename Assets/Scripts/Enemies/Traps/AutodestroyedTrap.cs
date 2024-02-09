using UnityEngine;

public class AutodestroyedTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            //GameManager.GetAudioManager().PlayClipByName("TrapFreeze");
            ItemDropper itemDrop = this.gameObject.GetComponent<ItemDropper>();
            if (itemDrop != null)
                itemDrop.Drop();
            else
                Debug.Log("Drop not founded");
            Destroy(gameObject);
        }
    }
}
