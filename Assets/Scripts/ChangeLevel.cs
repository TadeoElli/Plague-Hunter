using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeLevel : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] String level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            Time.timeScale = 1;
            //SceneManager.LoadScene(level);
            GameManager.GoToNextLevel();
        }
    }
}
