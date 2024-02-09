using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    
    void LateUpdate()
    {
        Camera camera = GameManager.GetMainCamera();
        //transform.forward = camera.transform.forward;
        transform.forward = transform.position - camera.transform.position;
    }
}
